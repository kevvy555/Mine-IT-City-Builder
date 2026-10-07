# 22 — Performance Budgets and Device Tiers

## Philosophy

Budgets are engineering constraints, not aspirational notes.

When a budget fails, first reduce representation frequency/detail or improve algorithms. Do not silently delete authoritative citizens, trips, inventories or congestion.

## Reference device classes

### Minimum supported class

Target characteristics:

- Android 10+;
- ARM64;
- 6 GB RAM;
- Vulkan-capable mobile GPU with OpenGL ES fallback;
- mid-range 2021+ CPU/GPU performance class.

Minimum class must load/play the game, but the 250k production benchmark is certified against the **reference production class** below.

### Reference production class

- 8 GB RAM;
- modern mid/high Android SoC;
- Vulkan;
- sustained performance representative of mainstream devices at production time.

### High class

- current flagship Android;
- 8–12+ GB RAM;
- 60 FPS option;
- higher render density.

Before release, name concrete devices for each class and preserve them in benchmark records.

## Frame budget

30 FPS frame = 33.3 ms.

Reference production target, 95th percentile during ordinary gameplay:

- total frame <= 33.3 ms;
- main thread <= 14 ms;
- render thread <= 10 ms;
- GPU <= 28 ms;
- simulation contribution averaged across frames <= 8 ms normal speed;
- UI update <= 3 ms ordinary state;
- no recurring GC spikes > 2 ms.

Budgets overlap; they are not summed mechanically.

60 FPS high mode requires 16.7 ms total and automatically reduces representation if necessary.

## Thermal target

20-minute ordinary play session on reference production device:

- should not enter sustained severe thermal throttling under default quality;
- if thermal state rises, adaptive quality reduces shadows/agents/effects/resolution;
- simulation correctness unchanged;
- if CPU cannot maintain requested game speed, achieved simulation speed falls visibly.

## Memory

Reference targets:

- total game resident memory target <= 1.5 GB normal city;
- hard investigation threshold 2.0 GB on 8 GB reference class;
- graphics/texture target <= 512 MB normal, <= 768 MB high;
- 250k citizen/household core state target <= 256 MB;
- route/cache budgets explicitly bounded;
- history buffers bounded/downsampled.

These are initial budgets; measured Unity runtime overhead may force refinement by Gate F.

## Rendering

Typical gameplay camera target:

- <= 800 render batches/draw submissions after instancing;
- investigation threshold > 1,200;
- <= 2.5 million visible triangles normal;
- <= 4 million high;
- one shadowed directional light;
- <= 8 important local realtime lights near camera;
- no per-window lights;
- visible moving representative agents target ~2,500 normal, up to ~5,000 high;
- PVG fallback can reduce below these automatically.

## Chunk residency

Initial target:

- detailed: ~9 chunks;
- normal: ~25–49 chunks depending camera;
- proxy: tile/district horizon;
- remaining detailed-world chunks no full render residency.

Measure memory/time rather than freezing exact ring sizes.

## Simulation throughput

Normal speed target = 5 simulation minutes per real second.

Fast = 20 minutes/s.

Very fast = up to 80 minutes/s, best effort with aggregate-safe batching.

At B250K on reference device:

- normal must sustain target while 30 FPS presentation is active;
- fast should sustain unless major rebuild/save occurs;
- very fast may report reduced achieved rate rather than dropping work.

## Routing throughput

At B250K expected average departures can reach thousands per real second at normal speed.

Targets:

- >= 2,000 route requests/s effective normal workload;
- >= 80% cache/hierarchy reuse in stable mature city where realistic;
- 95p route planning latency below user-visible trip planning threshold;
- no unbounded request backlog.

Fast-speed workload relies more heavily on route reuse/aggregation.

## Structural edit budgets

Local road edit:

- preview response < 50 ms perceived where possible;
- expensive diagnostics async;
- commit local topology rebuild target < 250 ms for ordinary edit;
- no whole-city graph rebuild.

Parcel generation:

- one affected block ideally < 10 ms worker time;
- large edit can spread across frames/jobs.

## Save budgets

B250K target:

- snapshot/main-thread stall < 100 ms;
- total background save ideally < 5 s;
- hard target < 10 s on reference device;
- save size target < 500 MB, expected much lower;
- autosave no multi-second render freeze.

## Startup/load

Targets:

- app to responsive menu < 10 s reference device;
- B250K save to interactive city < 20 s target;
- progressive world presentation allowed after authoritative load/validation.

## Battery/network

Core game is offline; no background network requirement.

Avoid continuous polling/services.

## Quality tiers

### Low/PVG

- PVG forced;
- low shadows;
- reduced render agents;
- reduced vegetation/effects;
- lower resolution scale;
- same simulation.

### Medium/default

- PVG/HQ mix;
- default agent density;
- moderate shadows/effects.

### High

- HQ assets where available;
- more render agents/vegetation;
- longer detail distances;
- optional 60 FPS if device allows.

## Performance gate response

If B250K fails:

1. profile;
2. fix algorithm/data layout;
3. reduce presentation;
4. increase aggregation of simulation only where conservation/equivalence tests prove valid;
5. revise target only through explicit specification decision.

Never hide failure by reducing benchmark population without decision.

## Tasks

- IMP-PERF-001 — Define concrete reference devices.
- IMP-PERF-002 — Add Android performance HUD/export.
- IMP-PERF-003 — Measure empty-project baseline.
- IMP-PERF-004 — Measure PVG 10k/50k instances.
- IMP-PERF-005 — Measure B10K.
- IMP-PERF-006 — Measure B50K.
- IMP-PERF-007 — Measure B250K.
- IMP-PERF-008 — Add thermal 20-minute protocol.
- IMP-PERF-009 — Add memory budget report.
- IMP-PERF-010 — Add adaptive quality controller.
- IMP-PERF-011 — Verify graphics tier does not change sim checksum.
- IMP-PERF-012 — Add save/load performance report.

## Gate F acceptance

Gate F requires:

- B250K loads;
- 30 FPS target met in representative camera scenarios;
- normal simulation speed sustained;
- route backlog bounded;
- memory within accepted budget;
- 20-minute thermal test acceptable;
- save/load meets agreed threshold;
- low/high graphics produce equivalent authoritative simulation metrics.
