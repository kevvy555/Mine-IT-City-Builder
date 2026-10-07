# 33 — Implementation Contracts and Handoff

## Purpose

This document defines what the next phase — implementation planning — must produce. It is intentionally not an implementation plan itself.

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

## Open decisions — intentionally deferred

These are not missing specification by accident:
- engine;
- PC-only versus PC/mobile/other;
- exact Concordia population;
- exact map size at v1;
- exact player civic title;
- exact campaign year;
- multiplayer;
- full interior scenes;
- detailed election system;
- planetary globe gameplay;
- complete economic inflation model.

Each must be resolved or explicitly marked post-v1 during implementation planning.

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
