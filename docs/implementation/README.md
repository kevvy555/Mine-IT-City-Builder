# MineIT City Builder — Implementation Plan

**Plan version:** 0.1  
**Status:** Detailed implementation plan; implementation not started  
**Target:** Android phones and tablets  
**Engine:** Unity 6.3 LTS  
**Primary architecture:** C# + Unity Entities/DOTS + Burst + Job System + URP  
**Source specification:** `docs/specification/`  
**QA acceptance plan:** `docs/qa/`

## Purpose

This catalogue turns the normative Koplin City Builder specification into an executable engineering plan. It defines the technical foundation, repository structure, domain boundaries, delivery phases, performance budgets, prototypes, CI/CD, save strategy, Universe integration, requirement ownership and acceptance gates needed before and during production.

The specification remains the product/design authority. This implementation catalogue defines **how** the approved requirements will be realised. If implementation work changes intended behaviour, the specification must be updated in the same branch and pull request.

## QA acceptance contract

The QA catalogue at [docs/qa/README.md](../qa/README.md) defines how completed implementation is verified against the specification.

The implementation plan and QA plan are complementary:

- `docs/specification/` defines **what the game must do**;
- `docs/implementation/` defines **how it will be built**;
- `docs/qa/` defines **how we prove the implementation matches the specification**.

Every implementation phase must identify the QA IDs, fixtures, invariants, benchmarks and manual acceptance checks that will verify its KCB requirements. A feature is not complete when code merely exists; it is complete only when its required QA evidence passes.

The existing testing/benchmark implementation document defines the test infrastructure and execution machinery. The QA catalogue defines the behavioural acceptance oracle and final specification sign-off.

## Plan conventions

Implementation work uses stable task IDs:

- `IMP-FND-*` — foundation/repository/build
- `IMP-CI-*` — CI/CD/release
- `IMP-ARC-*` — architecture
- `IMP-SIM-*` — simulation kernel/time
- `IMP-ID-*` — entity identity/data
- `IMP-WLD-*` — world/spatial/streaming
- `IMP-CAN-*` — Universe/canon import
- `IMP-REN-*` — rendering/Primitive Visual Grammar
- `IMP-INP-*` — camera/input/UI shell
- `IMP-RD-*` — roads/construction
- `IMP-DEV-*` — parcels/zoning/development/buildings
- `IMP-POP-*` — population/jobs/education
- `IMP-ECO-*` — economy/housing/industry
- `IMP-MOB-*` — pathfinding/traffic/transit/freight
- `IMP-CIV-*` — utilities/services/environment/resilience
- `IMP-GOV-*` — governance/events/progression/technology
- `IMP-SAV-*` — saves/migrations
- `IMP-UX-*` — explainability/overlays/dashboards
- `IMP-ACC-*` — accessibility/localisation/audio/settings
- `IMP-QA-*` — testing/benchmarks/instrumentation
- `IMP-PERF-*` — performance/device tiers
- `IMP-VS-*` — vertical slice
- `IMP-REL-*` — release readiness

Task status vocabulary: **Planned**, **In progress**, **Blocked**, **Complete**, **Deferred**.

## Locked implementation baseline

- Android 10 / API 29 minimum OS.
- Android API 36 target.
- ARM64 + IL2CPP.
- Landscape-first.
- Unity 6.3 LTS.
- URP.
- Vulkan primary; OpenGL ES fallback.
- Unity Entities/DOTS for high-count authoritative simulation.
- Burst + Jobs for deterministic/high-throughput systems.
- UI Toolkit for runtime UI.
- Input System for touch, mouse, keyboard and later controller parity.
- Addressables/content catalogues for game-owned content.
- Purpose-built graph routing; no authoritative city traffic based on Rigidbody or NavMesh.
- Primitive Visual Grammar as the initial production visual style and permanent low-cost renderer.
- 250,000 persistent citizens as the primary v1 scale target.
- 1,000,000 persistent/aggregated citizens as a stretch benchmark.
- 100 canonical 1 km² atlas tiles as the v1 detailed Concordia extent.
- 250 m x 250 m runtime chunks, 16 per canonical atlas tile.
- Versioned custom chunked binary saves.
- Pinned MineIT-Universe build-time import.
- Offline single-player v1.
- Year 5300 baseline.
- Game-facing player role: Concordia Metropolitan Steward.

## Document map

### Foundation and architecture
1. [Executive Baseline](00_EXECUTIVE_BASELINE.md)
2. [Technology and Android Platform](01_TECHNOLOGY_AND_ANDROID_PLATFORM.md)
3. [Repository and Unity Project Structure](02_REPOSITORY_AND_PROJECT_STRUCTURE.md)
4. [CI/CD and Android Release Pipeline](03_CI_CD_AND_ANDROID_RELEASE.md)
5. [Architecture Context and Domains](04_ARCHITECTURE_CONTEXT_AND_DOMAINS.md)
6. [Simulation Kernel, Time and Determinism](05_SIMULATION_KERNEL_TIME_AND_DETERMINISM.md)
7. [Entity Data Model and Stable IDs](06_ENTITY_DATA_MODEL_AND_STABLE_IDS.md)

