# 21 — Buildings, Zoning, Modularity and Architecture

## Building system goals

Buildings must satisfy three competing requirements:
1. simulation entities with clear capacity and inputs;
2. visually varied city fabric that adapts to irregular parcels;
3. performant renderable assets at metropolitan scale.

KCB-BLD-001 — Building simulation identity MUST be separate from its render representation.

KCB-BLD-002 — A building can change visual LOD without losing simulation state.

## Building categories

- residential;
- mixed use;
- retail/service;
- office/research;
- light manufacturing;
- heavy/specialist industry;
- logistics;
- education;
- health;
- civic/government;
- culture/religion;
- utility;
- transit;
- recreation/public realm;
- landmark/heritage.

Each building definition includes:
- semantic use;
- permitted zones/rules;
- parcel requirements;
- footprint generation constraints;
- height/massing range;
- floor allocation;
- capacity formulas;
- jobs/dwellings;
- utility demand;
- freight/service interfaces;
- construction cost/material profile;
- maintenance;
- upgrade/retrofit slots;
- environmental effects;
- art grammar/theme tags.

## Zoning

Zoning is permission and envelope, not a building spawner button.

A zoning rule set defines:
- allowed uses;
- mixed-use combinations;
- minimum/maximum density;
- height;
- setbacks;
- frontage;
- open space;
- servicing;
- environmental/heritage requirements.

KCB-BLD-010 — A parcel may remain undeveloped even when zoned if no viable proposal exists.

KCB-BLD-011 — Re-zoning does not instantly demolish nonconforming buildings; they become lawful-existing/nonconforming until redevelopment or enforcement policy applies.

## Parcel-to-building pipeline

1. compute buildable polygon;
2. apply setbacks/easements;
3. select viable archetypes;
4. calculate floor plate(s);
5. determine height/floor count;
6. select courtyard/terrace/stepback pattern;
7. assign uses by floor/wing;
8. generate facade grammar;
9. place access/service cores;
10. place roof/utility modules;
11. decorate public/private open space;
12. validate capacity and access.

KCB-BLD-020 — Generated geometry MUST be deterministic from building seed plus rule-set version.

KCB-BLD-021 — Invalid tiny residual geometry MUST fall back to a simpler archetype rather than generate broken structures.

## Modular architecture grammar

Architecture uses authored modules:
- structural bays;
- corner pieces;
- facade panels;
- windows;
- balcony/terrace modules;
- podium;
- tower sections;
- roofs;
- service cores;
- bridges;
- biodome elements;
- planters/green roofs;
- mechanical systems;
- entrances;
- loading/service faces.

KCB-BLD-030 — Procedural composition MUST use compatible connection sockets/metrics.

KCB-BLD-031 — Modules have theme tags, scale constraints and repetition limits.

KCB-BLD-032 — Landmark buildings are primarily authored; procedural systems may vary minor props/lighting only.

## Koplin architectural language

Canonical atlas visual language:
- clean white/light-grey primary structures;
- strong but controlled orange accents;
- blue glass;
- dark mechanical detail;
- integrated vegetation;
- glass biodomes where appropriate;
- mature, prosperous, maintained city;
- advanced low-impact infrastructure;
- no generic neon cyberpunk.

KCB-BLD-040 — The base palette is not a requirement that every building be identical. District sub-themes vary form, material ratios and detailing while remaining recognisably Koplin.

### District grammar examples

Federal Forum:
- monumental but distributed civic massing;
- plazas, terraces, water;
- assembly/civic symbols;
- generous setbacks and pedestrian priority.

Exchange Spine:
- dense commercial/research;
- towers/podiums;
- skybridges;
- rooftop gardens.

Innovation Gardens:
- research/cultural;
- elegant institutes;
- biodomes;
- public garden integration.

Old Civic/University:
- older institutional language;
- preserved layers;
- courtyards/archives/observatories;
- less uniform contemporary massing.

Residential Garden:
- mid-rise;
- community facilities;
- waterways;
- pocket green/biodomes.

Transit district:
- large concourses;
- mixed use above/around stations;
- elevated/embedded guideways.

## Building levels and condition

Avoid arbitrary “level 1/2/3” visual growth where inappropriate.

Building evolution dimensions:
- condition;
- efficiency standard;
- occupancy;
- prestige;
- technology generation;
- heritage age;
- extensions.

KCB-BLD-050 — Upgrades alter specific systems, not a generic building level number only.

## Building entrances and interfaces

Every active building defines:
- pedestrian entrance(s);
- service/loading entrance;
- transit internal link optional;
- emergency access;
- utility connection points.

KCB-BLD-060 — Agents route to explicit access points.
KCB-BLD-061 — Inaccessible loading interface constrains freight even if the parcel touches a road.

## Interiors

Full interiors are out of scope for normal simulation.

KCB-BLD-070 — Interior population is abstract capacity.
KCB-BLD-071 — Selected landmark interiors MAY be represented as dedicated scenes later but cannot be required for core simulation.

## Visual state

Building visuals should communicate:
- occupied/vacant;
- daytime/night activity;
- under construction;
- renovation;
- maintenance issue;
- service queue/activity;
- emergency;
- seasonal state;
- prosperity/condition within restrained bounds.

KCB-BLD-080 — State variation SHOULD primarily use material parameters, props, lights and modular swaps rather than unique meshes per state.

## Demolition

KCB-BLD-090 — Demolition checks heritage, occupants, utilities, network dependencies and relocation.
KCB-BLD-091 — Demolition creates a construction process and waste rather than instant disappearance in standard mode.

## Asset scalability

KCB-BLD-100 — Repeated modules/buildings SHOULD use instancing where engine permits.
KCB-BLD-101 — Building materials SHOULD share atlases/trim sheets where that improves batching without sacrificing art quality.
KCB-BLD-102 — Distant representations use merged/HLOD/impostor strategies according to engine choice.

## Acceptance criteria

- Irregular corner parcel produces a coherent building.
- Same grammar yields visible variation without breaking district identity.
- Building can be re-skinned to distant LOD without simulation change.
- Loading entrance affects cargo routing.
- Mixed-use building assigns capacity by floor/use.
- Heritage building can retrofit utilities without losing historical identity.
