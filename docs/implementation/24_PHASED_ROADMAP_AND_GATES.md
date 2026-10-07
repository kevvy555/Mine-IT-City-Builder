# 24 — Phased Roadmap and Milestone Gates

## Roadmap rule

Phases are dependency/risk ordered, not calendar promises. Do not assign duration before implementation velocity and CI build time are measured.

Each phase gets its own feature branch or coherent branch series from latest `main`.

## QA execution rule

The QA catalogue at [docs/qa/README.md](../qa/README.md) is the acceptance oracle for this roadmap.

Each implementation phase must:

1. identify the KCB requirements being implemented;
2. identify the linked QA IDs and fixtures;
3. implement/enable those QA cases as the feature becomes runnable;
4. retain evidence for the phase gate;
5. leave no required KCB item at `Implemented` when its gate requires `Verified`.

QA is executed continuously through the phases and comprehensively again at the final **Gate QA — Specification Acceptance**.

## Phase 0 — Planning integration

Scope:

- merge approved technology/spec changes;
- merge implementation catalogue;
- requirement ownership validator design;
- QA acceptance catalogue integration and KCB -> QA coverage mapping;
- create initial GitHub issues/milestones if desired.

Exit:

- plan/spec consistent;
- no unresolved foundation decision blocks bootstrap.

## Phase 1 — Unity + Android + CI proof

Scope:

- Unity 6.3 LTS project;
- package pinning;
- URP;
- Entities/Burst;
- UI Toolkit/Input System;
- GameCI;
- Android IL2CPP ARM64;
- primitive scene;
- Actions APK.

Exit:

- installable GitHub-built APK.

**Stop condition:** do not start large gameplay work if CI/licensing/build route is unreliable.

## Phase 2 — Core architecture, IDs, time, save v1

Scope:

- asmdefs/domain boundaries;
- stable IDs;
- deterministic clock/scheduler/RNG;
- command/event contracts;
- basic save container;
- diagnostics.

Gate A acceptance:

- deterministic headless run;
- save header;
- stable event order;
- simulation runs with rendering disabled.

## Phase 3 — Universe import + canonical world

Scope:

- universe.lock;
- importer;
- provenance;
- atlas tile import;
- coordinates;
- runtime chunks;
- basic terrain.

Exit:

- tile 0,0 loads from locked canon;
- broken canon reference fails CI.

## Phase 4 — PVG renderer + touch shell

Scope:

- primitive meshes/materials;
- visual archetypes;
- instancing;
- chunk rendering;
- camera/touch;
- selection/context shell;
- performance HUD.

Exit:

- attractive deterministic Federal Forum blockout on Android.

## Phase 5 — Roads and graph foundation

Scope:

- rights of way;
- spline tools;
- cross-sections;
- intersections;
- pedestrian/road graph derivation;
- local rebuild;
- construction preview.

Exit:

- editable curved network with stable identity.

## Phase 6 — Blocks, parcels, zoning, buildings

Scope:

- Clipper2 proof;
- blocks;
- parcels;
- zoning;
- development demand;
- building programme;
- PVG buildings;
- construction.

**Gate B — City geometry**

Acceptance:

- road -> block -> parcel -> generated building;
- irregular parcels;
- incremental rebuild.

## Phase 7 — Population, housing, jobs and education foundation

Scope:

- citizens/households;
- dwellings;
- jobs;
- education;
- matching;
- activity planning;
- wellbeing factors.

Exit:

- 10k living district fixture.

## Phase 8 — Mobility, traffic and transit

Scope:

- hierarchical routing;
- route cache;
- trips;
- link traffic queues;
- visible projection;
- one transit mode.

**Gate C — Living district**

Acceptance:

- household -> job -> trip -> arrival;
- congestion affects route/travel;
- camera does not change trip totals.

## Phase 9 — Power + service causality

Scope:

- utility graph;
- power;
- effective capacity;
- service facility;
- travel-time catchment;
- causal trace.

**Gate D — Networked city**

Acceptance:

- congestion/power/staffing affect delivered service;
- UI traces causes.

## Phase 10 — Economy, housing market, industry, freight

Scope:

- ledgers;
- household budgets;
- land value;
- affordability;
- business;
- recipes/inventory;
- freight/loading;
- external market;
- construction market.

Exit:

- closed, traceable vertical economy.

## Phase 11 — UX/overlays/dashboard/save completion

Scope:

- cause graph;
- overlays;
- dashboard;
- charts;
- alerts;
- search;
- complete vertical-slice save codecs/migrations.

## Phase 12 — Federal Forum vertical slice integration

Scope:

- complete `23_VERTICAL_SLICE_EXECUTION_PLAN.md`;
- PVG polish;
- canonical layout/identity;
- device test.

**Gate E — Canon/art identity**

Acceptance:

- canonical tile recognisable;
- end-to-end gameplay demonstration passes.

## Phase 13 — Environment, resilience, broader services

Scope:

- pollution;
- weather;
- drainage/flood;
- incidents;
- maintenance;
- health/emergency/waste/other service breadth.

## Phase 14 — Governance, policy, tech, events, progression

Scope:

- districts;
- policy;
- mandate;
- research programmes;
- events/news;
- scenarios/history.

## Phase 15 — Multi-tile city and 50k scale

Scope:

- tile-to-tile network seams;
- multiple districts;
- proxy/HLOD;
- external connectors;
- B50K.

Exit:

- stable multi-tile city;
- no whole-city rebuild path.

## Phase 16 — 250k scale hardening

Scope:

- B250K;
- memory layout;
- route/cache optimisation;
- sim LOD;
- render LOD;
- save/load;
- thermal adaptation.

**Gate F — Scale**

No later content-production lock if Gate F fails.

## Phase 17 — Accessibility/localisation/audio production

Some accessibility groundwork starts earlier. This phase closes full requirement set:

- TalkBack/focus;
- scaling/contrast;
- input remap/controller;
- localisation architecture;
- audio/music/ambience;
- settings.

## Phase 18 — First 100 km² content buildout

Scope:

- interactive atlas reconstruction;
- roads/transit/green-blue anchors;
- district grammars;
- landmarks/PVG or HQ replacements;
- scenarios;
- visual QA captures.

Content production uses proven tools, not manual one-off scenes.

## Phase 19 — Long-run balance and production foundation

Scope:

- 100-year soak;
- 500-year stress;
- economy tuning;
- alternative strategy viability;
- save migrations;
- content validation;
- accessibility regression;
- crash/error handling.

**Gate G — Production foundation**

## Phase 20 — Android release hardening

Scope:

- signed APK/AAB;
- release workflow;
- Play Internal Testing if enabled;
- privacy/telemetry decision;
- store assets;
- compatibility/device testing;
- upgrade/migration test.

## Milestone gates summary

| Gate | Proof |
|---|---|
| A | deterministic simulation kernel |
| B | road -> block -> parcel -> building |
| C | households -> jobs -> trips |
| D | transit/utility/service causality |
| E | canonical Koplin visual identity |
| F | 250k Android scale |
| G | migrations/content/accessibility/CI production readiness |
| QA | full specification acceptance against `docs/qa/` |

## Gate discipline

A gate may be:

- PASS;
- PASS WITH EXPLICIT FOLLOW-UP;
- FAIL.

“Looks good” is not a gate result. Every gate has stored test/benchmark evidence and commit SHA.

A failed gate blocks phases whose risk depends on it.


## Gate QA — Specification Acceptance

After Gates A–G and before production release, execute the final acceptance process in [docs/qa/13_FINAL_SPECIFICATION_ACCEPTANCE_GATE.md](../qa/13_FINAL_SPECIFICATION_ACCEPTANCE_GATE.md).

Gate QA answers whether the finished implementation actually satisfies the complete approved specification. It requires full requirement coverage, automated correctness, persistence/migration, long-run, Android/device, UX/accessibility and canon evidence.
