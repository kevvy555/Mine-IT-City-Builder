import sys
import unittest
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))

from universe_import import ImportFailure, validate_atlas, validate_required


class UniverseImporterValidationTests(unittest.TestCase):
    def test_missing_required_id_fails(self):
        collections = {"planets": [{"id": "planet-other"}]}
        with self.assertRaises(ImportFailure):
            validate_required(collections, {"planets": ["planet-koplin-prime"]})

    def test_duplicate_atlas_coordinates_fail(self):
        atlas = {
            "id": "world-atlas-koplin-3",
            "planetId": "planet-koplin-prime",
            "originSettlementId": "settlement-concordia",
            "tileSizeKm": 1,
        }
        tiles = [
            self.tile("a", 0, 0),
            self.tile("b", 0, 0),
        ]
        with self.assertRaises(ImportFailure):
            validate_atlas(
                atlas, tiles, 2, "planet-koplin-prime", "settlement-concordia"
            )

    def test_broken_neighbor_reference_fails(self):
        atlas = {
            "id": "world-atlas-koplin-3",
            "planetId": "planet-koplin-prime",
            "originSettlementId": "settlement-concordia",
            "tileSizeKm": 1,
        }
        tile = self.tile("a", 0, 0)
        tile["neighborTileIds"] = ["missing"]
        with self.assertRaises(ImportFailure):
            validate_atlas(
                atlas, [tile], 1, "planet-koplin-prime", "settlement-concordia"
            )

    @staticmethod
    def tile(stable_id, x, y):
        return {
            "id": stable_id,
            "atlasId": "world-atlas-koplin-3",
            "planetId": "planet-koplin-prime",
            "x": x,
            "y": y,
            "edgeContinuity": {
                "north": "",
                "east": "",
                "south": "",
                "west": "",
            },
            "neighborTileIds": [],
        }


if __name__ == "__main__":
    unittest.main()
