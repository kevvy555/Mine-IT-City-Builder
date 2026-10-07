# 22 — Art Direction and Rendering

## Visual target

The game should make Concordia look like the canonical atlas has become alive. The target is polished AAA city-builder readability, not photorealism at any cost.

Visual priorities in order:
1. city-scale readability;
2. coherent Koplin identity;
3. attractive close viewing;
4. clear simulation state;
5. stable performance.

KCB-ART-001 — The canonical atlas style is the primary visual reference for Concordia.

KCB-ART-002 — Rendering technology MUST serve readability and scale; expensive effects that obscure gameplay are optional.

## Camera-relative detail model

Define at least four visual bands.

### Band A — close/street
- highest building mesh/detail;
- pedestrians and local vehicles;
- facade interior/parallax details;
- small props;
- local VFX;
- animated doors/equipment selectively;
- high-quality shadows nearby.

### Band B — neighbourhood
- normal building LOD;
- representative pedestrians;
- traffic visible;
- medium props;
- vegetation clusters;
- reduced animation.

### Band C — district
- simplified building geometry;
- HLOD/merged clusters where appropriate;
- major traffic ribbons/representative movement;
- only large vegetation/features;
- simplified shadows.

### Band D — metro
- skyline/HLOD;
- major transport motion;
- water/terrain;
- district lighting/activity;
- no individual small agents required.

KCB-ART-010 — LOD transition MUST avoid visible popping through cross-fade, dither, geometry morph or sensible thresholding where supported.

KCB-ART-011 — Simulation LOD and visual LOD are independent.

## Modular asset strategy

Use:
- modular building kits;
- trim sheets;
- shared material libraries;
- decal libraries;
- instanced props;
- procedural dressing;
- authored landmarks.

KCB-ART-020 — Repetition is broken with controlled per-instance variation:
- material tint within theme range;
- window-light pattern;
- facade module permutation;
- balcony/planter selection;
- roof equipment;
- tree/prop seed;
- minor scale offsets where safe.

KCB-ART-021 — Variation MUST be deterministic so save/load does not visually reshuffle the city.

## Materials

Physically based material workflow SHOULD use:
- base colour/albedo;
- roughness;
- metallic where relevant;
- normal detail;
- ambient/occlusion or engine equivalent;
- emissive;
- optional height/parallax for close surfaces.

Core material families:
- pale advanced composite/stone;
- blue architectural glass;
- orange accent composite;
- dark machinery;
- metals;
- paving;
- water;
- vegetation;
- heritage materials.

KCB-ART-030 — Bright white structures MUST preserve surface detail through roughness/normal variation and lighting; avoid flat blown-out white.

## Glass

Blue glass is signature but can become expensive/noisy.

KCB-ART-040 — Use scalable glass tiers:
- close transparent/reflection-capable;
- mid simplified reflective/opaque approximation;
- far baked/material approximation.

KCB-ART-041 — Interior illusion can use cubemaps, parallax interiors or emissive room masks instead of real interiors.

## Lighting

Desired tone:
- bright temperate daylight;
- legible shadows;
- clean colour separation;
- warm/cool variation across day;
- attractive night lighting without neon overload.

KCB-ART-050 — Primary sun/directional light defines city form.
KCB-ART-051 — Global illumination method is engine/platform dependent, but distant city must not require per-building dynamic lights.
KCB-ART-052 — Window/city night lighting SHOULD be aggregated via emissive masks and clustered techniques where possible.
KCB-ART-053 — Important service/emergency lights use bounded real lights/VFX near camera.

## Shadows

KCB-ART-060 — Shadow quality scales by distance and object importance.
KCB-ART-061 — Small props lose dynamic shadows before major architecture.
KCB-ART-062 — Cascaded/virtual/shadow-map choice is engine dependent.

## Vegetation

Concordia integrates greenery heavily.

Techniques:
- authored species sets;
- instanced trees/shrubs;
- biome/district density maps;
- deterministic scatter;
- LOD/impostors;
- seasonal material/mesh variants;
- wind animation limited by distance.

KCB-ART-070 — Vegetation placement must respect roads, sight lines, entrances and infrastructure.
KCB-ART-071 — Trees are not only decorative where ecology/shade systems reference them; simulation uses lightweight spatial data separate from render meshes.

## Roads and surfaces

KCB-ART-080 — Roads use spline/decal/material systems that minimise unique meshes.
KCB-ART-081 — Lane markings, crossings, curb treatment and service markings are theme-aware.
KCB-ART-082 — Wear should be subtle in a maintained capital and intensify with actual condition.

## Decals and storytelling

Decals/props can show:
- maintenance;
- construction;
- transit wayfinding;
- civic decoration;
- water staining/wear;
- event setup.

KCB-ART-090 — No readable text is required in world textures for core readability; signage may use abstract symbols and game UI can provide labels.

## Water

