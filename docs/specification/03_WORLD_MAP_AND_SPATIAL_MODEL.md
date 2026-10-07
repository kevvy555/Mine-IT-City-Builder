# 03 — World Map and Spatial Model

## Spatial hierarchy

The game world is organised into nested spatial units so simulation, rendering, saves and UI can scale independently.

1. **Planet context** — Koplin 3; global climate, external regions and intercontinental links.
2. **Metropolitan region** — Concordia plus surrounding settlements, countryside and infrastructure.
3. **Canonical atlas tile** — 1 km x 1 km; stable coordinate aligned with world-atlas-koplin-3.
4. **Simulation chunk** — implementation-defined subdivision used for streaming and update scheduling.
5. **Block** — bounded by rights of way, water or immutable terrain.
6. **Parcel** — developable property/lot within a block.
7. **Footprint/cell** — local geometry used by buildings, paths and infrastructure.
8. **Interior slot** — abstract capacity where detailed interiors are not physically modelled.

KCB-MAP-001 — Atlas tile coordinates MUST remain stable across saves and game versions.

KCB-MAP-002 — Simulation chunks MAY subdivide atlas tiles but MUST have deterministic IDs derived from atlas coordinate plus local index.

KCB-MAP-003 — Save data MUST reference stable spatial IDs instead of array positions.

TUNING BASELINE — Each canonical 1 km x 1 km atlas tile is subdivided into a deterministic 4 x 4 grid of 250 m x 250 m runtime chunks, giving 16 runtime chunks per atlas tile.

KCB-MAP-004 — Runtime chunk IDs MUST derive from canonical atlas coordinate plus local 0–15 chunk index and MUST remain stable across save/load.

KCB-MAP-005 — Runtime chunk subdivision is an implementation/streaming unit and MUST NOT alter canonical atlas identity or coordinates.

## Canonical start area

The Concordia scenario starts from the canonical atlas, with the Federal Forum at 0,0.

KCB-MAP-010 — Imported atlas tiles MUST retain districtName, zone, ring, description and canonical image reference metadata.

KCB-MAP-011 — Atlas edge-continuity data SHOULD inform road/greenway/transit reconstruction where interactive geometry is authored.

KCB-MAP-012 — The canonical artwork is a visual reference, not collision geometry. Interactive roads/buildings require authored or procedurally reconstructed game geometry.

KCB-MAP-013 — Where artwork and future interactive geometry differ slightly for playability, semantic identity and major axes take precedence over pixel matching.

## Playable extent

The world should feel metropolitan without requiring the entire planet to be loaded.

TUNING BASELINE:
- initial detailed area: first 100 atlas tiles, approximately 100 km²;
- expandable detailed metropolitan region: at least several hundred km²;
- off-map region: aggregated Zoran/planetary connections.

KCB-MAP-020 — Detailed area expansion MUST occur through explicit acquisition/planning jurisdiction or scenario rules.

KCB-MAP-021 — Off-map external connections MUST model passengers, freight, utilities where relevant, fiscal transfers and interregional demand without requiring full off-map agent simulation.

KCB-MAP-022 — The map edge MUST not behave as a magical source/sink. Each external connector has identity, type, capacity, travel cost and market/service parameters.

## Terrain

Terrain layers:
- elevation;
- slope;
- soil/ground suitability;
- hydrology;
- vegetation;
- protected ecology;
- geology/resource relevance where gameplay needs it;
- heritage/archaeology constraints;
- flood/water-management zones.

KCB-MAP-030 — Construction cost MUST react to slope, ground conditions and required earthworks.

KCB-MAP-031 — Roads and rails MUST preview cut, fill, bridge, tunnel and retaining requirements.

KCB-MAP-032 — Water flow MAY be simplified but must be spatially coherent enough for drainage, flood risk and water features.

KCB-MAP-033 — Terrain editing MUST create a reversible plan preview before committing destructive changes.

## Water and green-blue infrastructure

Concordia’s atlas explicitly uses landscaped water and greenways.

KCB-MAP-040 — Rivers, canals, retention areas, wetlands, planted corridors and public-space water features MUST be first-class map objects, not decorative decals only.

KCB-MAP-041 — Green-blue corridors MAY simultaneously affect drainage, cooling, recreation, ecology, walking/cycling accessibility and land desirability.

KCB-MAP-042 — Culverting, rerouting or removing significant water features SHOULD create cost, ecological and political consequences.

## Blocks and parcels

Road/path/water boundaries generate blocks. A block is subdivided into parcels according to frontage, depth, shape, zoning and protected features.

KCB-MAP-050 — Parcel generation MUST support irregular polygons.

KCB-MAP-051 — Every developable parcel MUST have a legal access path or explicit easement.

KCB-MAP-052 — Parcel subdivision SHOULD prefer street frontage and avoid unusable slivers; residual land can become service alleys, landscaping, micro-parks or merge candidates.

KCB-MAP-053 — Parcel topology MUST be recalculated incrementally when nearby roads/rights of way change.

KCB-MAP-054 — Parcel IDs SHOULD persist where geometry remains materially the same so buildings and histories do not churn after minor road edits.

## Districts

Districts are overlapping semantic areas rather than one universal partition.

Possible layers:
- administrative district;
- planning district;
- service district;
- transit fare/operating district;
- heritage district;
- environmental protection district;
- statistical neighbourhood;
- player-named area.

KCB-MAP-060 — District layers MUST be independently editable where legally sensible.

KCB-MAP-061 — A building can belong to multiple district layers.

KCB-MAP-062 — District boundaries SHOULD snap optionally to roads, water and parcel edges but MAY be freeform.

## Addresses and place identity

KCB-MAP-070 — Buildings and parcels SHOULD receive stable human-readable addresses or location descriptors.

KCB-MAP-071 — Player-named roads, districts, parks and stations MUST persist independently from procedural display labels.

KCB-MAP-072 — Renaming MUST not break simulation references.

## Construction occupancy

Every spatial object has one of:
- existing/canonical;
- proposed;
- under construction;
- active;
- suspended;
- condemned;
- demolition queued;
- removed/archived.

KCB-MAP-080 — Proposed objects do not consume full operational capacity.
KCB-MAP-081 — Construction sites occupy land and affect traffic before completion.
KCB-MAP-082 — Demolition can create temporary waste/logistics traffic.

## Spatial queries

The simulation architecture MUST support:
- nearest reachable service;
- travel-time catchment;
- parcel suitability;
- local density;
- local externalities;
- district membership;
- utility connectivity;
- network reachability;
- flood/environment exposure;
- visible entities by camera/frustum;
- entities within simulation LOD bands.

KCB-MAP-090 — These queries MUST use shared spatial indices rather than each subsystem scanning the whole city.

## Acceptance criteria

- Loading the same atlas seed produces the same tile/chunk IDs.
- Curved roads can create usable irregular parcels.
- A road deletion invalidates only affected local blocks/parcels.
- A disconnected parcel cannot develop without access.
- District overlays remain correct after road edits.
- Moving the camera across tile boundaries produces no semantic discontinuity in city state.
