# 23 — Vertical Slice Execution Plan

## Slice objective

Build one canonical, installable, saveable, living 1 km² Concordia tile that proves the production architecture end-to-end.

Use **Federal Forum tile (0,0)** as the preferred slice because Primitive Visual Grammar removes the previous dependency on finished hero art. If imported atlas data shows a technical blocker, an adjacent canonical mixed district may temporarily host the systems fixture, but Gate E must return to Federal Forum before slice acceptance.

## What this slice proves

It is not a content demo. It proves:

- GitHub -> Unity -> Android build;
- canonical import;
- stable world identity;
- chunk streaming;
- deterministic simulation;
- primitive 3D presentation;
- touch camera;
- road construction;
- block/parcel generation;
- zoning/development;
- building construction;
- households/jobs;
- trip generation/pathfinding;
- traffic;
- one transit mode;
- power;
- one service;
- simple economy/housing;
- explainability/overlays;
- save/load;
- profiling.

## Slice content boundary

### Canon/world

- planet-koplin-prime;
- settlement-concordia;
- world-atlas-koplin-3;
- tile 0,0 metadata;
- imported tile adjacency/edge anchors;
- Federal Forum semantic district information;
- canonical visual reference key.

### Runtime world

- 16 x 250m chunks;
- lightweight terrain;
- water/greenway anchors where present;
- canonical major road/transit anchor stubs;
- external connector stubs at tile edges.

### Build tools

- one local street template;
- one boulevard/arterial template;
- pedestrian path;
- zoning brush;
- delete/cancel;
- blueprint/preview;
- undo before simulated consequence;
- cost/validation message.

### Development

- generated blocks/parcels;
- residential family;
- mixed-use family;
- office/research family;
- civic/Federal Forum primitive archetype;
- construction stages;
- occupancy.

### Population/economy

- households;
- dwellings;
- jobs;
- education/job qualification minimal;
- income/rent;
- municipal cash/operating spend;
- development demand.

### Mobility

- walking;
- road vehicle trips;
- link queue traffic;
- one transit line/mode;
- origin/destination routing;
- route cache;
- tile-edge external stub.

### Utility/service

- power generation/import source;
- distribution graph;
- building demand;
- one clinic or education service;
- staffing/access/power effective-capacity calculation.

### UI

- select parcel/building/road;
- context panel;
- top causal factors;
- traffic speed/volume;
- power overlay;
- housing/jobs summary;
- pause/speeds;
- save/load.

## Slice benchmark population

Use two modes:

1. **Canonical presentation fixture** — authored/simulated population consistent enough for tile gameplay.
2. **10k architecture benchmark fixture** — synthetic deterministic population allowed to stress the same systems without claiming 10k is canonical Federal Forum population.

Benchmark data must be visibly labelled test-only and never imported into canon.

## Work packages

### VS-01 Android proof

Tasks:

- IMP-CI-001..009;
- IMP-FND-001..010.

Acceptance:

- APK from Actions installs;
- PVG blocks render;
- touch camera works;
- build metadata shown.

### VS-02 deterministic core

Tasks:

- IMP-SIM-001..010;
- IMP-ID-001..005;
- initial IMP-SAV-001..006.

Acceptance:

- headless deterministic checksum;
- save header/version;
- stable IDs;
- pause/speed.

### VS-03 canon/world

Tasks:

- IMP-CAN-001..009;
- IMP-WLD-001..009.

Acceptance:

- exact Universe SHA reported;
- tile 0,0 imports;
- 16 chunks stream;
- camera crosses chunks.

### VS-04 PVG presentation

Tasks:

- IMP-REN-001..007;
- IMP-INP-001..006.

Acceptance:

- recognisable clean Concordia palette;
- deterministic primitives;
- selection/context panel.

### VS-05 roads/parcels

Tasks:

- IMP-RD-001..011;
- IMP-DEV-001..008.

Acceptance:

- draw road;
- create curved/irregular block;
- generate legal parcels;
- zone touch interaction.

### VS-06 buildings/development

Tasks:

- IMP-DEV-009..015;
- IMP-REN-011.

Acceptance:

- demand creates project;
- project constructs;
- building becomes occupied;
- cause factors visible.

### VS-07 people/jobs

Tasks:

- IMP-POP-001..013;
- selected IMP-ECO-001..008.

Acceptance:

- households live in dwellings;
- job slots matched;
- income/rent work;
- 10k fixture invariant passes.

### VS-08 mobility

Tasks:

- IMP-MOB-001..011;
- IMP-MOB-013..014.

Acceptance:

- home -> job route;
- congestion;
- transit option;
- traffic speed vs volume overlays.

### VS-09 utility/service

Tasks:

- IMP-CIV-001..009;
- IMP-CIV-016.

Acceptance:

- power bottleneck reduces building/service performance;
- trace shows cause;
- service access uses travel time.

### VS-10 save/explain/perf

Tasks:

- remaining vertical-slice IMP-SAV;
- IMP-UX-001..012;
- IMP-QA-001..010;
- IMP-PERF-001..006.

Acceptance:

- save/reload equivalence;
- interrupted save safe;
- developer profiler;
- Android 10k benchmark;
- 20-minute device smoke.

## Required demonstrations

A reviewer must be able to perform:

1. install APK from GitHub;
2. load Federal Forum slice;
3. pan/zoom/rotate;
4. inspect canonical tile identity;
5. draw curved road;
6. see blocks/parcels regenerate;
7. zone housing/mixed use;
8. advance time;
9. watch primitive buildings construct;
10. inspect household/job occupancy;
11. watch road traffic/transit;
12. create power bottleneck;
13. trace its effect to a building/service;
14. view traffic and utility overlays;
15. save;
16. kill/restart;
17. reload same city;
18. verify state/history persists.

## Slice rejection conditions

Reject the slice even if it looks impressive if:

- GameObjects/MonoBehaviours represent each citizen;
- roads are only visual meshes;
- parcel IDs churn on minor edits;
- save serialises ECS memory/world directly;
- render distance changes city outcomes;
- Universe data is copied/manual rather than imported;
- UI explanation is hardcoded separately from simulation factors;
- Android build requires undocumented local editor steps;
- 10k benchmark already exhibits unbounded route/memory growth.

## Gate mapping

Completing the vertical slice should satisfy:

- Gate A — simulation kernel;
- Gate B — city geometry;
- Gate C — living district;
- Gate D — networked city;
- Gate E — canon/art identity;
- part of Gate G — CI/save/accessibility shell.

Gate F scale remains later at 250k.
