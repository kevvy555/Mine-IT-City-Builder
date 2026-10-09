#!/usr/bin/env python3
"""Deterministic MineIT-Universe -> City Builder runtime canon importer.

The importer is the only production code that understands the authored Universe JSON
layout. Runtime game code consumes only the generated City Builder schema.
"""

from __future__ import annotations

import argparse
import hashlib
import json
import os
import subprocess
import sys
from pathlib import Path
from typing import Any, Dict, Iterable, List, Mapping, Sequence


class ImportFailure(RuntimeError):
    pass


def read_json(path: Path) -> Any:
    try:
        return json.loads(path.read_text(encoding="utf-8"))
    except FileNotFoundError as exc:
        raise ImportFailure(f"Required Universe file is missing: {path}") from exc
    except json.JSONDecodeError as exc:
        raise ImportFailure(f"Invalid JSON in {path}: {exc}") from exc


def write_canonical_json(path: Path, value: Any) -> bytes:
    payload = (
        json.dumps(value, ensure_ascii=False, sort_keys=True, separators=(",", ":"))
        + "\n"
    ).encode("utf-8")
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_bytes(payload)
    return payload


def git_head(source: Path) -> str:
    try:
        result = subprocess.run(
            ["git", "-C", str(source), "rev-parse", "HEAD"],
            check=True,
            capture_output=True,
            text=True,
        )
    except (OSError, subprocess.CalledProcessError) as exc:
        raise ImportFailure(f"Universe source is not a readable git checkout: {source}") from exc
    return result.stdout.strip()


def git_dirty(source: Path) -> bool:
    result = subprocess.run(
        ["git", "-C", str(source), "status", "--porcelain"],
        check=True,
        capture_output=True,
        text=True,
    )
    return bool(result.stdout.strip())


def resolve_collection_files(manifest: Mapping[str, Any], collection_name: str) -> List[str]:
    collections = manifest.get("collections")
    if not isinstance(collections, dict):
        raise ImportFailure("Universe manifest has no collections object.")

    declaration = collections.get(collection_name)
    if isinstance(declaration, str):
        return [declaration]
    if isinstance(declaration, list) and all(isinstance(x, str) for x in declaration):
        return declaration
    raise ImportFailure(
        f"Universe manifest does not declare collection '{collection_name}'."
    )


def load_collection(source: Path, manifest: Mapping[str, Any], name: str) -> List[Dict[str, Any]]:
    merged: List[Dict[str, Any]] = []
    for file_name in resolve_collection_files(manifest, name):
        value = read_json(source / "data" / file_name)
        if not isinstance(value, list):
            raise ImportFailure(f"Collection '{name}' shard '{file_name}' must be a JSON array.")
        for record in value:
            if not isinstance(record, dict):
                raise ImportFailure(f"Collection '{name}' contains a non-object record.")
            merged.append(record)

    seen: set[str] = set()
    for record in merged:
        stable_id = record.get("id")
        if not isinstance(stable_id, str) or not stable_id:
            raise ImportFailure(f"Collection '{name}' contains a record without a stable id.")
        if stable_id in seen:
            raise ImportFailure(f"Duplicate stable id '{stable_id}' in collection '{name}'.")
        seen.add(stable_id)
    return merged


def by_id(records: Sequence[Mapping[str, Any]]) -> Dict[str, Mapping[str, Any]]:
    return {str(record["id"]): record for record in records}


def validate_required(
    collections: Mapping[str, Sequence[Mapping[str, Any]]],
    required: Mapping[str, Sequence[str]],
) -> None:
    for collection_name, required_ids in required.items():
        if collection_name not in collections:
            raise ImportFailure(f"Required collection '{collection_name}' was not loaded.")
        ids = {str(x.get("id")) for x in collections[collection_name]}
        missing = [stable_id for stable_id in required_ids if stable_id not in ids]
        if missing:
            raise ImportFailure(
                f"Required canonical ids missing from '{collection_name}': {', '.join(missing)}"
            )