### World, data and presentation
8. [World, Atlas, Chunks and Streaming](07_WORLD_ATLAS_CHUNKS_AND_STREAMING.md)
9. [Universe Import and Content Pipeline](08_UNIVERSE_IMPORT_AND_CONTENT_PIPELINE.md)
10. [Primitive Visual Grammar and Rendering](09_PRIMITIVE_VISUAL_GRAMMAR_AND_RENDERING.md)
11. [Camera, Input and Mobile UI Shell](10_CAMERA_INPUT_AND_MOBILE_UI_SHELL.md)

### City systems
12. [Roads, Rights of Way and Construction](11_ROADS_RIGHTS_OF_WAY_AND_CONSTRUCTION.md)
13. [Blocks, Parcels, Zoning and Development](12_BLOCKS_PARCELS_ZONING_AND_DEVELOPMENT.md)
14. [Population, Households, Jobs and Education](13_POPULATION_JOBS_AND_EDUCATION.md)
15. [Economy, Housing, Industry and Land Value](14_ECONOMY_HOUSING_INDUSTRY_AND_LAND_VALUE.md)
16. [Pathfinding, Traffic, Freight and Transit](15_PATHFINDING_TRAFFIC_FREIGHT_AND_TRANSIT.md)
17. [Utilities, Services, Environment and Resilience](16_UTILITIES_SERVICES_ENVIRONMENT_AND_RESILIENCE.md)
18. [Governance, Technology, Events and Progression](17_GOVERNANCE_TECH_EVENTS_AND_PROGRESSION.md)

### Persistence, UX and quality
19. [Save, Migration and Data Format](18_SAVE_MIGRATION_AND_DATA_FORMAT.md)
20. [UI, Explainability, Overlays and Dashboards](19_UI_EXPLAINABILITY_OVERLAYS_AND_DASHBOARDS.md)
21. [Accessibility, Localisation, Audio and Settings](20_ACCESSIBILITY_LOCALISATION_AUDIO_AND_SETTINGS.md)
22. [Testing, Benchmarks, Soak and Instrumentation](21_TESTING_BENCHMARKS_SOAK_AND_INSTRUMENTATION.md)
23. [Performance Budgets and Device Tiers](22_PERFORMANCE_BUDGETS_AND_DEVICE_TIERS.md)

### Delivery
24. [Vertical Slice Execution Plan](23_VERTICAL_SLICE_EXECUTION_PLAN.md)
25. [Phased Roadmap and Milestone Gates](24_PHASED_ROADMAP_AND_GATES.md)
26. [Risk Register and Required Prototypes](25_RISK_REGISTER_AND_PROTOTYPES.md)
27. [Requirement Traceability](26_REQUIREMENT_TRACEABILITY.md)
28. [Initial Engineering Backlog](27_INITIAL_ENGINEERING_BACKLOG.md)
29. [Definition of Done and Release Gates](28_DEFINITION_OF_DONE_AND_RELEASE_GATES.md)

## Implementation order

The implementation order is intentionally risk-first:

1. prove Unity Android builds through GitHub Actions;
2. prove deterministic simulation and save foundations;
3. prove Universe import and stable world identity;
4. prove chunked world/render architecture and Primitive Visual Grammar;
5. prove road -> block -> parcel -> building pipeline;
6. prove households -> jobs -> trips;
7. prove pathfinding/traffic/transit and utilities;
8. complete the canonical 1 km² vertical slice;
9. scale from 10k -> 50k -> 250k;
10. expand system breadth and the first 100 km²;
11. harden saves, accessibility, content production and release.

No content-heavy phase may bypass a failed architecture/performance gate.

## Required first deliverable

The first executable milestone is deliberately small. Its implementation PR must also establish the first runnable QA evidence defined by the QA catalogue: a GitHub Actions workflow must generate an installable Android APK containing a touch-controlled 3D scene with a camera, deterministic primitive Concordia blockout, diagnostic overlay and build/version metadata. This proves the complete repository -> CI -> Unity -> Android path before major systems work begins.

## Definition of plan complete

This plan is ready for implementation when:

- each KCB requirement family has an implementation owner;
- all KCB MUST requirements are owned or explicitly deferred;
- all handoff decisions are frozen;
- high-risk systems have proof milestones before dependent production;
- the vertical slice has task-level acceptance criteria;
- performance budgets have measurable thresholds;
- CI/build/release mechanics are defined;
- save/canon/import ownership is explicit;
- branching and spec-sync rules in `AGENTS.md` are respected.
