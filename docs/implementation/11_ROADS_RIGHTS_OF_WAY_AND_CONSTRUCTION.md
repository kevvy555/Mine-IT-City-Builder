# 11 — Roads, Rights of Way and Construction

## Core model

A road is not a decorative mesh. The authoritative object is a **right-of-way corridor** with geometry, cross-section, permissions, infrastructure slots and ownership/state.

A corridor can carry:

- pedestrian space;
- cycle/micromobility;
- general vehicle lanes;
- service/emergency lanes;
- bus lanes;
- tram/guideway;
- landscaping/median;
- utilities;
- drainage;
- loading/curb allocation.

## Geometry

Store road alignment as deterministic control data:

- stable road ID;
- ordered control points in world coordinates;
- tangent/curve parameters;
- elevation mode;
- cross-section template ID;
- construction/lifecycle state.

Generated meshes and lane nodes are derived.

Use piecewise cubic Bezier/Hermite splines with deterministic sampling thresholds.

Do not save every generated vertex.

## Cross-section templates

Game-authored templates define lateral slots.

Example:

```text
sidewalk | trees | lane← | lane→ | median | lane← | lane→ | trees | sidewalk
```

Each slot contains:

- type;
- width;
- allowed modes;
- direction;
- surface;
- speed/capacity;
- utility/drainage compatibility;
- visual profile.

Templates are versioned content IDs.

## Junction generation

When corridor centre lines meet:

1. detect intersection;
2. split participating segments;
3. create stable junction ID;
4. build lane/mode connectivity;
5. generate turning movements;
6. apply control policy;
7. generate presentation geometry;
8. invalidate local routing cache.

Control types:

- priority/uncontrolled;
- signalised;
- roundabout;
- pedestrian priority;
- transit priority;
- service/emergency rules.

## Road graph separation

Authoritative corridor geometry and mobility graph are separate derived layers.

A single corridor may generate:

- pedestrian edges;
- cycle edges;
- road-lane edges;
- transit guideway edges;
- utility conduit edges.

This allows a cross-section change without replacing corridor identity.

## Editing

Supported operations:

- create;
- extend;
- insert control point;
- move control point;
- change curve;
- change cross-section;
- elevate/lower;
- bridge/tunnel state;
- upgrade;
- close/restrict;
- demolish.

Edits use preview geometry and impact analysis before commit.

## Snapping

Snap candidates:

- existing control point;
- corridor tangent;
- junction;
- parcel/block edge;
- grid;
- canonical atlas anchor;
- transit/utility connector.

Precision options:

- angle lock;
- grid lock;
- tangent lock;
- fixed length;
- fixed radius.

## Construction preview

Before commit show:

- cost;
- land acquisition;
- demolition/displacement;
- earthworks;
- bridge/tunnel requirement;
- affected parcels;
- affected utilities;
- expected network change;
- protected/heritage conflict;
- invalid geometry reason.

Expensive impact calculations can stream progressively but commit is disabled until required validation completes.

## Construction lifecycle

Committed road project creates:

- project ID;
- approved geometry;
- budget commitment;
- staged construction;
- temporary traffic/access changes;
- freight/material demand where enabled.

Stages may initially be simplified but must preserve non-instant construction contract.

## Incremental rebuild

A road edit computes an affected bounding region.

Rebuild only:

- touched corridor segments;
- nearby junctions;
- blocks whose boundary changed;
- affected parcels;
- graph nodes/edges in local region;
- presentation mesh in dirty chunks;
- overlays depending on those networks.

Whole-city graph rebuild is forbidden for normal local edits.

## Bridges and tunnels

V1 data model supports them from first road schema even if art/tools arrive later.

Vertical profile stores:

- surface;
- bridge;
- tunnel;
- retained/cut/fill segment.

Pathfinding uses explicit connectivity, not visual overlap.

## Curb/loading allocation

Curb is finite infrastructure.

Segment side allocation may be:

- pedestrian frontage;
- loading;
- transit stop;
- service access;
- parking/shared mobility;
- landscaping;
- no stopping.

Freight/service systems query these interfaces.

## Undo

Before simulation construction begins, committed planning changes may be cleanly undone.

Once construction/history effects occur, “undo” becomes cancellation/demolition with real consequences unless in explicit sandbox/editor mode.

## Data and algorithms

- deterministic integer/quantised 2D geometry for topology where practical;
- float mesh generation derived from topology;
- robust geometric epsilon constants centralised;
- spatial index for intersections/snapping;
- local graph version numbers for cache invalidation.

## Tasks

- IMP-RD-001 — Define corridor/right-of-way schema.
- IMP-RD-002 — Define cross-section schema.
- IMP-RD-003 — Implement spline alignment.
- IMP-RD-004 — Implement touch road preview.
- IMP-RD-005 — Implement snap service.
- IMP-RD-006 — Implement intersection detection/splitting.
- IMP-RD-007 — Implement junction connectivity.
- IMP-RD-008 — Generate pedestrian/road graph edges.
- IMP-RD-009 — Generate PVG road mesh.
- IMP-RD-010 — Implement validation/cost preview.
- IMP-RD-011 — Implement local dirty-region rebuild.
- IMP-RD-012 — Implement road upgrade.
- IMP-RD-013 — Implement closure/demolition.
- IMP-RD-014 — Add blueprint/undo integration.
- IMP-RD-015 — Add bridge/tunnel-compatible vertical data model.
- IMP-RD-016 — Benchmark 10k+ segment graph edit.

## Exit criteria

- curved roads create valid local graph;
- one edit does not rebuild whole city;
- road preview explains invalid placement;
- junction routes connect expected modes;
- changing cross-section preserves road identity/history;
- save/load reproduces road geometry and connectivity.
