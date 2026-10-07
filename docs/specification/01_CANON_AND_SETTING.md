# 01 — Canon and Setting

## Canon authority

KCB-CAN-001 — Canon precedence MUST follow the repository AGENTS.md. Long-form lore in data/lore outranks structured expansion data where they conflict.

KCB-CAN-002 — The city-builder specification MUST consume canon; it MUST NOT redefine foundational history inside this folder.

KCB-CAN-003 — Where the game needs a fact not yet canonical, the implementation plan MUST classify it as one of:
- authored game fiction that affects only the game;
- proposed Universe authored expansion requiring separate canon approval;
- procedural/save-state content;
- implementation-only metadata.

## Time period

The default game period SHOULD be the mature Commonwealth era around Year 5300, with scenario presets able to move within the late Commonwealth period without contradicting established chronology.

Why this period fits:
- fusion and advanced power electronics are normal;
- rapid planetary transport is normal;
- precision manufacturing and advanced medicine are mature;
- bounded high-capability AI is normal;
- Veyrite and early interstellar expansion exist, creating off-world economic context;
- Concordia can be a prosperous federal capital rather than a frontier colony.

KCB-CAN-010 — Exact campaign start year is a game-design setting and MUST be frozen during implementation planning before economy/event content is authored.

## Koplin 3

Canonical identity:
- ID: planet-koplin-prime
- Name: Koplin 3
- Type: temperate Commonwealth homeworld
- Atmosphere: breathable
- Surface: approximately half land and half water
- Major inhabited continents: Trondonian, Zoran, Blaxmar
- Dominant landforms: plains, lowlands, mountains
- Dominant biomes: forest, grassland, wetland, tundra
- Dominant hydrosphere: ocean, lake, river

KCB-CAN-020 — Normal Concordia gameplay MUST not behave like a sealed colony. Breathable atmosphere and mature planetary infrastructure are baseline assumptions.

KCB-CAN-021 — Food, water and energy remain economic/infrastructure systems but ordinary failure represents distribution, capacity or affordability failure, not discovery of basic survival science.

## Concordia

Canonical identity:
- ID: settlement-concordia
- Type: Federal capital city
- Continent: Zoran
- Function: Commonwealth government, intercontinental coordination, science, culture and advanced services
- Character: mature, heavily networked, distributed government, public institutions, rapid transit, gardens and advanced low-impact infrastructure

The canonical atlas originates at the Federal Forum at coordinate 0,0 and uses 1 km by 1 km authored tiles.

KCB-CAN-030 — The Federal Forum and named atlas districts MUST be preserved as the canonical starting geography for the Concordia scenario unless a deliberate alternate-history/sandbox option is selected.

KCB-CAN-031 — The game MAY allow redevelopment of canonical districts, but destructive changes to major heritage assets SHOULD require an explicit warning, political process or sandbox override.

KCB-CAN-032 — The starting city SHOULD already function. The player’s challenge is stewardship, growth and reform, not placing the first road onto empty land.

## The three peoples

Canonical peoples:
- species-trondonian
- species-zoran
- species-blaxmar

KCB-CAN-040 — Species may influence small physiological comfort parameters only where lore supports them, such as heat/cold tolerance.

KCB-CAN-041 — Species MUST NOT directly determine profession, education, intelligence, wealth, criminality, political alignment or productivity.

KCB-CAN-042 — Mixed ancestry and mixed households MUST be supported as normal demographic states.

KCB-CAN-043 — Common Koplin Standard is the ordinary shared language. Ancestral language/culture may contribute to district identity, events and institutions without mechanically segregating populations.

## Historical layers

The game should treat the city as a civilisation with memory.

Relevant canonical periods:
- pre-contact continental civilisations;
- Zoran–Trondonian contact;
- Blaxmar conquest and occupation;
- industrial integration;
- global institutions;
- digital age;
- AI Wars around Year 2400;
- Commonwealth Compact in Year 2600;
- Long Plateau;
- Veyrite discovery around Year 5100;
- early interstellar period by Year 5300.

KCB-CAN-050 — Heritage content MAY include buildings, memorials, archives, festivals, place names and civic debates referencing these periods.

KCB-CAN-051 — The conquest MUST be represented as conquest and exploitation, not romanticised as inevitable unification.

KCB-CAN-052 — Modern conflicts SHOULD primarily concern class, land use, budgets, environmental priorities, transport, technology, historical interpretation and institutional power rather than default species antagonism.

## Government

The Koplin Commonwealth is a federal system with intentionally distributed authority.

KCB-CAN-060 — The player MUST NOT be framed as an absolute sovereign.

KCB-CAN-061 — Player authority SHOULD be represented as a planning/executive mandate operating through elected/appointed institutions, budgets and legal powers.

KCB-CAN-062 — Some actions SHOULD require political legitimacy, consultation, statutory process or compensation rather than merely currency.

KCB-CAN-063 — Local governments control everyday law, culture and public services while planetary/Commonwealth institutions retain higher-level responsibilities. The game may abstract these layers but MUST preserve the distinction.

## AI constraints

KCB-CAN-070 — High-capability AI has bounded purpose.
KCB-CAN-071 — AI may not hold sovereign authority or public office.
KCB-CAN-072 — Strategic decisions require accountable biological authority.
KCB-CAN-073 — Critical systems require auditable traces.
KCB-CAN-074 — Critical networks require segmentation/fallback modes.
KCB-CAN-075 — No unrestricted self-directed replication.

Gameplay implication: civic AI can forecast demand, optimise schedules, identify anomalies, automate routine dispatch and propose plans, but player/government choices remain meaningful.

## Economy and technology

Currency:
- currency-commonwealth-credit
- Display symbol: CC

KCB-CAN-080 — Homeworld economy SHOULD be materially comfortable and highly automated, not post-scarcity.

KCB-CAN-081 — Expensive/scarce categories include strategic materials, high-purity elements, Veyrite-linked industry, orbital construction and specialised infrastructure.

KCB-CAN-082 — Automation reduces routine labour requirements but does not eliminate employment, responsibility, care work, design, governance, research or physical infrastructure.

## Canon atlas integration

KCB-CAN-090 — world-atlas-koplin-3 is a canonical visual/geographical authored layer, not a per-save map.

KCB-CAN-091 — The future game’s import pipeline MUST preserve atlas tile IDs and coordinates.

KCB-CAN-092 — Atlas art can seed terrain, district identity, landmark placement and visual references, but interactive geometry must be represented by game-native data.

KCB-CAN-093 — Changes made by the player MUST be stored as save-state deltas or game-world state, never written back into Universe canon automatically.

## Canon review gate

Before implementation planning is signed off, every proposed:
- new named institution;
- new permanent Concordia landmark;
- new political body;
- new canonical transport technology;
- new canonical demographic number;
- new canonical historical event

must be tagged either “game-only” or “Universe canon proposal.” No accidental canon promotion is permitted.
