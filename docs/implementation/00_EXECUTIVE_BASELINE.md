# 00 — Executive Implementation Baseline

## Product to build

MineIT City Builder is an Android-first, landscape 3D city simulator set in canonical Concordia on Koplin 3. It must combine a deep, inspectable city simulation with a deliberately lightweight initial rendering strategy so engineering can prove city behaviour before the project depends on a large 3D art library.

The project is not a throwaway prototype. The first playable architecture must be capable of evolving into production without replacing stable IDs, saves, world coordinates, simulation ownership or the render/simulation boundary.

## V1 outcome

A v1-quality foundation means the player can:

- load the canonical Concordia start area;
- pan, rotate, zoom and inspect the 3D city on an Android touch device;
- build and edit roads, paths, zoning and infrastructure;
- observe parcels and buildings develop;
- simulate households, jobs, education, housing, businesses and municipal finance;
- observe real trip demand through walking, road traffic and public transport;
- operate power and other city services;
- diagnose problems through causal UI and overlays;
- save and reload long-running cities;
- expand across the first 100 canonical 1 km² atlas tiles;
- run at useful mobile performance with 250,000 persistent citizens;
- retain stable behaviour when graphics quality or camera location changes.

## V1 non-goals

The following must not block v1:

- multiplayer;
- explorable full building interiors;
- planet-wide physically simulated population;
- photorealistic art;
- bespoke high-detail 3D models for every building/vehicle/person;
- arbitrary executable user mods on Android;
- fully simulated national/global macroeconomics;
- detailed election campaigning;
- physics-based vehicle simulation;
- every citizen rendered at once;
- fully detailed simulation for off-map regions.

## Engineering principles

### Simulation owns truth

Rendering never owns authoritative economic, demographic, mobility or city state. A building mesh may disappear, change LOD or be replaced entirely while the simulation entity remains unchanged.

### Primitive first, replaceable forever

The first visual system is intentionally capable of shipping: clean primitive assemblies with strong colour, shape, lighting and motion. High-quality assets later replace visual archetypes rather than replacing simulation entities.

### Mobile first

Thermal behaviour, memory, touch interaction, background interruptions, Android lifecycle, long frame spikes and save resilience are architecture concerns from Phase 1.

### Risk before volume

Pathfinding, deterministic multithreading, parcel geometry, save migration, simulation LOD and 250k scale are proven before the project invests heavily in content.

### Explainability is architecture

Any major number shown to the player must be traceable to the authoritative calculation that produced it. Explanation metadata is designed with systems rather than fabricated by UI later.

## Scale contract

### Detailed world

- canonical detailed v1 extent: 100 km²;
- source grid: 100 canonical 1 km² atlas tiles;
- runtime spatial grid: 1,600 chunks at 250m x 250m;
- only a camera/relevance window is render-resident at full detail;
- simulation LOD may vary by relevance, but authoritative totals persist.

### Population

- 10,000: vertical-slice benchmark;
- 50,000: early integration benchmark;
- 250,000: primary production benchmark;
- 1,000,000: stretch benchmark using more aggressive aggregation.

The 250k target is not permission to create 250k heavyweight Unity objects. Persistent state must be compact and high-count work must be data-oriented.

## Performance contract

Baseline gameplay targets:

- 30 FPS on the defined reference Android device class;
- 60 FPS as an optional mode on capable hardware;
- no frame-time-dependent simulation;
- no routine main-thread stalls from saving, content streaming or network rebuilds;
- thermal degradation reduces presentation quality and achieved simulation speed before compromising simulation correctness.

Exact budgets are in `22_PERFORMANCE_BUDGETS_AND_DEVICE_TIERS.md`.

## Android lifecycle contract

The game must tolerate:

- application pause;
- backgrounding;
- device rotation request even though landscape is locked;
- OS memory pressure;
- interrupted save;
- process death after a completed save;
- loss/recreation of graphics context as supported by Unity;
- low battery/thermal performance changes.

Autosave and lifecycle hooks are required before large player cities exist.

## Data ownership contract

| Data | Authority |
|---|---|
| Koplin lore/canon | MineIT-Universe |
| Canon IDs/world atlas | MineIT-Universe |
| Game mechanics/tuning | Mine-IT-City-Builder |
| Generated primitive visual definitions | Mine-IT-City-Builder |
| Player city state | Save file |
| Generated citizens/businesses/buildings | Save/procedural state |
| Derived render caches | Regenerable game cache |
| Imported Universe runtime catalogue | Derived build artefact |

## Architecture acceptance

Before broad feature production:

- Android build proof passes;
- deterministic kernel exists;
- stable IDs exist;
- save version 1 exists;
- Universe lock/import exists;
- one atlas tile streams;
- Primitive Visual Grammar renders through instancing;
- touch camera works;
- a benchmark harness reports CPU/GPU/memory/simulation metrics.

These are architectural gates, not polish.
