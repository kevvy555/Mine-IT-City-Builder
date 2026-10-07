# Koplin City Builder — Specification Catalogue

**Status:** Pre-implementation specification  
**Working title:** Koplin City Builder  
**Setting:** Koplin 3, primarily Concordia on the Zoran continent  
**Canon baseline:** MineIT Universe plus the Koplin 3 canonical atlas  
**Implementation status:** Not started

## Purpose

This catalogue defines the complete product and simulation specification for a city-builder set on the Koplin homeworld. It is intentionally split into focused documents so the next phase can turn each requirement group into an implementation plan, work breakdown, architecture decisions, tests and content tasks without rediscovering design intent.

This repository does not create a second copy of Universe canon. Canonical facts remain in data/lore, data collections and the Koplin 3 atlas. The game may consume and reference those stable IDs. City layouts, citizens, traffic, budgets, player policies, construction, simulation history and other per-save state belong to the future game repository and save files.

## Normative language

- MUST / MUST NOT: required for the intended game.
- SHOULD / SHOULD NOT: strong design requirement; deviation needs an explicit reason.
- MAY: optional or implementation-dependent.
- TUNING BASELINE: a starting value for balancing, not Universe canon.
- DEFERRED: intentionally left to the implementation-plan phase or later design review.

Requirement IDs are stable references for implementation planning and test coverage.

## Product thesis

Koplin City Builder is not a generic city simulator with science-fiction art. It is a simulation of a mature Commonwealth city on a real canonical world. The player shapes Concordia while respecting its federal history, mixed population, advanced but bounded technology, strong public institutions, mature infrastructure, ecological stewardship and post-AI-war political safeguards.

The core fantasy is to make a city that is both beautiful to watch and interesting to diagnose: people live real lifecycles, businesses and institutions choose locations, goods and commuters move through physical networks, public services have capacity and travel constraints, land develops in response to accessibility and desirability, and every major problem can be traced to understandable causes.

## Canon integration rule

The future game MUST load or transform canonical Universe records through an explicit import layer. It MUST NOT silently fork canonical facts into independently edited game data.

Canonical references include, at minimum:

- planet-koplin-prime
- settlement-concordia
- world-atlas-koplin-3
- species-trondonian
- species-zoran
- species-blaxmar
- organisation-koplin-compact
- currency-commonwealth-credit
- canonical landform, biome, hydrosphere, substance, organisation and technology vocabularies where relevant

The atlas is a canonical place reference and art/world-building baseline. A player save may alter the built environment, but the save must distinguish authored starting-state content from player-created or simulated changes.

## Document map

### Foundation
1. [Vision and Design Pillars](00_VISION_AND_PILLARS.md)
2. [Canon and Setting](01_CANON_AND_SETTING.md)
3. [Game Modes and Player Role](02_GAME_MODES_AND_PLAYER_ROLE.md)
4. [World Map and Spatial Model](03_WORLD_MAP_AND_SPATIAL_MODEL.md)
5. [Time, Simulation and Determinism](04_TIME_SIMULATION_AND_DETERMINISM.md)

### Physical city
6. [City Growth and Land Development](05_CITY_GROWTH_AND_LAND_DEVELOPMENT.md)
7. [Roads, Streets and Rights of Way](06_ROADS_STREETS_AND_RIGHTS_OF_WAY.md)
8. [Public Transit and Mobility](07_PUBLIC_TRANSIT_AND_MOBILITY.md)
9. [Traffic, Pathfinding and Logistics](08_TRAFFIC_PATHFINDING_AND_LOGISTICS.md)
10. [Buildings, Zoning, Modularity and Architecture](21_BUILDINGS_ZONING_MODULARITY_AND_ARCHITECTURE.md)

### People and society
11. [Population, Households and Lifecycles](09_POPULATION_HOUSEHOLDS_AND_LIFECYCLES.md)
12. [Jobs, Education and Workforce](10_JOBS_EDUCATION_AND_WORKFORCE.md)
13. [Housing, Land Value and Property](11_HOUSING_LAND_VALUE_AND_PROPERTY.md)
14. [Culture, History, Identity and Social Systems](19_CULTURE_HISTORY_IDENTITY_AND_SOCIAL_SYSTEMS.md)
15. [Events, News, Narrative and Living World](26_EVENTS_NEWS_NARRATIVE_AND_LIVING_WORLD.md)

