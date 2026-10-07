# 27 — Initial Engineering Backlog

## Purpose

This is the ordered starting backlog. Detailed task definitions live in the subsystem documents. The backlog identifies critical path, dependencies and the first implementation slices.

## Epic 0 — Repository planning/tooling

1. IMP-QA-016 requirement parser.
2. IMP-QA-017 duplicate ID validation.
3. IMP-QA-018 prefix ownership validation.
4. IMP-QA-019 unowned requirement validation.
5. Add implementation plan link to root README.
6. Create ADR template.
7. Create benchmark threshold schema.

Depends on: plan merge.

## Epic 1 — Unity bootstrap

1. IMP-FND-001 create Unity 6.3 project.
2. IMP-FND-002 pin stable packages.
3. IMP-FND-003 Android IL2CPP/ARM64/API settings.
4. IMP-FND-004 URP/Vulkan.
5. IMP-FND-005 ECS/Burst smoke.
6. IMP-FND-008 UI Toolkit touch shell.
7. IMP-FND-010 build metadata.
8. IMP-FND-021 gitignore/gitattributes.
9. IMP-FND-022 asmdefs.
10. IMP-FND-023 namespaces.

Critical path to all code.

## Epic 2 — GitHub Actions proof

1. IMP-CI-001 workflow.
2. IMP-CI-002 Unity activation.
3. IMP-CI-003 blank APK.
4. IMP-CI-005 EditMode test.
5. IMP-CI-006 PlayMode smoke.
6. IMP-CI-007 version screen.
7. IMP-CI-008 artifact.
8. IMP-CI-009 logs.
9. physical install confirmation.

**Milestone:** first GitHub-built APK.

## Epic 3 — Core deterministic architecture

1. IMP-ARC-001..010.
2. IMP-ID-001..005.
3. IMP-SIM-001..010.
4. IMP-QA-001..003.
5. first headless checksum fixture.

**Gate A candidate.**

## Epic 4 — Save foundation

1. IMP-SAV-001 header.
2. IMP-SAV-002 sections.
3. IMP-SAV-003 binary codec.
4. IMP-SAV-004 checksums.
5. IMP-SAV-005 ID/string table.
6. IMP-SAV-006 scheduler/RNG.
7. IMP-SAV-008 atomic writer.
8. IMP-SAV-012 permanent v1 fixture.

Do this early, before domains proliferate.

## Epic 5 — Universe/canon pipeline

1. IMP-CAN-001 lock.
2. IMP-CAN-002 source resolver.
3. IMP-CAN-003 manifest.
4. IMP-CAN-005 required IDs.
5. IMP-CAN-006 atlas.
6. IMP-CAN-007 runtime catalogue.
7. IMP-CAN-008 provenance.
8. IMP-CAN-009 CI.

## Epic 6 — World/chunks/PVG/input

Parallelisable after core contracts stabilise:

- IMP-WLD-001..009;
- IMP-REN-001..007;
- IMP-INP-001..006;
- IMP-PERF-002..004.

Milestone: canonical tile blockout on Android.

## Epic 7 — Roads

- IMP-RD-001..011;
- touch construction integration;
- graph debug view;
- local rebuild profiler.

## Epic 8 — Parcels/buildings

- IMP-DEV-001..015;
- IMP-QA geometry fuzz;
- PVG construction states.

**Gate B.**

## Epic 9 — Population/workforce

- IMP-POP-001..013;
- B10K;
- conservation checks.

## Epic 10 — Mobility

- IMP-MOB-001..011;
- transit IMP-MOB-013..014;
- P3 routing prototype;
- B10K trip load.

**Gate C.**

## Epic 11 — Power/service

- IMP-CIV-001..009;
- IMP-CIV-016;
- utility/service UI trace.

**Gate D.**

## Epic 12 — Economy/freight/housing

- IMP-ECO-001..016;
- IMP-MOB-012;
- freight-starved fixture;
- affordability fixture.

## Epic 13 — Explainability/UI/save closeout

- IMP-UX-001..015;
- remaining vertical-slice save codecs;
- alerts/history basics;
- save/reload equivalence.

## Epic 14 — Vertical slice integration

Execute `23_VERTICAL_SLICE_EXECUTION_PLAN.md`.

**Gate E.**

## Epic 15 — Environment/resilience/governance

- remaining IMP-CIV;
- IMP-GOV-001..014.

## Epic 16 — Multi-tile + B50K

- remaining world streaming;
- HLOD/proxies;
- external connectors;
- B50K save/routing/perf.

## Epic 17 — B250K scale gate

- IMP-QA-007;
- IMP-PERF-007..012;
- sim LOD equivalence;
- thermal;
- save benchmark.

**Gate F.**

## Epic 18 — Accessibility/audio/localisation

Accessibility groundwork is earlier; close:

- IMP-ACC-001..015;
- controller/focus where required;
- audio LOD;
- localisation validation.

## Epic 19 — 100 km² canonical content

- atlas reconstruction tools;
- district grammars;
- 100 tiles;
- landmark archetypes;
- transit/road continuity;
- standard capture QA.

## Epic 20 — Long-run/release

- 100-year soak;
- 500-year stress;
- balance;
- migration fixtures;
- signed AAB/APK;
- release notes;
- Play internal test if enabled.

**Gate G + release candidate.**

## Issue template fields

Each implementation issue should contain:

- IMP task ID;
- KCB requirement IDs;
- scope;
- dependencies;
- data owned/read;
- save impact;
- deterministic impact;
- performance impact;
- UI/explainability impact;
- tests;
- acceptance;
- spec changes included;
- screenshots/benchmark evidence if relevant.

## Commit guidance

Commit messages may include task/requirement IDs, e.g.:

`feat(sim): add deterministic event scheduler [IMP-SIM-004] [KCB-TIME-030]`

Do not force every tiny refactor to list every related KCB ID, but feature PRs must provide traceability.

## Backlog completion

A task is not complete because code exists. It requires the Definition of Done in the next document.
