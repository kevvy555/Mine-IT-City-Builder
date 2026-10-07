# 28 — Save Data, Modding and Data Pipeline

## Data ownership

There are four distinct data classes.

1. **Universe canon** — immutable reference input from MineIT-Universe.
2. **Game authored data** — building archetypes, recipes, tuning, scenarios, visual grammars and other game-specific content.
3. **Procedural deterministic data** — generated from seed/rule versions and regenerable where safe.
4. **Save state** — mutable player/simulation state.

KCB-DATA-001 — These classes MUST be stored and versioned separately enough that no save can overwrite canon.

KCB-DATA-002 — The implementation plan MUST define the source repository and release process for game-authored content.

## Canon import

Import pipeline:
1. identify compatible Universe release/commit;
2. load schema;
3. validate required IDs;
4. transform canonical records into game reference format;
5. cache/package required subset;
6. record source commit/version in build metadata;
7. run referential integrity tests.

KCB-DATA-010 — Every packaged build MUST be able to report the Universe source commit/version used.

KCB-DATA-011 — Imported data MUST retain canonical IDs.

KCB-DATA-012 — A changed display name must not create a new identity if canonical ID is unchanged.

## Atlas import

KCB-DATA-020 — Atlas tile ID, coordinate, districtName, description, adjacency metadata and reference-art key are imported.

KCB-DATA-021 — Interactive geometry is maintained as game data keyed to atlas tile IDs.

KCB-DATA-022 — When Universe atlas art changes, import tooling MUST flag affected tiles for visual QA rather than silently overwriting player saves.

## Save format

Top-level save header:
- saveFormatVersion;
- gameVersion;
- contentVersion;
- universeSourceVersion;
- rootSeed;
- scenarioId;
- simulationTimestamp;
- enabledMods;
- feature flags;
- integrity/checksum metadata.

State partitions:
- world/chunks;
- network graphs;
- parcels/buildings;
- citizens/households;
- organisations/economy;
- services;
- utilities;
- transit;
- environment;
- government/policies;
- events/history;
- UI/user metadata separate where possible.

KCB-DATA-030 — Save file MUST have an explicit schema/version from version 1.

KCB-DATA-031 — Large state SHOULD be chunked so future partial/streaming load is possible.

KCB-DATA-032 — User preferences SHOULD NOT require rewriting the whole city save.

## Stable IDs

KCB-DATA-040 — Runtime entity IDs persisted to save cannot be raw memory pointers or transient array indices.

KCB-DATA-041 — IDs need namespace/type safety or equivalent protection against accidental cross-type references.

KCB-DATA-042 — Deleted persistent entities MAY leave tombstones/history references if other records cite them.

## Save transactions

KCB-DATA-050 — Save writing MUST be atomic from the user’s perspective.
KCB-DATA-051 — Interrupted save must not destroy the last known-good save.
KCB-DATA-052 — Autosave uses rotation slots.
KCB-DATA-053 — Manual saves are not silently overwritten by autosave.

## Migration

Every schema change defines:
- source version;
- destination version;
- transformation;
- validation;
- rollback/failure message.

KCB-DATA-060 — Migration code is tested with fixture saves from supported historical versions.

KCB-DATA-061 — Removing a content ID requires mapping, retirement or graceful missing-content behaviour.

## Deterministic regeneration

Terrain dressing/HLOD/caches MAY be omitted from saves if safely regenerated.

KCB-DATA-070 — Only derived state may be regenerated. Authoritative simulation state cannot be reconstructed approximately after load.

## Mod/content extension model

Mod classes:
- data-only content;
- visual assets;
- scenarios;
- simulation systems;
- UI extensions.

KCB-DATA-080 — Base IDs are namespaced/reserved.
KCB-DATA-081 — Mods use their own namespaces.
KCB-DATA-082 — Duplicate IDs fail validation loudly.
KCB-DATA-083 — Save records mod identifiers and versions.
KCB-DATA-084 — Missing required mod on load produces a clear compatibility screen.

## Extension points

Stable extension APIs SHOULD exist for:
- registering content definitions;
- adding simulation processors/systems;
- subscribing/publishing domain events;
- adding overlays/panels;
- adding policies/scenarios.

KCB-DATA-090 — Mods SHOULD register systems rather than patch arbitrary internals where possible.

KCB-DATA-091 — Extension API has its own compatibility version.

## Data validation

Build-time validation:
- unique IDs;
- referential integrity;
- range constraints;
- no circular dependencies where prohibited;
- localisation keys;
- asset references;
- recipe conservation rules;
- zoning/building compatibility;
- save migration registration.

KCB-DATA-100 — Invalid authored content fails CI rather than becoming runtime mystery behaviour.

## Security and safety

KCB-DATA-110 — Untrusted mod code/content must follow platform sandbox/security capabilities where possible.
KCB-DATA-111 — Save parser validates lengths/counts before allocating large memory.
KCB-DATA-112 — Importers never execute content merely because it appears in a data file.

## Export/debug

Laboratory/developer tools SHOULD export:
- city metrics CSV/JSON;
- network diagnostics;
- entity snapshot;
- event trace;
- performance profile;
- deterministic input log.

KCB-DATA-120 — Debug export excludes unnecessary personal platform/account identifiers.

## Acceptance criteria

- Same save loads after a minor content display-name change.
- Missing canonical ID fails import validation.
- Interrupted autosave preserves previous slot.
- A migration fixture upgrades deterministically.
- Removing a mod gives a precise dependency error.
- Canon import reports source commit.
