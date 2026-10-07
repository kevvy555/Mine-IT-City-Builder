# 32 — Content Catalogue and Asset Requirements

## Purpose

This document defines the content families the implementation plan must account for. Exact item counts are production estimates and should be refined after engine/procedural-tool decisions.

## Canon world content

Required imports:
- Koplin 3 planet metadata;
- Concordia settlement;
- Koplin 3 atlas;
- atlas tiles and edge continuity;
- three peoples;
- Commonwealth;
- currency;
- relevant land/biome/hydrosphere vocabularies;
- relevant organisations/materials/technology references.

KCB-CONT-001 — Imported canon retains original IDs and source attribution metadata where available.

## Terrain/environment assets

Families:
- temperate Zoran terrain materials;
- river/lake/canal;
- wetlands;
- grassland;
- woodland;
- managed park;
- garden planting;
- peri-urban agriculture;
- rock/fault outcrops;
- retaining/embankment;
- drainage/stormwater.

Needed:
- close material;
- blend masks;
- decals;
- vegetation sets;
- LOD/impostors;
- seasonal variants as supported.

## Road/public-realm kit

- local streets;
- boulevard;
- arterial;
- pedestrian street;
- transit mall;
- cycle/greenway;
- bridges;
- tunnel portals;
- retaining walls;
- intersections;
- crossings;
- curbs;
- lighting;
- benches;
- barriers;
- planters;
- water-edge pieces;
- civic plaza pieces.

KCB-CONT-010 — Road kit uses modular/spline-compatible assets.

## Transit kit

- stops;
- shelters;
- surface platforms;
- major stations;
- interchange;
- guideway;
- elevated supports;
- tunnel portals;
- depots/yards;
- vehicles;
- signage symbols;
- maintenance props.

## Building kit families

### Civic/Federal
- assembly/government;
- ministry/admin;
- courts/rights institutions game-only if authored;
- public offices;
- civic pavilions.

### Research/University
- teaching blocks;
- laboratories;
- archives;
- observatory structures;
- biodomes;
- research towers.

### Residential
- low-rise courtyard;
- mid-rise garden blocks;
- tower/podium;
- mixed-use;
- cooperative/public;
- student/specialist.

### Commercial/Office
- market/service;
- retail podium;
- office;
- innovation tower;
- conference/hospitality.

### Service
- clinic/hospital;
- school;
- emergency;
- maintenance depot;
- community;
- cultural.

### Industry/Logistics
- clean fabrication;
- precision;
- warehouse;
- distribution;
- utility plant;
- recycling/recovery;
- construction support.

KCB-CONT-020 — Each procedural family needs enough facade/roof/corner variation to avoid obvious repetition at neighbourhood scale.

## Landmark set

At minimum, central canonical area requires authored hero assets or hero-quality procedural assemblies for:
- Federal Forum assembly/civic centre;
- major maglev/interchange;
- key university/archive/observatory;
- key museum/memorial complex;
- Innovation Gardens research/culture landmark.

KCB-CONT-030 — Landmark list must be reconciled against atlas tile descriptions before production lock.

## Character assets

People need:
- Trondonian/Zoran/Blaxmar visual representation that remains compatible with close shared ancestry;
- broad age ranges;
- mixed ancestry presentation;
- clothing categories;
- mobility aids;
- service uniforms;
- construction/maintenance;
- cultural/event variants.

KCB-CONT-040 — Visual design MUST avoid caricature based on species.

KCB-CONT-041 — Character system should maximise modular combinations and LOD reuse.

## Vehicle assets

- public transit by mode;
- service/emergency;
- delivery;
- freight;
- construction;
- private/shared mobility;
- maintenance;
- optional government/special vehicles.

KCB-CONT-050 — Vehicle classes need simulation collision/size data independent from render mesh.

## Props

- street furniture;
- bollards/barriers;
- utility cabinets;
- transit equipment;
- market/event props;
- construction;
- maintenance;
- waste/recycling;
- park;
- waterfront;
- campus;
- rooftop;
- accessibility.

KCB-CONT-060 — Props are tagged for compatible surfaces/districts and instancing.

## VFX

- rain;
- mist;
- drainage;
- construction dust restrained;
- steam/heat;
- clean industrial exhaust where relevant;
- emergency;
- utility arcs/sparks restrained;
- water fountains;
- transit effects;
- holographic/UI-like in-world effects sparingly.

KCB-CONT-070 — Effects cannot replace necessary UI feedback.

## Audio assets

- ambience by district;
- traffic/transit;
- service buildings;
- utilities;
- weather;
- crowds;
- construction;
- emergency;
- UI;
- music;
- optional radio/news.

Each needs distance/LOD strategy.

## UI assets

- icons for every tool/service/resource/overlay;
- alert severity;
- policy;
- accessibility patterns;
- charts;
- cursor/tool handles;
- map symbols.

KCB-CONT-080 — Icons need vector/resolution-independent source where possible.

## Data catalogues

Game data must define:
- road templates;
- lane types;
- transit modes/vehicles;
- zone rules;
- building archetypes;
- modules;
- utility assets;
- service definitions;
- policies;
- technology programmes;
- production recipes;
- event templates;
- difficulty presets;
- scenario definitions;
- audio mappings;
- art theme/grammar definitions.

## Localisation content

Every content item needs:
- display name key;
- short description;
- long/help description where needed;
- lore/canon reference optional;
- accessibility label;
- units.

## Production statuses

Asset/content lifecycle:
- concept;
- blockout;
- gameplay-ready;
- art alpha;
- art final;
- LOD/performance complete;
- accessibility/localisation complete;
- validated;
- released.

KCB-CONT-090 — “Art final” is not “done” until LOD/collision/performance and data bindings are complete.

## Asset naming and IDs

KCB-CONT-100 — Asset filename and content stable ID are separate.
KCB-CONT-101 — IDs are lower-case stable slugs/namespaces per project standard.
KCB-CONT-102 — Renaming an asset file does not require changing persistent game identity.

## Initial production estimate categories

Implementation planning should produce exact counts for:
- road cross-sections;
- intersection modules;
- transit modes;
- vehicle meshes;
- building procedural families;
- facade modules;
- hero landmarks;
- vegetation species;
- props;
- UI icons;
- sound groups;
- music tracks;
- scenarios.

The estimate must include LOD work, not only base meshes.

## Acceptance criteria

- Every gameplay feature in the vertical slice maps to required art/data/audio assets.
- No canonical central district relies on generic placeholder architecture at art lock.
- Procedural family can generate at least dozens of visibly distinct valid buildings.
- Content validator detects missing asset/localisation references.
