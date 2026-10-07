# Research — City-Builder Genre Findings

## Purpose

This document records design conclusions from research into established city builders, simulation architecture, procedural urban generation and large-world rendering. It is not a feature-comparison checklist. The goal is to identify techniques that solve recurring city-builder problems and then adapt them to Koplin.

## 1. Deep simulation is strongest when causality is inspectable

Cities: Skylines II models households, businesses, service institutions, resources and physical travel as interacting agents. The important lesson is not to copy its exact formulas; it is that the game exposes flow, volume, budgets, service efficiency, production surplus/deficit and entity-level needs through dedicated info views.

Koplin conclusion:
- model enough underlying causality to create emergent outcomes;
- provide overlays and selected-entity explanations for every important outcome;
- surface actionable bottlenecks rather than requiring players to infer invisible formulas.

## 2. Transport is both geometry and economics

Modern city builders increasingly treat route cost as more than distance. Travel time, congestion, mode choice, parking, cargo access and network hierarchy affect where people and businesses can function.

Koplin conclusion:
- accessibility is a first-class value used by housing, employment, commerce, services and land value;
- the route graph is shared infrastructure, not a traffic-only minigame;
- cargo and passenger flows need different priorities and network options.

## 3. Organic urbanism requires more than square zoning cells

Foundation demonstrates the appeal of gridless growth and procedural/modular buildings. Procedural urban research commonly separates road generation, block detection, parcel subdivision and building generation.

Koplin conclusion:
- roads define blocks;
- blocks generate buildable parcels;
- parcel frontage/depth/shape and planning rules determine viable development;
- procedural buildings use authored kits/grammars so irregular parcels still look intentional;
- direct placement remains available for civic and special buildings.

## 4. Players need planning tools, not only bulldozers

Blueprint/ghost-building approaches in production-focused builders show that planning before committing resources reduces friction. Modern road tools also emphasise snapping, curves, grids, replacement/upgrades and previewing.

Koplin conclusion:
- every expensive construction tool needs preview;
- plans can exist unfunded;
- conflicts, demolition, gradients, capacity and estimated cost are visible before confirmation;
- upgrade/replace workflows preserve corridors when possible.

## 5. Simulation scale requires level of detail

Large-world engine documentation converges on the same techniques:
- spatial partition/streaming;
- hierarchical level of detail for distant geometry;
- mesh instancing for repeated assets;
- different visual representations for agents at different distances;
- different simulation update frequencies for less relevant entities.

Koplin conclusion:
- simulation LOD and rendering LOD are designed independently;
- distant districts continue economically at aggregate resolution;
- camera proximity increases animation/agent fidelity but must not change macro outcomes;
- repeated buildings, props, vegetation and vehicles use instancing/batching;
- world chunks align with stable save/spatial IDs.

## 6. Population does not require one heavyweight object per citizen

Data-oriented/entity systems can represent huge populations more efficiently than object-heavy models. Representation systems can switch entities between full actors, lightweight instances and no rendered representation.

Koplin conclusion:
- persistent citizens can exist as compact records;
- only a bounded active set needs detailed route/animation state simultaneously;
- cohorts may be used for macro calculation while sampled individuals give visible life and narrative continuity;
- important named citizens can retain higher persistence/detail.

## 7. Procedural graphics need constrained variation

GPU instancing gains efficiency by sharing mesh/material data; procedural-building research gains visual variety by composing rules and modules.

Koplin conclusion:
- use a finite high-quality architectural kit with controlled parameter variation;
- vary footprint, height, facade modules, roof equipment, vegetation, signage shapes, wear and occupancy lighting;
- preserve district visual grammar so randomisation does not become visual noise;
- reserve unique geometry for landmarks.

## 8. Service placement becomes interesting when capacity, logistics and efficiency matter

A sophisticated service building is not only a radius. Services can have:
- passive local influence;
- finite throughput;
- mobile response units;
- travel time;
- staffing;
- utility/input requirements;
- upgrades;
- maintenance;
- district assignment.

Koplin conclusion:
use a shared service model so hospitals, schools, fire response, maintenance, waste and civic services are system variations rather than bespoke one-off code.

## 9. Progression should unlock choices rather than force population grinding

A development-tree approach gives players agency in what capability to prioritise.

Koplin conclusion:
- Concordia already possesses mature technology, so classic “unlock electricity at 5,000 population” is inappropriate;
- progression should instead represent budget authority, modernisation programmes, political mandate, specialised infrastructure and access to advanced projects;
- sandbox can unlock everything.

## 10. City sound should carry state

City-builder sound design benefits from layered ambience, local service/building sounds, traffic, weather and distance-aware source management.

Koplin conclusion:
- audio is an information layer;
- overloads, emergencies, dense activity, quiet parks, transit hubs and weather should sound different;
- source grouping/LOD prevents thousands of individual emitters.

## 11. Accessibility must be planned into overlays

City builders rely heavily on colour maps and dense text, which can create barriers.

Koplin conclusion:
- all overlays need non-colour encodings;
- scalable text and UI;
- high-contrast option;
- keyboard/controller/touch navigation plans;
- narration metadata for critical menus and values;
- reduced motion and flashing controls.

## 12. Modding is easier when systems register through explicit extension points

Colossal Order’s published modding approach highlights the cost of mods patching internal code and the value of stable system/component APIs.

Koplin conclusion:
- data schemas and simulation messages should be explicit;
- content packs should extend catalogues without overwriting base IDs;
- simulation hooks should be versioned;
- save files must record mod dependencies and versions.

## Research-to-design rule

KCB-RSCH-001 — Research findings may inspire mechanisms, but the implementation MUST be original and adapted to MineIT canon.

KCB-RSCH-002 — No competitor’s proprietary data, art, text or exact balancing values are to be copied.

KCB-RSCH-003 — External research sources and dates are retained in REFERENCES.md so future reviewers can distinguish evidence from internal design choices.