def validate_atlas(
    atlas: Mapping[str, Any],
    tiles: Sequence[Mapping[str, Any]],
    expected_count: int,
    planet_id: str,
    settlement_id: str,
) -> None:
    if atlas.get("planetId") != planet_id:
        raise ImportFailure("Atlas planetId does not match locked City Builder planet.")
    if atlas.get("originSettlementId") != settlement_id:
        raise ImportFailure("Atlas originSettlementId does not match Concordia.")
    if atlas.get("tileSizeKm") != 1:
        raise ImportFailure("City Builder requires canonical 1 km atlas tiles.")
    if len(tiles) != expected_count:
        raise ImportFailure(
            f"Expected {expected_count} atlas tiles but locked Universe contains {len(tiles)}."
        )

    ids: set[str] = set()
    coordinates: set[tuple[int, int]] = set()
    tile_by_id: Dict[str, Mapping[str, Any]] = {}

    for tile in tiles:
        tile_id = tile.get("id")
        if not isinstance(tile_id, str) or not tile_id:
            raise ImportFailure("Atlas tile without stable id.")
        if tile_id in ids:
            raise ImportFailure(f"Duplicate atlas tile id '{tile_id}'.")
        ids.add(tile_id)
        tile_by_id[tile_id] = tile

        if tile.get("atlasId") != atlas.get("id"):
            raise ImportFailure(f"Tile '{tile_id}' references the wrong atlas.")
        if tile.get("planetId") != planet_id:
            raise ImportFailure(f"Tile '{tile_id}' references the wrong planet.")

        x, y = tile.get("x"), tile.get("y")
        if not isinstance(x, int) or not isinstance(y, int):
            raise ImportFailure(f"Tile '{tile_id}' has invalid coordinates.")
        key = (x, y)
        if key in coordinates:
            raise ImportFailure(f"Duplicate atlas coordinates {key}.")
        coordinates.add(key)

        edges = tile.get("edgeContinuity")
        if not isinstance(edges, dict) or any(k not in edges for k in ("north", "east", "south", "west")):
            raise ImportFailure(f"Tile '{tile_id}' has incomplete edge continuity metadata.")

    if (0, 0) not in coordinates:
        raise ImportFailure("Locked atlas does not contain canonical tile (0,0).")

    for tile in tiles:
        tile_id = str(tile["id"])
        x, y = int(tile["x"]), int(tile["y"])
        for neighbor_id in tile.get("neighborTileIds", []):
            neighbor = tile_by_id.get(str(neighbor_id))
            if neighbor is None:
                raise ImportFailure(
                    f"Tile '{tile_id}' references missing neighbor '{neighbor_id}'."
                )
            dx = abs(int(neighbor["x"]) - x)
            dy = abs(int(neighbor["y"]) - y)
            if dx + dy != 1:
                raise ImportFailure(
                    f"Tile '{tile_id}' neighbor '{neighbor_id}' is not orthogonally adjacent."
                )
            if tile_id not in neighbor.get("neighborTileIds", []):
                raise ImportFailure(
                    f"Atlas neighbor relationship is not reciprocal: '{tile_id}' <-> '{neighbor_id}'."
                )


def select_ids(records: Sequence[Mapping[str, Any]], ids: Iterable[str]) -> List[Mapping[str, Any]]:
    index = by_id(records)
    return [index[stable_id] for stable_id in ids]


def compact_entity(record: Mapping[str, Any], fields: Sequence[str]) -> Dict[str, Any]:
    return {field: record.get(field) for field in fields if field in record}


def build_runtime_catalog(
    lock: Mapping[str, Any],
    manifest: Mapping[str, Any],
    config: Mapping[str, Any],
    collections: Mapping[str, Sequence[Mapping[str, Any]]],
) -> Dict[str, Any]:
    planet = by_id(collections["planets"])[config["planetId"]]
    settlement = by_id(collections["settlements"])[config["settlementId"]]

    reference_ids = {
        "celestialBodyKinds": [planet.get("celestialBodyKindId")],
        "worldTypes": [planet.get("worldTypeId")],
        "atmosphereTypes": [planet.get("atmosphereTypeId")],
        "surfaceLandforms": list(planet.get("dominantLandformIds", [])),
        "surfaceBiomes": list(planet.get("dominantBiomeIds", [])),
        "surfaceHydrospheres": list(planet.get("dominantHydrosphereIds", [])),
        "landscapeTilesets": [planet.get("landscapeTilesetId")],
    }

    referenced: Dict[str, List[Mapping[str, Any]]] = {}
    for name, ids in reference_ids.items():
        clean = [str(x) for x in ids if isinstance(x, str) and x]
        index = by_id(collections[name])
        missing = [stable_id for stable_id in clean if stable_id not in index]
        if missing:
            raise ImportFailure(
                f"Planet references missing ids in '{name}': {', '.join(missing)}"
            )
        referenced[name] = [index[stable_id] for stable_id in clean]

    required = config["requiredIds"]
    return {
        "formatVersion": 1,
        "universeRepository": lock["repository"],
        "universeCommit": lock["commit"],
        "universeSchemaVersion": manifest["schemaVersion"],
        "universeContentVersion": manifest.get("contentVersion", ""),
        "civilisationBaselineYear": manifest.get("civilisationBaselineYear"),
        "planet": compact_entity(
            planet,
            [
                "id", "name", "systemId", "worldType", "environment",
                "governingOrganisationId", "canonStatus", "celestialBodyKindId",
                "worldTypeId", "atmosphereTypeId", "dominantLandformIds",
                "dominantBiomeIds", "dominantHydrosphereIds",
                "landscapeTilesetId", "capitalSettlementId", "worldAtlasId",
            ],
        ),
        "settlement": compact_entity(
            settlement,
            [
                "id", "name", "systemId", "planetId", "locationType", "purpose",
                "governingOrganisationId", "canonStatus",
            ],
        ),
        "species": [
            compact_entity(x, ["id", "name", "speciesType", "homeworldId", "canonStatus"])
            for x in select_ids(collections["species"], required["species"])
        ],
        "organisations": [
            compact_entity(x, ["id", "name", "organisationType", "scale", "canonStatus"])
            for x in select_ids(collections["organisations"], required["organisations"])
        ],
        "currencies": [
            compact_entity(x, ["id", "name", "symbol", "currencyType"])
            for x in select_ids(collections["currencies"], required["currencies"])
        ],
        "worldReferences": {
            name: [
                compact_entity(x, ["id", "name", "description"])
                for x in values
            ]
            for name, values in referenced.items()
        },
    }


