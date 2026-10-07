# 33 — Implementation Contracts and Handoff

## Purpose

This document defines what the next phase — implementation planning — must produce. It is intentionally not an implementation plan itself.

## Resolved implementation baseline

The implementation-planning phase has now frozen the following v1 foundation decisions.

| Decision | Locked v1 baseline |
|---|---|
| Target platform | Android phones and tablets, landscape-first |
| Minimum OS | Android 10 / API 29 |
| Store target | Android API 36 |
| CPU architecture | ARM64 |
| Engine | Unity 6.3 LTS |
| Language | C# |
| Simulation stack | Unity Entities/DOTS + Burst + Job System |
| Rendering | Universal Render Pipeline (URP) |
| Graphics API | Vulkan primary; OpenGL ES fallback where supported |
| UI | Unity UI Toolkit with touch-first interaction |
| Asset/content loading | Addressables/content catalogues |
| Build backend | IL2CPP |
| Baseline frame target | 30 FPS gameplay; 60 FPS optional on capable devices |
| Primary population scale | 250,000 persistent citizens |
| Stretch population scale | 1,000,000 persistent/aggregated citizens subject to benchmark gates |
| Detailed v1 city extent | 100 canonical 1 km² atlas tiles, streamed rather than resident |
| Runtime spatial subdivision | 250 m x 250 m chunks; 16 runtime chunks per canonical atlas tile |
| Simulation/render relationship | Authoritative simulation fully separated from visual representation |
| Traffic/pathfinding | Purpose-built multimodal graphs; no Rigidbody/NavMesh dependency for authoritative city traffic |
| Initial visual production | Primitive Visual Grammar using instanced blocks, cylinders, spheres, capsules, wedges/planes and palette materials |
| Future visual upgrade | Visual archetype IDs allow high-quality meshes/materials to replace primitive representations without simulation changes |
| Save technology | Explicitly versioned chunked binary container with deterministic, reflection-free per-domain codecs |
| Canon integration | Pinned MineIT-Universe commit imported and validated at build/content-generation time |
| Networking | Offline single-player v1 |
| Modding | Data-driven internal extension points; public arbitrary code mods deferred beyond v1 |
| Campaign year | Year 5300 baseline |
| Player role | Concordia Metropolitan Steward (game-facing role, not new Universe canon) |

KCB-HAND-050 — These decisions are normative for v1 implementation unless deliberately revised in the specification and implementation plan together.

KCB-HAND-051 — Primitive rendering is an intentional gameplay-ready presentation mode and MUST remain available as a low-cost graphics mode after higher-fidelity assets are introduced.

KCB-HAND-052 — High-quality art replacement MUST bind through stable visual/content archetype IDs and MUST NOT require simulation-state migration merely because a render asset changes.

KCB-HAND-053 — The 250,000-citizen target MUST be proven on the agreed Android reference device class before content-heavy production is considered scale-safe.

KCB-HAND-054 — The one-million-citizen target is a stretch benchmark and MAY use more aggressive simulation aggregation while preserving authoritative totals and expected outcomes.


## Decisions the implementation plan must freeze

KCB-HAND-001 — Target platforms and minimum hardware.
KCB-HAND-002 — Engine/framework and version.
KCB-HAND-003 — Rendering pipeline.
KCB-HAND-004 — Guaranteed population/city scale.
KCB-HAND-005 — Fixed simulation step and domain cadences.
KCB-HAND-006 — Data-oriented entity strategy.
KCB-HAND-007 — Pathfinding/network library/approach.
KCB-HAND-008 — Procedural parcel/building technology.
KCB-HAND-009 — Save format/serialization technology.
KCB-HAND-010 — Modding scope for v1.
KCB-HAND-011 — Online/offline requirements.
KCB-HAND-012 — Canon import packaging strategy.
KCB-HAND-013 — Initial campaign start year.
KCB-HAND-014 — Initial detailed map extent.
KCB-HAND-015 — Accessibility target matrix.
KCB-HAND-016 — Performance budgets.

## Required architecture outputs