Water rendering needs:
- scalable reflections;
- shoreline blending;
- depth/turbidity variation;
- flow hints where relevant;
- weather response.

KCB-ART-100 — Water visuals reflect water-quality state only at severe/meaningful thresholds, not cartoonishly.

## Weather and atmosphere

Layers:
- clouds/sky;
- rain;
- wet surfaces;
- puddles selectively;
- fog/haze;
- wind vegetation;
- temperature/season tint;
- lightning if appropriate.

KCB-ART-110 — Weather effects MUST have quality tiers.
KCB-ART-111 — Wetness can be driven by material parameter rather than duplicate wet assets.

## Construction visuals

Stages should visibly progress:
- site fencing/markers;
- earthworks;
- foundations;
- frame;
- envelope;
- fit-out;
- landscaping;
- operational.

KCB-ART-120 — Construction geometry SHOULD use reusable stage kits to avoid bespoke assets for every building.

## Crowds and vehicles

KCB-ART-130 — Close agents may use skinned/animated characters.
KCB-ART-131 — Mid-distance agents SHOULD use cheaper animation/vertex animation/batched representation.
KCB-ART-132 — Far agents may be omitted or represented statistically/through aggregate motion.
KCB-ART-133 — Vehicle variation uses shared meshes with liveries/instance data where possible.

## Instancing and batching

Industry research and engine documentation show repeated urban geometry is a prime case for instancing.

KCB-ART-140 — Repeated static props, vegetation and modular building parts SHOULD be rendered through instancing/batching.
KCB-ART-141 — Material proliferation is budgeted; unique materials require justification.
KCB-ART-142 — Per-instance custom data SHOULD drive allowed variation instead of cloning materials.

## HLOD / district proxies

KCB-ART-150 — Distant chunks SHOULD be replaceable by merged/simplified proxy geometry.
KCB-ART-151 — Proxy generation must preserve skyline silhouette, key landmarks and major colour blocks.
KCB-ART-152 — Landmark buildings may remain separate at longer distances.

## Occlusion and culling

KCB-ART-160 — Frustum culling is mandatory.
KCB-ART-161 — Occlusion culling/Hi-Z/engine equivalent SHOULD be evaluated especially in dense street canyons.
KCB-ART-162 — Culling solution must support a dynamically changing city; techniques requiring fully baked static geometry cannot be the sole method.

## Level streaming

KCB-ART-170 — World geometry is partitioned spatially.
KCB-ART-171 — Camera and optional background simulation sources determine loaded visual chunks.
KCB-ART-172 — Unloaded detailed chunks retain HLOD/summary visual representation where visible.

## Post processing

Allowed subtle effects:
- tone mapping;
- exposure;
- bloom restrained;
- ambient occlusion;
- colour grading;
- depth of field only in photo/cinematic mode by default;
- motion blur configurable/off by default for accessibility preference;
- anti-aliasing/upscaling engine dependent.

KCB-ART-180 — Gameplay camera MUST prioritise crisp UI/map readability over cinematic blur.

## Visual state overlays

Heatmaps should temporarily override/simplify materials:
- traffic;
- utilities;
- land value;
- pollution;
- services;
- zoning;
- environment.

KCB-ART-190 — Overlay rendering MUST not require material duplication per building.
KCB-ART-191 — Overlay legends must use shape/pattern/value in addition to colour where practical.

## Atlas-to-game art workflow

1. import atlas tile metadata;
2. review canonical reference image;
3. author/reconstruct terrain and major network anchors;
4. place landmarks and district grammar volumes;
5. generate/author parcels;
6. seed building kit rules;
7. validate skyline and edge continuity;
8. create HLOD;
9. compare against atlas for identity, not pixel-perfect tracing.

KCB-ART-200 — Every canonical tile has a visual-reference capture for art QA.

## Performance budgets

Exact numbers are platform dependent and must be fixed in implementation planning, but budgets MUST exist for:
- frame time CPU/GPU;
- draw calls;
- visible triangles;
- material/shader variants;
- dynamic lights;
- shadow casters;
- skinned agents;
- particles;
- texture memory;
- streaming bandwidth;
- loaded chunks.

KCB-ART-210 — Content cannot be approved without performance cost classification.

## Quality tiers

At minimum:
- low;
- medium;
- high;
- ultra/desktop optional.

Scalable features:
- crowd density;
- vehicle representation;
- shadow distance;
- vegetation detail;
- reflections;
- weather particles;
- HLOD distances;
- animation density;
- texture resolution;
- post effects.

KCB-ART-220 — Lower tiers reduce representation before removing simulation.

## Acceptance criteria

- A canonical tile is recognisable from district layout/palette at gameplay camera distance.
- Thousands of repeated props do not require thousands of unique materials.
- Night city remains attractive without one dynamic light per window.
- Zooming from street to metro does not produce obvious world disappearance.
- Overlay mode remains readable over bright white/blue-glass architecture.
- Low quality preserves simulation and essential visual cues.