### Economy and government
16. [Economy, Finance and Markets](12_ECONOMY_FINANCE_AND_MARKETS.md)
17. [Industry, Production and Supply Chains](13_INDUSTRY_PRODUCTION_AND_SUPPLY_CHAINS.md)
18. [City Services and Facilities](14_CITY_SERVICES_AND_FACILITIES.md)
19. [Power, Water, Waste and Utilities](15_POWER_WATER_WASTE_AND_UTILITIES.md)
20. [Health, Safety, Emergency and Resilience](16_HEALTH_SAFETY_EMERGENCY_AND_RESILIENCE.md)
21. [Governance, Districts, Policies and Law](17_GOVERNANCE_DISTRICTS_POLICIES_AND_LAW.md)
22. [Environment, Climate, Ecology and Pollution](18_ENVIRONMENT_CLIMATE_ECOLOGY_AND_POLLUTION.md)
23. [Research, Technology and AI](20_RESEARCH_TECHNOLOGY_AND_AI.md)

### Presentation and interaction
24. [Art Direction and Rendering](22_ART_DIRECTION_AND_RENDERING.md)
25. [Camera, Controls, Input and Construction Tools](23_CAMERA_CONTROLS_INPUT_AND_CONSTRUCTION_TOOLS.md)
26. [UI, UX, Info Views and Explainability](24_UI_UX_INFO_VIEWS_AND_EXPLAINABILITY.md)
27. [Audio, Music and City Soundscape](25_AUDIO_MUSIC_AND_CITY_SOUNDCAPE.md)
28. [Progression, Goals, Difficulty and Failure](27_PROGRESSION_GOALS_DIFFICULTY_AND_FAILURE.md)

### Technical and production contracts
29. [Save Data, Modding and Data Pipeline](28_SAVE_DATA_MODDING_AND_DATA_PIPELINE.md)
30. [Simulation Architecture and Performance](29_SIMULATION_ARCHITECTURE_AND_PERFORMANCE.md)
31. [Accessibility, Localisation and Settings](30_ACCESSIBILITY_LOCALISATION_AND_SETTINGS.md)
32. [Testing, Validation, Telemetry and Balancing](31_TESTING_VALIDATION_TELEMETRY_AND_BALANCING.md)
33. [Content Catalogue and Asset Requirements](32_CONTENT_CATALOGUE_AND_ASSET_REQUIREMENTS.md)
34. [Implementation Contracts and Handoff](33_IMPLEMENTATION_CONTRACTS_AND_HANDOFF.md)
35. [Glossary and Requirement Index](34_GLOSSARY_AND_REQUIREMENT_INDEX.md)

### Research
- [Genre Findings](research/GENRE_FINDINGS.md)
- [Canon Audit](research/CANON_AUDIT.md)
- [Research References](research/REFERENCES.md)

## Cross-system rules

KCB-CROSS-001 — Every simulated shortage or failure MUST have at least one player-visible causal trace.

KCB-CROSS-002 — Every networked service MUST distinguish nominal capacity from effective delivered service after routing, congestion, outages and staffing.

KCB-CROSS-003 — Buildings MUST not be independent stat boxes. Their outcomes SHOULD depend on access, workforce, utilities, inputs, nearby externalities and policy.

KCB-CROSS-004 — Citizens MUST be aggregated where scale requires it, but individual visible agents MUST remain explainable projections of the underlying simulation.

KCB-CROSS-005 — The simulation MUST support graceful level-of-detail reduction without changing high-level economic outcomes merely because the camera moved away.

KCB-CROSS-006 — Art and animation MUST communicate simulation state: occupancy, prosperity, congestion, maintenance, weather, activity, emergency and time of day should alter the visible city where practical.

KCB-CROSS-007 — Automation and AI may assist planning and operations but MUST remain bounded, auditable and under biological authority, consistent with Commonwealth canon.

KCB-CROSS-008 — Species identity MUST NOT impose profession, intelligence, class or behaviour stereotypes. Individual and household differences drive simulation outcomes.

KCB-CROSS-009 — The game MUST preserve a strict distinction between source-canonical facts, authored game starting state, procedural content and mutable save state.

KCB-CROSS-010 — Deep systems MUST be optional to micromanage. Default policies and competent institutional automation should keep ordinary cities functional; expert controls expose additional optimisation.

## Definition of specification complete

This catalogue is ready for implementation planning when:

- every subsystem has an owner document and stable requirement IDs;
- all cross-system inputs and outputs are identified;
- every canonical dependency is named;
- performance-sensitive systems define simulation and visual LOD expectations;
- UI requirements expose causes rather than only symptoms;
- save/schema/versioning requirements exist before implementation;
- acceptance criteria can be translated into automated or deterministic tests;
- unresolved choices are explicit rather than hidden assumptions.

The implementation plan MUST trace work items back to these requirement IDs.


## Repository ownership

This catalogue is authoritative in `kevvy555/Mine-IT-City-Builder`. MineIT-Universe remains authoritative for shared Universe canon. The original specification was migrated from MineIT-Universe commit `ba3235d8b4c6e5d55408903952c466b85e3f1dae`.
