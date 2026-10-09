# Phase 3 — Canonical Universe and World Evidence

**Status:** Candidate — canon/Unity/Android build pass; physical rendered-UI acceptance FAILED on first candidate and re-test is required  
**Implementation branch:** `feature/phase-3-universe-canonical-world`  
**Tested executable SHA:** `a20a8f1e8ed9537710d255de9b332bc801799aa1`  
**Locked Universe SHA:** `2a3251ba439a2108f0bf03e46eda02343e518925`

## Canon source

Phase 3 consumes an exact immutable MineIT-Universe commit rather than a moving branch.

The locked snapshot contains:

- `planet-koplin-prime` — Koplin 3;
- `settlement-concordia` — Concordia;
- `world-atlas-koplin-3`;
- 100 canonical 1 km x 1 km atlas tiles;
- Federal Forum at canonical coordinate `(0,0)`;
- the three canonical Koplin peoples;
- Commonwealth government and currency identities.

The exact locked Universe SHA independently passed the MineIT-Universe validation workflow in run `37741528451`.

## Import evidence

City Builder workflow run `37885477937` validates the lock and importer before Unity executes.

Passing checks:

- exact 40-character Universe SHA lock;
- exact git checkout HEAD equals the lock;
- Universe schema compatibility = 12;
- required canonical IDs resolve;
- collection shards are merged before identity validation;
- duplicate stable IDs fail;
- atlas contains exactly 100 unique coordinates;
- atlas tile IDs, planet IDs and atlas IDs reconcile;
- Federal Forum `(0,0)` exists;
- neighbour references exist, are reciprocal and orthogonally adjacent;
- edge-continuity metadata exists on every tile;
- Koplin 3 links to Concordia and `world-atlas-koplin-3`;
- deliberately broken required IDs, duplicate coordinates and broken neighbours fail importer tests;
- two imports of the same locked source are byte-for-byte identical.

Generated canon content hash:

`64d2c982485be747a2d114b86acad11a02f23b6abf59e51694ac460798eb4c75`

Generated atlas art state at the locked snapshot:

- 50 generated tile images;
- 50 pending tile images.

Incomplete art is not a Phase 3 blocker: canonical metadata and interactive world identity are imported independently from visual replacement/production work.

## Runtime evidence

The runtime consumes generated City Builder resources only; simulation/runtime assemblies do not parse raw Universe JSON.

Implemented runtime contracts include:

- stable `AtlasCoordinate`;
- 4 x 4 deterministic subdivision per canonical tile;
- 16 stable 250 m runtime chunks per 1 km tile;
- deterministic integer world origins supporting negative and positive atlas coordinates;
- separate simulation and render chunk states;
- deterministic basic terrain semantics keyed from canonical tile/chunk identity;
- canonical atlas lookups by stable ID and coordinate;
- Federal Forum origin tile materialisation.

The Android bootstrap loads the generated canon and reports:

- Koplin 3;
- Concordia;
- Federal Forum `(0,0)`;
- 100 canonical atlas tiles;
- 16 origin runtime chunks;
- Universe commit;
- generated canon content hash.

Build metadata also embeds the Universe commit and canon content hash.

## Automated Unity evidence

City Builder workflow run `37885477937`:

- 28 tests executed;
- 28 passed;
- 0 failed;
- 0 skipped/inconclusive.

Phase 3-specific cases cover:

- locked Koplin 3 / Concordia / Federal Forum load;
- exact locked Universe SHA;
- deterministic provenance hash;
- 100 unique runtime atlas coordinates;
- exactly 16 unique chunks per atlas tile;
- stable chunk origins across negative and positive atlas coordinates;
- deterministic basic terrain;
- chunk registry identity;
- independent render/simulation chunk state.

## Requirement acceptance

| Requirement | Phase 3 evidence |
|---|---|
| KCB-CAN-001 | canon remains sourced from MineIT-Universe and locked by immutable SHA |
| KCB-CAN-030 | Federal Forum retained as Concordia origin |
| KCB-CAN-090..093 | canonical atlas kept separate from mutable game state and imported by stable identity |
| KCB-MAP-001..005 | stable coordinates and deterministic 4x4/250m chunk identity |
| KCB-MAP-010 | district, zone, ring, description and image reference imported |
| KCB-MAP-011..013 | edge metadata retained; art remains reference rather than collision geometry |
| KCB-DATA-010 | APK/build metadata records Universe source SHA/hash |
| KCB-DATA-011..012 | canonical IDs retained independently of display name |
| KCB-DATA-020 | tile identity, coordinates, district/description/adjacency/art references imported |
| KCB-DATA-100 | malformed/broken canon fails CI |
| KCB-ARCH-040 | chunk registry establishes shared spatial partition foundation |

QA coverage: `QA-CAN-001`, `QA-CAN-002`, `QA-CAN-006`, plus deterministic importer validation.

## Rendered UI acceptance defect

The first physical Android Phase 3 candidate booted and rendered the 3D city, but the top-left diagnostic/canon card was blank.

Observed:
- card background rendered;
- labels/button content did not render;
- pan/zoom/rotate continued to work.

This means the executable passed data, hierarchy and packaging tests but failed rendered-UI acceptance.

Root cause identified:
- runtime-created UI Toolkit `PanelSettings` did not reference a packaged runtime `ThemeStyleSheet`;
- the plain card background could render while text/control styling/font resources were unavailable.

Regression/process response:
- packaged runtime theme added;
- automated theme/content contract added;
- `docs/qa/14_RENDERED_UI_PHASE_ACCEPTANCE.md` added;
- every implementation phase now has an explicit UI plan;
- physical Android rendered acceptance is mandatory for UI-1+ phases.

See `PHASE3_UI_DEVICE_ACCEPTANCE.md`.

## Remaining before Phase 3 PASS

- replacement APK must pass the Phase 3 physical rendered-UI checklist;
- PR-specific exact-head checks must pass;
- merge SHA must be recorded after integration.
