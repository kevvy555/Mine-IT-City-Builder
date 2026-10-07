# 09 — Primitive Visual Grammar and Rendering

## Intent

Primitive Visual Grammar (PVG) is the initial production rendering strategy, not disposable greyboxing.

It should look like a polished Commonwealth architectural model: clean shapes, strong silhouettes, controlled palette, attractive lighting, activity and readable state.

## Primitive mesh library

Base meshes:

- unit cube;
- unit cylinder;
- unit sphere;
- capsule;
- wedge/prism;
- plane;
- low-poly cone optional;
- spline/extrusion profile library for linear infrastructure.

Meshes are immutable/shared.

Variation comes from transform and instance data.

## Palette

Initial shared materials:

- structure.light;
- accent.orange;
- glass.blue;
- mechanical.dark;
- metal.neutral;
- paving.light;
- paving.dark;
- vegetation.green;
- water.blue;
- construction.warning;
- service.emergency;
- overlay categories.

Material Property Blocks/DOTS material properties or equivalent instance data alter approved tint/emissive values without unique material creation.

## Visual archetype

Example logical definition:

```yaml
id: visual.building.residential.garden-midrise.pvg
representation:
  - primitive: cube
    role: podium
    scaleRule: parcel-podium
    material: structure.light
  - primitive: cube
    role: tower
    scaleRule: floor-mass
    material: glass.blue
  - primitive: cube
    role: accent-spine
    material: accent.orange
  - primitive: cylinder
    role: roof-service
    material: mechanical.dark
stateBindings:
  occupied: emissive-density
  condition: roughness/tint
  powered: emissive-enabled
lod:
  close: full
  mid: merge-minor-parts
  far: proxy
```

The schema is data-driven and deterministic.

## Building grammar

Inputs:

- parcel polygon/footprint;
- building archetype;
- height/floor count;
- use mix;
- district palette;
- deterministic seed;
- construction state;
- operational state.

Outputs:

- primitive parts;
- instance transforms;
- material variation;
- LOD/proxy data;
- selection bounds.

## Visual replacement contract

A simulation building references:

- building archetype ID;
- visual archetype ID.

Renderer resolves visual archetype according to installed content/quality:

```text
visual ID
→ high-quality pack available + quality allows? use HQ asset
→ otherwise PVG renderer
```

Save identity never stores “cube mesh #7.”

## Vehicles

Initial vehicles use 2–6 primitives:

- body cuboid;
- canopy;
- wheel suggestion optional;
- service light/emissive;
- freight body.

No wheel physics.

Scale/dimensions come from simulation vehicle class so later meshes preserve footprint.

## Citizens

Initial representation:

- capsule/body;
- sphere/head;
- simple colour categories;
- optional two-part animation/translation.

At mid/far distance use even cheaper instances or omit visuals.

Citizen ancestry/culture is not encoded as simplistic colour classes.

## Vegetation

Initial trees:

- cylinder trunk;
- one/two sphere/ellipsoid canopy forms.

Shrubs:

- scaled spheres/capsules.

Later vegetation meshes bind to same visual category/placement records.

## Roads

Road rendering uses spline/extruded strips, markings and curb profiles rather than thousands of bespoke meshes.

At minimum:

- road surface;
- sidewalk;
- median;
- lane marking;
- green strip;
- transit/guideway profile.

## Construction states

PVG can visually express stages cheaply:

1. ground highlight/site;
2. foundation slab;
3. frame blocks;
4. massing;
5. finished palette;
6. landscaping/roof modules.

## Simulation state bindings

Visual-only state reads include:

- occupancy/activity;
- power/service status;
- construction;
- condition;
- emergency;
- selection;
- overlay value;
- time of day.

Bindings are one-way. Changing a colour never changes authoritative state.

## LOD

Close:
- full primitive assembly;
- nearby state detail.

Mid:
- fewer parts;
- merged conceptual massing;
- reduced vegetation/agents.

Far:
- chunk/building proxy;
- skyline silhouette and palette.

Metro:
- tile/district proxy with landmarks.

## HLOD generation

PVG enables cheap deterministic proxy generation:

- compute combined bounding/massing;
- preserve important orange/blue/light colour ratios;
- preserve landmarks;
- omit tiny parts;
- generate one/few proxy meshes per chunk.

Generated proxy cache is regenerable, not save-authoritative.

## Rendering budgets

Initial target per detailed chunk:

- shared primitive meshes only;
- bounded material count;
- no per-building GameObject hierarchy;
- instance buffers generated from chunk presentation data;
- dynamic lights limited to important nearby effects;
- shadows limited by band/importance.

Exact numeric budgets in performance document.

## Art upgrade phases

1. PVG only.
2. improved shaders/material palette.
3. high-quality roads/transit.
4. landmark replacements.
5. building family packs.
6. vehicle packs.
7. citizen/vegetation upgrades.

Each phase keeps PVG fallback.

## Tasks

- IMP-REN-001 — Create primitive mesh library.
- IMP-REN-002 — Create palette material library.
- IMP-REN-003 — Define visual archetype schema.
- IMP-REN-004 — Implement PVG assembler.
- IMP-REN-005 — Implement deterministic variation.
- IMP-REN-006 — Implement Entities Graphics instancing.
- IMP-REN-007 — Implement building state bindings.
- IMP-REN-008 — Implement primitive vehicles.
- IMP-REN-009 — Implement primitive people.
- IMP-REN-010 — Implement primitive vegetation.
- IMP-REN-011 — Implement construction visual stages.
- IMP-REN-012 — Implement LOD representation resolver.
- IMP-REN-013 — Implement chunk proxy generation.
- IMP-REN-014 — Implement HQ-asset override/fallback.
- IMP-REN-015 — Android 10k/50k instance benchmark.

## Exit criteria

- one canonical tile is attractive/readable with no bespoke building models;
- thousands of buildings share small mesh/material set;
- save/reload produces identical visual grammar;
- low quality can force PVG even if HQ content exists;
- replacing one archetype with a detailed mesh needs no simulation/save migration.
