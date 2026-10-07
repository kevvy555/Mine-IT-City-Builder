# Research References

Research performed for the Koplin City Builder specification. Sources are listed for design provenance; external mechanics are paraphrased and adapted rather than copied.

## City-builder system design

### Cities: Skylines II — Paradox / Colossal Order
- Features overview: https://www.paradoxinteractive.com/games/cities-skylines-ii/features
- Road tools: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/road-tools
- Traffic AI: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/traffic-ai
- Public and cargo transportation: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/public-cargo-transportation
- Zones and signature buildings: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/zones-signature-buildings
- City services, districts and policies: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/city-services-districts-policies
- Maps and themes: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/maps-themes
- Economy and production: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/economy-production
- Game progression: https://www.paradoxinteractive.com/games/cities-skylines-ii/features/game-progression
- Sound and music diary: https://colossalorder.fi/news/development-diary-12-sound-music/
- Cinematic camera and photo mode: https://colossalorder.fi/news/development-diary-13-cinematic-camera-photo-mode/
- Code modding diary: https://colossalorder.fi/news/development-diary-code-modding/
- Custom assets diary: https://colossalorder.fi/news/development-diary-custom-assets/

Design takeaways used:
- multi-factor pathfinding and traffic agents;
- physical public/cargo transport;
- demand-linked zones;
- services with capacity, efficiency and upgrades;
- economy built from household/business/resource flows;
- information overlays;
- choice-based progression;
- layered/distance-aware city audio;
- stable extension points for modding.

### Foundation — Polymorph Games
- Official Foundation page/wiki: https://wiki.polymorph.games/foundation/Foundation
- Official wiki index: https://wiki.polymorph.games/foundation/Foundation_Wiki

Design takeaways used:
- gridless city building;
- modular buildings;
- organic development;
- procedural generation;
- architecture built for very large numbers of moving parts.

### Anno production-builder references
Research also reviewed production-chain, statistics and blueprint/planning patterns in Anno-style city/production builders. The specification adopts the general ideas of explicit production flow reporting and non-destructive planning, not proprietary formulas or data.

## Procedural urban generation

- Müller, Wonka, Haegler, Ulmer, Van Gool — Procedural Modeling of Buildings, SIGGRAPH 2006: https://doi.org/10.1145/1179352.1141931
- Vanegas, Kelly, Weber, Halatsch, Aliaga, Müller — Procedural Generation of Parcels in Urban Modeling, Computer Graphics Forum 2012: https://doi.org/10.1111/j.1467-8659.2012.03047.x

Design takeaways used:
- rule/grammar-driven building shells;
- road/block/parcel/building pipeline;
- procedural subdivision of irregular urban blocks;
- authored constraints plus procedural variation.

## Large-world rendering and simulation architecture

### Unreal Engine documentation
- World Partition: https://dev.epicgames.com/documentation/en-us/unreal-engine/world-partition-in-unreal-engine
- World Partition HLOD: https://dev.epicgames.com/documentation/unreal-engine/world-partition---hierarchical-level-of-detail-in-unreal-engine
- Instanced Static Mesh Component: https://dev.epicgames.com/documentation/en-us/unreal-engine/instanced-static-mesh-component-in-unreal-engine
- Mass Gameplay overview: https://dev.epicgames.com/documentation/en-us/unreal-engine/overview-of-mass-gameplay-in-unreal-engine
- PCG with World Partition: https://dev.epicgames.com/documentation/unreal-engine/using-pcg-with-world-partition-in-unreal-engine

Design takeaways used:
- spatial grid streaming;
- distant proxy/HLOD rendering;
- instancing repeated geometry;
- data-oriented entities;
- separate visual and simulation LOD;
- optional “no representation” for distant agents.

### Unity documentation
- GPU instancing: https://docs.unity3d.com/Manual/GPUInstancing.html
- CullingGroup distance bands: https://docs.unity3d.com/ScriptReference/CullingGroup.SetBoundingDistances.html
- Occlusion culling: https://docs.unity3d.com/Manual/OcclusionCulling.html
- Adaptive Performance: https://docs.unity.com/en-us/engine/6000.5/manual/analysis/runtime-performance-scaling/adaptive-performance/introduction

Design takeaways used:
- shared mesh/material instancing;
- distance-based behaviour/representation bands;
- frustum/occlusion-aware rendering;
- thermal/power-aware quality scaling on constrained hardware.

## Accessibility

Microsoft Xbox Accessibility Guidelines:
- index: https://learn.microsoft.com/gaming/accessibility/guidelines
- text display: https://learn.microsoft.com/gaming/accessibility/xbox-accessibility-guidelines/101
- contrast: https://learn.microsoft.com/gaming/accessibility/xbox-accessibility-guidelines/102
- screen narration: https://learn.microsoft.com/gaming/accessibility/xbox-accessibility-guidelines/106

Referenced targets include:
- meaningful text/UI should meet strong contrast expectations;
- standard important text/visual elements target at least 4.5:1 contrast;
- large elements target at least 3:1;
- high-contrast mode can target 7:1;
- important visual-only information needs an alternate/narratable representation;
- UI, notifications, maps and key values should be considered for narration.

## Research limitations

- Competitor public design material describes intended systems, not necessarily every shipped implementation detail.
- Engine documentation informs architectural techniques, not an engine decision for Koplin.
- Numerical balance values in this specification are original tuning baselines unless explicitly identified as canon.
- No external source overrides MineIT Universe canon.