The implementation plan MUST include:
- context/system diagram;
- domain dependency graph;
- simulation update-order diagram;
- entity/component/data model;
- network graph model;
- spatial/chunk model;
- save schema outline;
- canon import pipeline;
- render representation pipeline;
- LOD conversion rules;
- mod/content registration model;
- instrumentation/telemetry architecture.

## Required backlog structure

Every implementation epic/task must reference one or more KCB requirement IDs.

Suggested epics:
1. repository/build/CI;
2. Universe import;
3. deterministic simulation kernel;
4. world/atlas/chunk streaming;
5. road/network editor;
6. parcel/zoning/development;
7. building procedural renderer;
8. utilities;
9. population/households;
10. jobs/education;
11. economy/industry;
12. routing/traffic;
13. transit;
14. services;
15. environment/resilience;
16. governance/policies;
17. UI/info views;
18. art/audio;
19. save/migrations;
20. accessibility/localisation;
21. modding;
22. tests/performance;
23. content/scenarios.

## Recommended vertical slice

The first implementation slice SHOULD prove the architecture with one canonical 1 km² tile plus immediate network stubs, preferably Federal Forum or a less asset-heavy adjacent mixed district if Federal Forum’s hero art would delay systems work.

The slice must include:

### World
- imported canonical tile ID/metadata;
- terrain;
- road/greenway/transit anchors;
- chunk streaming boundary.

### Build tools
- one road type with curve;
- pedestrian path;
- basic zoning;
- blueprint/undo.

### Development
- block/parcel generation;
- at least residential, mixed-use and office/research building grammar;
- construction state.

### Population/economy
- households;
- jobs;
- commuting;
- simple income/rent;
- building occupancy.

### Mobility
- walking;
- road vehicle trips;
- one transit line/mode;
- pathfinding;
- traffic volume/flow.

### Utilities/service
- power network;
- one healthcare or education service;
- effective capacity calculation.

### UI
- selection panel;
- causal factor display;
- traffic overlay;
- utility overlay;
- housing/job overview.

### Technical
- deterministic seed;
- save/load;
- simulation/render LOD boundary;
- profiling counters;
- 10k+ population benchmark path.

KCB-HAND-020 — Vertical slice is rejected if it uses throwaway architecture that bypasses stable IDs, save versioning or simulation/render separation.

## Milestone gates

### Gate A — Simulation kernel
Prove deterministic scheduling, data model, save header and instrumentation.

### Gate B — City geometry
Prove road -> block -> parcel -> generated building.

### Gate C — Living district
Prove households -> jobs -> trips -> buildings.

### Gate D — Networked city
Prove transit, utility and service causality.

### Gate E — Canon/art identity
Prove a canonical tile looks recognisably Koplin with scalable rendering.

### Gate F — Scale
Prove agreed population/map benchmark with profiling.

### Gate G — Production foundation
Prove migrations, content validation, accessibility shell and CI.

## Traceability matrix

The implementation plan should create a machine-readable or Markdown table:
Requirement ID | Owner epic | Implementation task | Test | Status

KCB-HAND-030 — No MUST requirement may remain without an owner or explicit defer decision.

## Remaining deferred product decisions

The following are deliberately outside the v1 implementation baseline unless promoted by a later specification change:
- multiplayer/networked city play;
- full explorable building interiors;
- detailed election simulation beyond mandate/governance systems;
- planetary globe gameplay;
- complete macroeconomic inflation modelling;
- public arbitrary-code mod execution on Android.

These items require an explicit future specification decision before implementation.

## Risk register seeds

High-risk areas:
- route/pathfinding scale;
- deterministic multi-threading;
- procedural building quality;
- save migrations;
- aggregate/detailed simulation equivalence;
- irregular parcel geometry;
- agent/render count;
- UI explainability;
- art production volume;
- canonical atlas reconstruction;
- long-run economy stability.

KCB-HAND-040 — Implementation plan assigns a proof/prototype to every high-risk area before content-heavy production depends on it.

## Definition of implementation-plan ready

The specification is ready when reviewers can answer:
- what does the system do?
- what data does it own?
- what other systems does it consume/provide?
- what can the player see/control?
- what must remain canon-consistent?
- how can it degrade for performance?
- how is it saved?
- how is it tested?

This catalogue is designed to make those answers explicit.