def build_atlas_index(
    lock: Mapping[str, Any],
    atlas: Mapping[str, Any],
    tiles: Sequence[Mapping[str, Any]],
) -> Dict[str, Any]:
    ordered = sorted(tiles, key=lambda x: (int(x["y"]), int(x["x"]), str(x["id"])))
    compact_tiles: List[Dict[str, Any]] = []

    for tile in ordered:
        image = tile.get("image") or {}
        edges = tile.get("edgeContinuity") or {}
        compact_tiles.append(
            {
                "id": tile["id"],
                "name": tile.get("name", ""),
                "x": tile["x"],
                "y": tile["y"],
                "sequence": tile.get("sequence", 0),
                "batch": tile.get("batch", 0),
                "ring": tile.get("ring", 0),
                "districtName": tile.get("districtName", ""),
                "zone": tile.get("zone", ""),
                "sector": tile.get("sector", ""),
                "mappedAreaKm2": tile.get("mappedAreaKm2", 1),
                "description": tile.get("description", ""),
                "northEdge": edges.get("north", ""),
                "eastEdge": edges.get("east", ""),
                "southEdge": edges.get("south", ""),
                "westEdge": edges.get("west", ""),
                "neighborTileIds": list(tile.get("neighborTileIds", [])),
                "referenceTileIds": list(tile.get("referenceTileIds", [])),
                "imageKey": image.get("key", ""),
                "imageGenerated": bool(image.get("generated", False)),
                "imageStatus": image.get("status", ""),
                "canonStatus": tile.get("canonStatus", ""),
            }
        )

    return {
        "formatVersion": 1,
        "universeCommit": lock["commit"],
        "atlas": {
            "id": atlas["id"],
            "name": atlas.get("name", ""),
            "planetId": atlas["planetId"],
            "originSettlementId": atlas["originSettlementId"],
            "tileSizeKm": atlas["tileSizeKm"],
            "coordinateOrigin": atlas.get("coordinateSystem", {}).get("origin", ""),
            "xPositive": atlas.get("coordinateSystem", {}).get("xPositive", ""),
            "yPositive": atlas.get("coordinateSystem", {}).get("yPositive", ""),
            "phaseOneTileCount": atlas.get("phaseOne", {}).get("tileCount", len(compact_tiles)),
        },
        "tiles": compact_tiles,
    }


