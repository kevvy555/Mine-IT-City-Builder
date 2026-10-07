# 25 — Risk Register and Required Prototypes

## Scoring

Probability (P): 1–5.  
Impact (I): 1–5.  
Risk = P × I.

Scores are planning baselines and should be revised with evidence.

| Risk | P | I | Score | Required proof | Deadline |
|---|---:|---:|---:|---|---|
| 250k mobile CPU scale | 4 | 5 | 20 | B250K simulation benchmark | Gate F |
| routing request throughput | 4 | 5 | 20 | hierarchical routing prototype | Phase 8 |
| deterministic multithreading | 3 | 5 | 15 | repeated checksum jobs test | Gate A |
| parcel geometry robustness | 4 | 4 | 16 | 10k polygon fuzz + Clipper2 proof | Gate B |
| save size/time/migration | 3 | 5 | 15 | 50k then 250k save benchmark | Gate F |
| simulation LOD equivalence | 4 | 5 | 20 | detailed vs aggregate metric fixtures | Gate F |
| Android thermal throttling | 4 | 4 | 16 | 20-minute sustained device test | Gate F |
| rendering instance scale | 2 | 4 | 8 | PVG 50k+ instance Android test | Phase 4 |
| UI explainability complexity | 3 | 4 | 12 | cause graph vertical slice | Gate D |
| economy long-run instability | 4 | 4 | 16 | 100-year soak | Gate G |
| atlas reconstruction workload | 4 | 3 | 12 | procedural/authoring workflow for 3 tiles | Phase 15 |
| Unity/GameCI licensing friction | 3 | 4 | 12 | Actions-built APK | Phase 1 |
| package regression | 3 | 4 | 12 | pinned packages + upgrade branch policy | ongoing |
| Android accessibility semantics | 3 | 4 | 12 | TalkBack prototype | before UI lock |
| art replacement compatibility | 2 | 4 | 8 | PVG -> HQ visual swap test | Phase 12 |
| memory/history growth | 3 | 5 | 15 | 100-year memory/save report | Gate G |

## Prototype P1 — CI Android proof

Question: can this repo build Unity Android entirely through Actions?

Pass:

- clean checkout;
- Unity activation;
- IL2CPP ARM64 APK;
- artifact downloadable;
- installs/starts.

Failure response:

- fix pipeline before gameplay implementation.

## Prototype P2 — ECS population footprint

Generate:

- 10k;
- 50k;
- 250k citizens/households.

Measure:

- component bytes;
- world memory;
- daily lifecycle job time;
- structural change count.

Pass before population architecture expands.

## Prototype P3 — Routing hierarchy

Synthetic city graph with dynamic closure.

Test:

- local A*;
- chunk portal hierarchy;
- route cache;
- invalidation.

Measure 2k+ effective route requests/s target workload.

If weak:

- add landmark heuristic;
- improve OD batching;
- increase route reuse;
- redesign hierarchy before transit/freight depend on it.

## Prototype P4 — Parcel geometry

Generate 10k pathological/random blocks:

- concave;
- narrow;
- holes;
- acute;
- near-collinear;
- curved-road approximation.

Assert:

- termination;
- valid non-overlap;
- access;
- area conservation tolerance.

## Prototype P5 — Deterministic jobs

Run same input 100+ times with:

- different worker counts where available;
- different rendering frame rates;
- save/reload midpoint.

Compare authoritative checksums.

Any ordering dependence is fixed before high-count domain work.

## Prototype P6 — PVG Android scale

Render:

- 10k;
- 50k;
- 100k primitive parts/instances.

Vary:

- materials;
- LOD;
- shadows;
- camera.

Measure GPU/frame/memory.

This proves primitive art strategy is cheap enough.

## Prototype P7 — Simulation LOD equivalence

Run same seeded district:

- fully detailed;
- mixed warm/aggregate;
- mostly aggregate.

Compare:

- population;
- completed trips;
- money;
- inventory;
- service outcomes;
- migration.

Define tolerance per metric. Camera cannot decide result.

## Prototype P8 — Save benchmark

Synthetic 50k then 250k:

- save;
- interrupt write;
- load;
- migrate fixture;
- compare checksum.

Measure stall/total time/size.

## Prototype P9 — Explainability

Create a deliberately underperforming clinic:

- low power;
- staff shortage;
- congestion.

Simulation publishes factor list and upstream trace.

UI must display correct top cause without duplicate formula.

## Prototype P10 — Thermal

On named reference Android device:

- 20 minutes;
- B50K/B250K representative city;
- normal speed;
- default quality.

Capture:

- FPS;
- achieved sim speed;
- thermal status;
- battery temperature where accessible;
- CPU/GPU timing.

Tune adaptive quality from evidence.

## Prototype P11 — Accessibility semantics

Vertical-slice menu/context panel:

- TalkBack;
- focus;
- slider/value;
- alert;
- overlay text summary.

If Unity UI Toolkit bridge cannot satisfy, prototype native bridge before UI architecture locks.

## Prototype P12 — Art replacement

Same save/building:

1. load PVG visual;
2. install/bind HQ visual archetype;
3. reload same save.

Building ID, capacity, history and gameplay checksum unchanged.

## Risk review

Every milestone PR checks:

- new risks;
- changed scores;
- evidence links;
- expired assumptions.

High risks cannot remain “we'll optimise later.”
