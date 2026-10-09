# 21 — Testing, Benchmarks, Soak and Instrumentation

## Relationship to the QA acceptance catalogue

This document defines the **engineering test harness, benchmark infrastructure and instrumentation**.

The authoritative specification-acceptance strategy lives in [docs/qa/README.md](../qa/README.md).

Implementation tests created here must execute the QA cases defined there. In particular:

- laboratory fixtures come from `docs/qa/02_REFERENCE_LABORATORY_CITIES.md`;
- invariants come from `docs/qa/03_INVARIANTS_AND_CONSERVATION.md`;
- causal tests come from `docs/qa/04_CAUSAL_AND_DIRECTIONAL_TESTS.md`;
- cross-system scenarios come from `docs/qa/06_CROSS_SYSTEM_SCENARIOS.md`;
- save/determinism acceptance comes from `docs/qa/08_SAVE_MIGRATION_AND_DETERMINISM.md`;
- Android performance acceptance comes from `docs/qa/09_ANDROID_PERFORMANCE_AND_DEVICE_QA.md`;
- UX/accessibility acceptance comes from `docs/qa/10_MOBILE_UX_ACCESSIBILITY_AND_EXPLAINABILITY.md`;
- rendered UI phase acceptance comes from `docs/qa/14_RENDERED_UI_PHASE_ACCEPTANCE.md`;
- long-run acceptance comes from `docs/qa/11_LONG_RUN_STABILITY_AND_BALANCE.md`.

A test harness capability without a linked QA acceptance case does not by itself prove a KCB requirement.

## Test pyramid

### Pure unit tests

For:

- fixed-point maths;
- route cost;
- capacity formulas;
- polygon rules;
- IDs;
- save codecs;
- migrations;
- policy precedence;
- random streams.

These should run without Unity scene loading where possible.

### ECS/system tests

Create small worlds and execute deterministic systems.

### Integration fixtures

Load deterministic synthetic/canonical fixtures and assert metric ranges/invariants.

### Android smoke

Build/install/start/load basic scene and exercise lifecycle.

For any phase classified UI-1 or higher, Android smoke also verifies rendered UI, not merely process startup:

- required text glyphs are visible;
- buttons/controls visibly render;
- runtime theme/font assets are present;
- safe-area/layout is valid;
- no blank panel or exception overlay appears;
- representative screenshot evidence is retained.

The mandatory per-phase procedure is defined in `docs/qa/14_RENDERED_UI_PHASE_ACCEPTANCE.md`.

### Long-run headless

Run years without rendering.

## Required deterministic fixtures

1. balanced district;
2. disconnected suburb;
3. congested corridor;
4. transit-rich centre;
5. freight-starved industrial;
6. utility bottleneck;
7. housing affordability crisis;
8. understaffed service network;
9. flood/drainage event;
10. fiscal stress;
11. long-term ageing city;
12. content/mod compatibility;
13. canonical Federal Forum vertical slice;
14. bridge-closure reroute;
15. interrupted save.

Fixture IDs are stable.

## Invariants

Development/test builds evaluate configurable invariant groups:

Population:
- no negative counts;
- population ledger reconciles;
- household membership valid.

Housing/jobs:
- dwelling occupancy reconciles;
- job occupancy valid.

Economy:
- money ledger reconciles;
- inventory nonnegative;
- project commitments valid.

Networks:
- graph references valid;
- route modes legal;
- utility flow within capacity.

World:
- parcel polygons valid/non-overlapping;
- building access valid;
- stable IDs unique.

Persistence:
- save references resolve;
- event order stable.

## Deterministic checksum

At fixed reporting cadence, hash canonical metrics/state summaries:

- population by state;
- households;
- money totals;
- inventory totals;
- network flow totals;
- RNG stream checkpoints;
- scheduled event counts;
- building counts/state.

Checksums make drift easy to locate.

## Golden benchmarks

Maintain seeded saves/world setups:

- B10K;
- B50K;
- B250K;
- B1M-stretch.

Each records:

- simulation throughput;
- main/job CPU;
- memory;
- route request latency;
- graph size;
- loaded render instances;
- save size/time;
- economic/population reference ranges.

Thresholds stored in `config/benchmark-thresholds.json`.

## CI benchmark policy

Fast PR:

- B10K;
- correctness/invariants;
- short deterministic run.

Milestone/main/manual:

- B50K;
- selected performance checks.

Scale gate:

- B250K on reference Android hardware;
- headless CI comparison;
- thermal test.

B1M is manual/stretch until proven affordable.

## Long-run soak

Headless:

- 1 year every relevant PR;
- 10 years main/nightly;
- 100 years milestone/nightly;
- 500+ year stress manual.

Check:

- population runaway/collapse;
- money drift;
- orphan entities;
- event queue growth;
- history buffer growth;
- save growth;
- ID exhaustion;
- route cache leakage;
- memory leak.

## Performance instrumentation

Counters:

- sim phase times;
- systems skipped/batched;
- ECS entity counts;
- chunk states;
- graph nodes/edges;
- route requests/cache hit/expansions;
- active trips;
- traffic queues;
- utility solver time;
- parcel rebuild count;
- render instances/batches;
- memory;
- save snapshot/write;
- UI read-model update.

Provide in-game developer overlay and JSON export.

## Regression thresholds

Performance comparison:

- absolute hard budget;
- percentage regression threshold;
- noise floor.

A statistically noisy 1% change should not fail CI. A repeatable 15% pathfinding slowdown should.

## Fuzzing

Generate random:

- road edits;
- polygon blocks;
- zoning changes;
- demolitions;
- policy toggles;
- save/load timing;
- network closures.

Run invariant checks.

## Visual regression

Standard capture positions for canonical tile:

- day;
- night;
- traffic overlay;
- utility overlay.

Use screenshots for human/automated broad regression, not pixel-perfect failure where GPU variance makes it fragile.

## Android lifecycle tests

Manual/automated where possible:

- background/resume;
- lock/unlock;
- incoming memory pressure;
- rotate request;
- suspend during autosave;
- kill/relaunch after completed save.

## Tasks

- IMP-QA-001 — Create test assembly structure.
- IMP-QA-002 — Implement invariant framework.
- IMP-QA-003 — Implement deterministic checksum.
- IMP-QA-004 — Create required fixtures.
- IMP-QA-005 — Create B10K generator.
- IMP-QA-006 — Create B50K generator.
- IMP-QA-007 — Create B250K generator.
- IMP-QA-008 — Create B1M stretch generator.
- IMP-QA-009 — Add benchmark report JSON.
- IMP-QA-010 — Add developer profiler overlay.
- IMP-QA-011 — Add 1/10/100-year runners.
- IMP-QA-012 — Add geometry/network fuzzers.
- IMP-QA-013 — Add Android lifecycle checklist/test.
- IMP-QA-014 — Add screenshot baseline tooling.
- IMP-QA-015 — Add CI threshold comparison.

## Exit criteria

- deliberate broken ID fails validation;
- deliberate pathfinding slowdown appears in report;
- save/reload checksum stays within defined exact/tolerance contract;
- 100-year soak passes without invariant failure before production lock;
- B250K becomes a mandatory Gate F result.