def run_import(source: Path, output: Path, lock_path: Path, config_path: Path, verify_git: bool = True) -> Dict[str, Any]:
    lock = read_json(lock_path)
    config = read_json(config_path)

    expected_commit = lock.get("commit")
    if not isinstance(expected_commit, str) or len(expected_commit) != 40:
        raise ImportFailure("Universe lock must contain an exact 40-character commit SHA.")

    if verify_git:
        actual_head = git_head(source)
        if actual_head != expected_commit:
            raise ImportFailure(
                f"Universe checkout HEAD {actual_head} does not match lock {expected_commit}."
            )
        if git_dirty(source):
            raise ImportFailure("Universe checkout is dirty; refusing releasable import.")

    manifest = read_json(source / "data" / "manifest.json")
    if str(manifest.get("schemaVersion")) != str(lock.get("schemaCompatibility")):
        raise ImportFailure(
            f"Universe schema {manifest.get('schemaVersion')} is incompatible with lock "
            f"{lock.get('schemaCompatibility')}."
        )

    required_names = set(config["requiredIds"].keys())
    required_names.update(
        {
            "celestialBodyKinds",
            "worldTypes",
            "atmosphereTypes",
            "surfaceLandforms",
            "surfaceBiomes",
            "surfaceHydrospheres",
            "landscapeTilesets",
            "worldAtlasTiles",
        }
    )

    collections: Dict[str, List[Dict[str, Any]]] = {
        name: load_collection(source, manifest, name)
        for name in sorted(required_names)
    }
    validate_required(collections, config["requiredIds"])

    atlas = by_id(collections["worldAtlases"])[config["atlasId"]]
    atlas_tiles = [
        tile for tile in collections["worldAtlasTiles"]
        if tile.get("atlasId") == config["atlasId"]
    ]
    validate_atlas(
        atlas,
        atlas_tiles,
        int(config["expectedAtlasTileCount"]),
        str(config["planetId"]),
        str(config["settlementId"]),
    )

    planet = by_id(collections["planets"])[config["planetId"]]
    if planet.get("capitalSettlementId") != config["settlementId"]:
        raise ImportFailure("Koplin 3 does not reference Concordia as capital in locked canon.")
    if planet.get("worldAtlasId") != config["atlasId"]:
        raise ImportFailure("Koplin 3 does not reference the locked world atlas.")

    catalog = build_runtime_catalog(lock, manifest, config, collections)
    atlas_index = build_atlas_index(lock, atlas, atlas_tiles)

    catalog_bytes = write_canonical_json(output / "canon.catalog.json", catalog)
    atlas_bytes = write_canonical_json(output / "atlas.index.json", atlas_index)
    content_hash = hashlib.sha256(catalog_bytes + atlas_bytes).hexdigest()

    imported_id_set = {
        stable_id
        for ids in config["requiredIds"].values()
        for stable_id in ids
    }
    imported_id_set.update(str(tile["id"]) for tile in atlas_tiles)

    planet_reference_fields = (
        ("celestialBodyKinds", [planet.get("celestialBodyKindId")]),
        ("worldTypes", [planet.get("worldTypeId")]),
        ("atmosphereTypes", [planet.get("atmosphereTypeId")]),
        ("surfaceLandforms", planet.get("dominantLandformIds", [])),
        ("surfaceBiomes", planet.get("dominantBiomeIds", [])),
        ("surfaceHydrospheres", planet.get("dominantHydrosphereIds", [])),
        ("landscapeTilesets", [planet.get("landscapeTilesetId")]),
    )
    for _, values in planet_reference_fields:
        imported_id_set.update(
            str(value) for value in values if isinstance(value, str) and value
        )

    imported_ids = sorted(imported_id_set)
    generated_images = sum(1 for tile in atlas_tiles if (tile.get("image") or {}).get("generated"))
    provenance = {
        "formatVersion": 1,
        "repository": lock["repository"],
        "universeCommit": lock["commit"],
        "universeSchemaVersion": manifest["schemaVersion"],
        "universeContentVersion": manifest.get("contentVersion", ""),
        "importerVersion": lock["importerVersion"],
        "contentHashSha256": content_hash,
        "atlasTileCount": len(atlas_tiles),
        "atlasImagesGenerated": generated_images,
        "atlasImagesPending": len(atlas_tiles) - generated_images,
        "importedStableIds": imported_ids,
        "sourceFiles": sorted(
            {
                "data/manifest.json",
                *[
                    f"data/{name}"
                    for collection in required_names
                    for name in resolve_collection_files(manifest, collection)
                ],
            }
        ),
    }
    write_canonical_json(output / "canon.provenance.json", provenance)
    return provenance


def main(argv: Sequence[str] | None = None) -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--source", required=True, type=Path)
    parser.add_argument(
        "--output",
        type=Path,
        default=Path("MineITCityBuilder/Assets/Game/Generated/Resources/Canon"),
    )
    parser.add_argument(
        "--lock", type=Path, default=Path("config/universe.lock.json")
    )
    parser.add_argument(
        "--config", type=Path, default=Path("config/universe.import.json")
    )
    parser.add_argument("--no-git-verify", action="store_true")
    args = parser.parse_args(argv)

    try:
        provenance = run_import(
            args.source.resolve(),
            args.output.resolve(),
            args.lock.resolve(),
            args.config.resolve(),
            verify_git=not args.no_git_verify,
        )
    except ImportFailure as exc:
        print(f"Universe import failed: {exc}", file=sys.stderr)
        return 2

    print(
        "Universe import OK: "
        f"{provenance['atlasTileCount']} atlas tiles, "
        f"SHA {provenance['universeCommit']}, "
        f"content {provenance['contentHashSha256']}"
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
