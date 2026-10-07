# 29 — Simulation Architecture and Performance

## Architectural goal

A city builder has two scale problems at once:
- many simulated entities;
- many visible objects.

The architecture must prevent either from forcing every object to be a heavyweight engine actor updated every frame.

KCB-ARCH-001 — Authoritative simulation state MUST be separable from render/game-object representation.

KCB-ARCH-002 — The engine choice is deferred, but the implementation MUST support data-oriented processing, spatial partitioning and representation LOD.

## Domain boundaries

Recommended simulation domains:
- Time/Event Scheduler
- Spatial/World
- Networks/Routing
- Population/Households
- Economy/Organisations
- Buildings/Development
- Transport/Traffic
- Services
- Utilities
- Environment
- Government/Policies
- Events/Narrative
- Presentation bridge

KCB-ARCH-010 — Domains communicate through explicit data contracts/events rather than uncontrolled cross-system mutation.

KCB-ARCH-011 — Cyclic dependencies are identified in architecture diagrams and resolved through staged update order or shared authoritative domain.

## Update phases

Example fixed simulation cycle:
1. ingest player commands due at boundary;
2. process scheduled events;
3. update network topology changes;
4. utility/service fast systems;
5. mobility/traffic step;
6. production/economy step as due;
7. household/citizen step as due;
8. development/land-value step as due;
9. environment step as due;
10. statistics/history;
11. generate presentation events/snapshots.

Exact order is implementation-plan work.

KCB-ARCH-020 — Update ordering MUST be documented and covered by deterministic tests.

## Data-oriented storage

High-count entities SHOULD use structure-of-arrays/component/chunk-oriented storage or equivalent efficient representation.

Examples:
- citizens;
- vehicles/trips;
- parcels;
- buildings;
- network edges;
- environmental cells.

KCB-ARCH-030 — Hot-loop data SHOULD avoid per-entity heap allocation.
KCB-ARCH-031 — Frequently processed components should be compact and cache-friendly.
KCB-ARCH-032 — Strings and large narrative data stay outside hot records, referenced by IDs.

## Spatial partition

Use stable spatial chunks aligned to atlas tiles.

KCB-ARCH-040 — Spatial queries operate through indices/chunks.
KCB-ARCH-041 — Updating one road does not scan every building in the metropolis.
KCB-ARCH-042 — Chunks track dirty flags by subsystem.

## Simulation LOD

Suggested bands:
- active: detailed routing/agents;
- warm: reduced frequency/detail;
- aggregate: district/chunk flow model;
- external: connector-level model.

KCB-ARCH-050 — LOD level is driven by simulation relevance, not camera alone.

KCB-ARCH-051 — Camera can request higher representation/detail but cannot reduce correctness elsewhere.

KCB-ARCH-052 — Aggregate/detailed conversions conserve authoritative totals and inventories.

## Active-set budgeting

Systems with potentially huge work queues:
- pathfinding;
- agent instantiation;
- development proposals;
- migration decisions;
- business matching;
- service dispatch;
- visual updates.

KCB-ARCH-060 — Work is budgeted per simulation step.
KCB-ARCH-061 — Backlogs are instrumented.
KCB-ARCH-062 — High speed may allocate more CPU budget if available but cannot starve mandatory systems.

## Pathfinding architecture

Requirements:
- shared multimodal network;
- hierarchy;
- reusable route costs;
- localized invalidation;
- path request queue;
- cache metrics.

KCB-ARCH-070 — Network versioning identifies whether a cached route is still valid.
KCB-ARCH-071 — Traffic congestion cost updates SHOULD not invalidate every full route every micro-step; use thresholded/dynamic cost handling.

## Multithreading

Parallel candidates:
- independent chunk environment updates;
- pathfinding requests;
- household evaluations;
- business production;
- statistics;
- procedural generation.

KCB-ARCH-080 — Parallel jobs MUST not write overlapping authoritative state without deterministic synchronization.

KCB-ARCH-081 — Job completion order must not become random simulation order.

## Rendering separation

Presentation bridge receives:
- transforms;
- state categories;
- animation parameters;
- visibility;
- material/instance data.

KCB-ARCH-090 — Rendering code does not own authoritative citizen money, inventory or service state.

KCB-ARCH-091 — Destroying a visual representation cannot destroy the simulated entity.

## World streaming

Techniques expected from modern engines:
- grid/cell streaming;
- hierarchical LOD/proxies;
- instancing;
- frustum culling;
- optional occlusion;
- asset streaming.

KCB-ARCH-100 — Detailed visual chunks can unload while simulation summary remains.

## Memory budgets

Implementation plan MUST establish per-platform budgets for:
- persistent simulation;
- loaded world chunks;
- textures;
- meshes;
- animation;
- pathfinding caches;
- save snapshot overhead;
- UI/history;
- mod headroom.

KCB-ARCH-110 — Memory use is profiled at target city sizes, not only tutorial city.

## Frame/simulation budgets

Targets to decide by platform:
- render frame ms;
- main thread ms;
- simulation ms per fixed step;
- pathfinding budget;
- streaming budget;
- worst-case GC/allocation;
- autosave pause budget.

KCB-ARCH-120 — Performance CI records benchmark thresholds on standard scenarios.

## Thermal/constrained hardware

If mobile/portable is targeted:
- adaptive quality;
- reduced crowd representation;
- shadow/reflection scaling;
- dynamic resolution;
- simulation speed cap under thermal pressure if necessary.

KCB-ARCH-130 — Thermal adaptation reduces presentation first.
KCB-ARCH-131 — If simulation throughput must fall, the UI reports achieved speed rather than skipping state changes.

## Profiling instrumentation

Counters:
- entities by type;
- active trips;
- route requests/sec;
- route queue;
- route cache hit;
- network rebuild time;
- simulation step time by domain;
- chunks loaded;
- draw calls/instances;
- visible agents;
- memory by subsystem;
- save size/time.

KCB-ARCH-140 — Profiling counters are available in development/laboratory builds from first vertical slice.

## Target scale tiers

Implementation plan should benchmark at:
- tiny: 10k residents;
- small: 50k;
- medium: 250k;
- large: 1m;
- stretch: several million using aggregation.

These are engineering test scales, not canonical Concordia population.

KCB-ARCH-150 — The implementation plan must choose a guaranteed supported population target and a stretch target.

## Failure degradation order

When hardware is overloaded:
1. lower decorative particles;
2. lower shadows/reflections;
3. lower distant props/vegetation;
4. reduce rendered crowds/vehicles;
5. increase visual LOD aggressiveness;
6. reduce noncritical simulation update frequency within allowed bounds;
7. reduce achieved fast-forward speed.

Never:
- delete citizens;
- clear traffic;
- teleport freight;
- skip financial ledger events;
- alter demand because FPS is low.

## Acceptance criteria

- Removing a render proxy does not alter simulation.
- Benchmark city can zoom from close to metro without massive allocation spike.
- Pathfinding backlog is observable.
- Distant district economy continues with no camera.
- Detailed-to-aggregate transition conserves population, money and goods.
- Fixed benchmark seed produces repeatable metrics under different render quality settings.
