# Research — Koplin Canon Audit for City Builder

## Canon sources reviewed

Highest precedence:
1. data/lore/Koplin_Universe_Expanded_Backstory_Lore_Bible.md
2. data/lore/Koplin_Universe_Materials_And_Substances.md
3. data/lore/Koplin_Universe_World_Surface.md
4. data/lore/Koplin_Scenario_II_Deep_Reach_Mining_Charter.md

Homeworld authored expansion:
- docs/Koplin3CanonicalWorldAtlas.md
- data/world-atlases.json
- data/world-atlas-tiles.json
- data/settlements.json
- data/planets.json

Supporting structured data reviewed:
- organisations
- species
- currencies
- buildings
- facilities
- operations
- economic sectors
- research technologies
- world/surface classification collections

## Locked facts used by the game specification

### Planet
Koplin 3 is a breathable temperate homeworld, approximately half land and half water, with three major inhabited continental masses and two moons.

### Continents
Trondonian continent:
- tropical/equatorial;
- biomass rich;
- historic strength in biological management, food and water systems.

Zoran continent:
- temperate;
- faulted and ore rich;
- historic strength in metallurgy, surveying, standards, engineering institutions;
- contains Concordia.

Blaxmar continent:
- polar/sub-polar;
- energy and mineral rich;
- history centred on heat, logistics, communal survival and the Dark Life tradition.

### Concordia
Concordia is the federal capital and a canonical authored expansion. It occupies a temperate, well-watered plain on the Zoran continent. It is defined by distributed government, public institutions, rapid transit, gardens and low-impact advanced infrastructure.

### Atlas
world-atlas-koplin-3:
- 1 km x 1 km canonical tiles;
- origin Federal Forum;
- positive X east;
- positive Y north;
- first authored 100 km²;
- clockwise square-spiral authoring sequence;
- core-to-rural density gradient.

Named central districts already include:
- Federal Forum;
- Exchange Spine;
- Commonwealth Innovation Gardens;
- Ceremonial Axis;
- Old Civic and University Quarter;
- Administrative and University District;
- Residential Garden Quarter;
- Southern Transit District;
- Service and Market districts and further spiral districts in the atlas.

### Visual identity
- high oblique isometric/axonometric reference art;
- white/light-grey advanced structures;
- strong orange accents;
- blue glass;
- clean dark mechanical detail;
- integrated greenery;
- biodomes where appropriate;
- prosperous and mature;
- not ruined, grimy or generic cyberpunk.

### Population and identity
The three peoples are closely related and commonly mixed in modern society. Occupation and conquest remain historical realities but modern identity is not a permanent race-war structure.

### Commonwealth government
- federal;
- rights-based;
- intentionally distributed power;
- local authority over ordinary services;
- higher-level coordination for planetary/intercontinental/off-world/high-risk matters;
- corporations cannot become sovereign.

### AI
- powerful;
- bounded;
- auditable;
- non-sovereign;
- strategic decisions require accountable biological authority;
- fallback and segmentation matter.

### Economy
- materially comfortable but not post-scarcity;
- Commonwealth Credit exists;
- automation is widespread;
- rare/high-purity/strategic materials and major infrastructure retain economic scarcity.

## Canon boundaries the game must respect

The following are NOT currently canonical facts and must not be silently promoted:
- Concordia total population;
- exact municipal boundaries;
- exact number of districts;
- exact city tax code;
- exact city government structure/titles;
- exact local transit technologies beyond the atlas’s maglev/rapid-transit references and broader advanced transport baseline;
- exact household income distribution;
- exact property ownership law;
- exact climate values;
- exact utility capacities;
- exact map-wide road network;
- exact names of most schools, hospitals or neighbourhood institutions.

These are suitable for game-only authored content until approved as Universe expansion.

## Canon versus save state

Universe canon may say “Federal Forum exists.” A save may say:
- it has been pedestrianised;
- an adjacent road was rerouted;
- a transit station was expanded;
- local rent is high;
- 18,432 simulated residents live in the surrounding district;
- the player restored a historic plaza.

Those facts remain in the save.

## Import strategy

The implementation plan should define an importer that:
1. loads Universe collections;
2. resolves IDs;
3. creates immutable game reference records;
4. maps atlas tiles into starting-world chunks;
5. applies game-only authored starting-state data;
6. applies scenario overrides;
7. finally applies mutable save-state deltas.

This order prevents save data or game content from accidentally overwriting canonical identity.

## Canon gaps intentionally left open

The specification deliberately leaves room for future decisions on:
- city population scale;
- off-map regional simulation;
- Commonwealth institutional names specific to city government;
- which canonical organisations have Concordia offices;
- named transport operators;
- named local media;
- exact player title;
- formal heritage register.

Those choices should be made during content planning and, when appropriate, promoted through a separate canon-authoring change.
