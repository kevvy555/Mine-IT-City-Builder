# 09 — Android Performance and Device QA

## Purpose

Prove production behaviour on real Android hardware rather than inferring from editor performance.

## Device matrix

Before Gate F, select named physical devices for:

- minimum supported class;
- reference production class;
- high/flagship class.

Record:

- model;
- SoC/GPU;
- RAM;
- Android version;
- graphics API;
- screen resolution/refresh.

Emulators are not substitutes for performance/thermal certification.

## Performance suites

### QA-PERF-001 — Empty/bootstrap baseline

Measure startup, idle frame, memory.

### QA-PERF-002 — PVG instance scale

10k / 50k / 100k+ primitive parts.

Measure:
- CPU;
- GPU;
- batches;
- memory.

### QA-PERF-003 — B10K

Full vertical systems representative.

### QA-PERF-004 — B50K

Multi-tile integration.

### QA-PERF-005 — B250K

Production-scale certification.

### QA-PERF-006 — B1M stretch

Informational unless promoted.

## B250K production acceptance

On reference production device:

- representative gameplay achieves 30 FPS budget;
- normal simulation speed sustained;
- route queue bounded;
- memory within accepted threshold;
- save/load acceptable;
- no recurring large GC stalls;
- render/simulation LOD transitions do not corrupt outcome.

## Frame analysis

Record:

- median;
- 95p;
- 99p;
- worst frame;
- main thread;
- render thread;
- GPU;
- simulation systems;
- UI.

Average FPS alone is insufficient.

## Thermal QA

QA-PERF-010 — 20-minute default-quality representative session.

Capture:
- frame rate over time;
- achieved simulation speed;
- thermal status if available;
- clocks where tooling permits;
- battery temperature where available.

Pass criteria:
- no severe sustained thermal collapse;
- adaptive quality activates appropriately;
- simulation correctness unchanged.

Also run longer 60-minute soak at release candidate on reference/high device.

## Memory QA

Track:

- resident memory;
- graphics memory estimate;
- ECS/domain allocations;
- route cache;
- history;
- chunk streaming;
- peak save snapshot.

QA-PERF-020 — Repeated pan/stream cycles do not cause monotonic leak.

QA-PERF-021 — Repeated city load/unload returns near baseline allocation envelope.

## Lifecycle QA

QA-PERF-030:
- background/resume;
- screen lock;
- app switch;
- low-memory warning where reproducible;
- graphics context restoration;
- Android process kill/relaunch using persisted save.

## Graphics tiers

Run same authoritative scenario:

- Low/PVG;
- Medium;
- High.

QA-PERF-040 — authoritative checksum/metric envelope identical.

Only presentation/performance may differ.

## Touch latency

Measure perceived/tool latency:

- tap selection;
- camera gesture;
- road preview;
- confirm;
- overlay toggle.

Ordinary interactions should respond immediately even if expensive analysis completes asynchronously.

## Battery

Record representative battery drain for release candidate. No fixed threshold initially, but regression > meaningful agreed percentage requires investigation.

## Build/package QA

Verify:

- ARM64;
- API target;
- min OS;
- Vulkan;
- fallback graphics path where supported;
- release signing;
- clean install;
- upgrade install;
- AAB install via Play internal when enabled.

## Evidence

Android performance result always includes exact build SHA and device ID/model. A desktop benchmark cannot certify Gate F.
