# 07 — World, Atlas, Chunks and Streaming

## Coordinate model

Canonical atlas coordinates remain authoritative.

World hierarchy:

```text
Koplin 3
└─ Concordia metropolitan region
   └─ canonical atlas tile (1,000m x 1,000m)
      └─ runtime chunk 4 x 4 grid
         └─ block
            └─ parcel
               └─ building footprint
```

## Runtime chunk key

Each tile is 16 chunks.

```text
ChunkKey:
  atlasTileX : int32
  atlasTileY : int32
  localX     : uint8  // 0..3
  localY     : uint8  // 0..3
```

Local index = `localY * 4 + localX`.

World coordinate origin follows atlas Federal Forum origin.

Use double or integer world coordinates for canonical/spatial bookkeeping if floating-origin precision requires it; local render coordinates may be float.

## Floating origin

A 100 km² region can fit float precision reasonably, but future metropolitan expansion and close placement require planning.

Implement a camera-relative render origin:

- authoritative spatial positions use stable world coordinates;
- render chunks transform into local float coordinates;
- origin shifts never mutate authoritative world location.

## Chunk states

Simulation and rendering have separate states.

Simulation state:

- Dormant external/summary;
- Aggregate;
- Warm;
- Active.

Render state:

- Unloaded;
- Proxy;
- Normal;
- Detailed.

Camera does not directly set simulation state. Relevance is computed from:

- active trips/networks;
- incidents;
- player selection;
- construction;
- scheduled events;
- camera for presentation-only needs.

## Streaming rings

Initial render tuning:

- Detailed: selected chunk + immediate neighbours;
- Normal: several chunks around camera;
- Proxy: visible district/tile horizon;
- Unloaded: beyond useful view.

Exact distances are profiled on device.

## Chunk data ownership

Per chunk:

- terrain semantic grid/mesh reference;
- water/green-blue objects;
- road/network segment membership;
- blocks/parcels;
- building membership;
- environment fields;
- render instance lists;
- dirty flags;
- history/metrics partition references.

Networks crossing boundaries are owned by stable network entities rather than duplicated per chunk.

## Loading process

1. determine required render/simulation chunk set;
2. request chunk data;
3. hydrate/regenerate derived representation;
4. create presentation entities;
5. resolve cross-chunk seams;
6. mark ready;
7. transition old chunks down a representation level;
8. release regenerable resources.

No simulation entity is destroyed merely because its render chunk unloads.

## Initial canonical extent

V1 detailed dataset contains the first 100 canonical atlas tiles (~100 km²).

Not all 100 tiles are loaded simultaneously.

Scenario may initially grant player planning control over a subset while remaining tiles exist as canonical/locked context.

## Terrain representation

Start lightweight:

- deterministic height/elevation source;
- low-poly chunk terrain;
- semantic ground type;
- water polygons/strips;
- vegetation scatter seeds;
- protected/heritage constraints.

Terrain collision is generated only where construction/selection needs it.

## Spatial index

Shared spatial service provides:

- chunk lookup;
- nearest entities;
- rectangle/polygon overlap;
- district membership;
- service candidates;
- environment sample;
- visible render candidates.

Preferred implementation:

- fixed chunk partition at top level;
- per-chunk compact grids/BVH/native structures depending query type.

Do not build one universal heavyweight tree for all domains if specialised local structures are cheaper.

## Dirty propagation

Road edit example:

```text
road segment changed
→ affected chunks dirty
→ nearby block topology dirty
→ parcel topology dirty
→ mobility graph local region dirty
→ utility corridor connections dirty if applicable
→ presentation mesh dirty
→ overlay data dirty
```

Only impacted regions rebuild.

## Boundary continuity

Atlas tile borders use imported continuity metadata plus authored interactive anchors.

Validation checks:

- road exits match;
- transit anchors match;
- water features align;
- major greenways align;
- no duplicate cross-border segment;
- semantic district transition is intentional.

## Save strategy

Authoritative mutable chunk data is partitioned by ChunkKey.

Static canonical source is not redundantly saved unless needed to identify version/override.

Player modifications store deltas/state keyed to stable source IDs.

## Tasks

- IMP-WLD-001 — Implement atlas/world coordinate types.
- IMP-WLD-002 — Implement ChunkKey.
- IMP-WLD-003 — Implement chunk registry/state machine.
- IMP-WLD-004 — Implement camera-relative render origin.
- IMP-WLD-005 — Implement shared spatial lookup.
- IMP-WLD-006 — Load one canonical tile metadata.
- IMP-WLD-007 — Generate 16 runtime chunks.
- IMP-WLD-008 — Render chunk terrain.
- IMP-WLD-009 — Implement render streaming rings.
- IMP-WLD-010 — Implement simulation relevance states.
- IMP-WLD-011 — Implement dirty-region propagation.
- IMP-WLD-012 — Validate edge continuity.
- IMP-WLD-013 — Stream across tile boundary without state discontinuity.
- IMP-WLD-014 — Partition/save mutable chunk state.

## Exit criteria

- same atlas coordinates always produce same ChunkKeys;
- camera can cross tile/chunk boundaries without simulation change;
- unloaded render chunk retains authoritative city state;
- editing one road rebuilds only local affected topology;
- memory does not grow with repeated streaming cycles.
