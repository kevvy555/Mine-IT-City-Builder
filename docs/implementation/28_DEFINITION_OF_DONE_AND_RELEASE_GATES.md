# 28 — Definition of Done and Release Gates

## Feature Definition of Done

A feature/task is **Complete** only when all applicable items are true.

### Design/spec

- intended behaviour matches current specification;
- changed behaviour updated specification in same branch/PR;
- relevant KCB requirements referenced;
- no contradictory obsolete normative text remains.

### Code/data

- production path is singular; no accidental v2/alternate implementation;
- stable IDs used where persistence requires;
- no inappropriate managed allocations/hot-loop GameObjects;
- domain ownership respected;
- error states explicit.

### Determinism

- update cadence documented;
- random stream named/versioned;
- ordering deterministic where required;
- render/frame-rate dependency absent.

### Persistence

- save impact assessed;
- codec/schema updated if needed;
- migration added if existing save shape changed;
- derived caches excluded where regenerable.

### Explainability

- meaningful output/shortage/failure exposes factors;
- UI uses authoritative factor data;
- relevant overlay/dashboard path exists or is scheduled in same milestone.

### Performance

- profiler checked;
- no obvious whole-city scan in frequent path;
- allocations bounded;
- benchmark updated if hot path changed;
- mobile-specific impact assessed.

### Rendered UI

- phase declares UI impact level UI-0..UI-4;
- affected screens/panels/overlays are listed;
- required `QA-UI-*` checks from `docs/qa/14_RENDERED_UI_PHASE_ACCEPTANCE.md` pass;
- runtime theme/font/resources required by the player build are packaged and validated;
- UI-1+ work has physical Android rendered evidence before phase close;
- UI-2+ work has physical touch/interaction evidence;
- UI-0 work still passes the current shell regression;
- no blank, missing, clipped or exception-covered critical UI remains.

### Accessibility/localisation

- touch target/interaction works;
- no colour/audio-only essential signal;
- user strings use localisation keys;
- accessible label/state supplied for UI.

### QA acceptance

- relevant `QA-*` cases identified;
- new/changed KCB requirements have QA mapping;
- required laboratory/causal/invariant scenarios updated;
- evidence produced when the feature is runnable;
- failed acceptance is fixed or explicitly returned to design/specification.

### Tests

- unit/system tests;
- integration fixture if cross-domain;
- invariant updated;
- deterministic reference updated intentionally;
- Android smoke if platform/presentation affected;
- rendered-UI regression at the phase's declared UI impact level.

### Documentation

- implementation doc/ADR updated when architecture changed;
- public/internal schema documented;
- spec kept current.

## Pull request gate

PR body includes:

- summary;
- IMP tasks;
- KCB requirements;
- spec changes;
- testing and QA IDs/evidence;
- benchmark/performance;
- save compatibility;
- UI impact level and UI test plan/result;
- screenshots/video when UI-1+ or otherwise visual;
- known deferred items.

Required checks green.

## Milestone gate evidence

Store:

- commit SHA;
- build version;
- benchmark report;
- test report;
- device model/OS for Android proof;
- screenshots where visual or required by the phase UI plan;
- acceptance checklist.

## Gate A — Simulation kernel

Required:

- deterministic scheduler;
- stable IDs;
- root seed/streams;
- save header;
- instrumentation;
- headless sim.

Reject if rendering is required to run simulation.

## Gate B — City geometry

Required:

- road spline;
- intersection;
- block;
- irregular parcels;
- zoning;
- PVG building;
- incremental rebuild;
- geometry fuzz.

Reject if normal edit rebuilds whole city.

## Gate C — Living district

Required:

- households;
- dwellings;
- jobs;
- activity plan;
- trips;
- road traffic;
- population conservation.

Reject if camera changes trip/population outcome.

## Gate D — Networked city

Required:

- transit;
- power;
- service;
- travel-time catchment;
- effective capacity;
- cause trace.

Reject if service is simple radius/stat bonus where routing should matter.

## Gate E — Canon/art identity

Required:

- locked Universe SHA;
- canonical tile 0,0;
- Concordia PVG palette/grammar;
- atlas continuity;
- canonical references;
- Android vertical slice.

Reject if game copied/forked canon manually.

## Gate F — Scale

Required B250K on named reference Android class:

- 30 FPS representative camera target;
- normal simulation speed;
- bounded routing backlog;
- memory budget;
- save/load budget;
- thermal protocol;
- LOD equivalence.

If failed, architecture changes before 100 km² content lock.

## Gate G — Production foundation

Required:

- save migration suite;
- 100-year soak;
- content validator;
- accessibility shell;
- localisation architecture;
- CI/release pipeline;
- crash/compatibility handling;
- first 100 km² production workflow proven.

## Gate QA — Final specification acceptance

After implementation Gates A–G, execute [docs/qa/13_FINAL_SPECIFICATION_ACCEPTANCE_GATE.md](../qa/13_FINAL_SPECIFICATION_ACCEPTANCE_GATE.md).

Gate QA requires, at minimum:

- complete KCB -> QA mapping;
- all non-deferred MUST requirements executed;
- automated correctness/invariants passing;
- save/migration/determinism acceptance;
- required 100-year stability suite;
- B250K Android evidence;
- mobile UX/accessibility acceptance;
- canon/content acceptance;
- zero open Blocker/Critical defects.

Code-complete is not specification-complete until Gate QA passes.

## Release candidate gate

In addition to Gate G:

- signed APK and AAB;
- clean install;
- upgrade from previous supported build;
- save compatibility verified;
- supported-device matrix smoke;
- no blocker/critical known bug;
- privacy/telemetry decision documented;
- accessibility release checklist;
- release notes;
- Universe/content versions recorded;
- reproducible tag build.

## Severity

Blocker:
- data loss;
- cannot install/start/load;
- deterministic corruption;
- save migration failure affecting supported saves;
- severe canon integrity issue.

Critical:
- core system unusable;
- major crash;
- city-wide invariant corruption;
- serious accessibility blocker for primary path.

Major:
- significant feature issue with workaround.

Minor:
- cosmetic/localised problem.

No release candidate with Blocker or unresolved Critical issue.

## Post-release compatibility

Any production save format version becomes a fixture.

Every subsequent release must:

- load directly; or
- migrate explicitly; or
- state support window and produce clear compatibility message.

Never silently reset a player's long-running city.

## Implementation start criterion

Implementation may begin after this plan is merged and the owner approves it.

The first implementation branch should be Phase 1 Unity/Android/CI proof—not gameplay content.
