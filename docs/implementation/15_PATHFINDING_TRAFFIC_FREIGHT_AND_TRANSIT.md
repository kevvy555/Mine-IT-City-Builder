# 15 — Pathfinding, Traffic, Freight and Transit

## Architecture

Mobility uses interoperable graphs, not Unity NavMeshAgents or Rigidbody traffic.

Graph layers:

- pedestrian;
- micromobility;
- road lanes;
- transit guideways;
- regional/external;
- freight restrictions;
- service/emergency permissions;
- building access/loading.

## Graph representation

Nodes/edges stored in compact native arrays partitioned by runtime chunk/region.

Edge attributes:

- stable edge ID;
- mode;
- length;
- nominal time;
- dynamic delay;
- capacity;
- direction;
- restrictions;
- money cost;
- reliability;
- grade;
- destination/access tags.

Network mutation produces a new local graph epoch/version.

## Hierarchical routing

Use three levels:

### L0 local graph

Fine pedestrian/lane/access graph inside/near runtime chunk.

### L1 chunk portal graph

Boundary portals and major junctions summarise cross-chunk connectivity/cost.

### L2 corridor/regional graph

Arterials, rapid transit, regional/external connectors.

Route:

1. local origin to candidate portal/corridor;
2. A* across hierarchy;
3. local destination access;
4. refine fine path where needed.

Use admissible spatial/time heuristic.

Do not commit to contraction hierarchies for v1 because dynamic edits make preprocessing expensive; prototype ALT/landmark heuristics only if A* hierarchy needs improvement.

## Route cache

Cache keys include:

- origin portal/zone;
- destination portal/zone;
- mode/profile;
- relevant cost epoch.

Local network edits invalidate only caches touched by changed region/version.

Track:

- hit rate;
- miss rate;
- mean/95p expansions;
- queue latency.

## Generalised cost

Integer/quantised cost combines:

- expected time;
- monetary cost;
- transfers;
- reliability;
- comfort;
- restrictions.

Profiles vary by trip purpose/citizen.

## Traffic model

Authoritative road congestion uses link queues/flow rather than physics.

Each minute:

- trips attempt edge progression;
- edge capacity admits flow;
- downstream storage limits movement;
- queues accumulate;
- delay updates expected edge cost;
- severe queue spillback blocks upstream flow.

This supports congestion without millisecond car-following simulation.

## Visible vehicles

Presentation vehicles are mapped to selected authoritative trips/flows.

Near camera:

- individual representative vehicles follow lane splines;
- visual spacing/acceleration is interpolated;
- signal state affects presentation.

Offscreen:

- no need for rendered vehicle;
- trip and queue state continues.

Visible count is not equal to authoritative flow count.

## Trip lifecycle

```text
planned
→ waiting departure
→ routing
→ travelling
→ transfer/wait
→ arrived
or failed/cancelled
```

Trip failure records reason:

- no path;
- service unavailable;
- capacity;
- missed transfer;
- closure;
- destination invalid.

## Departure spreading

Activity plan provides windows. Deterministic sampling distributes departures rather than minute-aligned spikes.

Repeated congestion may alter:

- route;
- mode;
- departure time;
- destination candidate.

## Transit

Transit model:

- line;
- ordered stops;
- service pattern;
- frequency/timetable;
- capacity;
- fleet/depot;
- fare;
- reliability.

At first vertical slice, implement one surface/guideway transit mode.

Passengers use same multimodal routing with transfer edges.

Vehicles can be schedule/event based rather than fully physical offscreen.

## Freight

Freight order:

- commodity;
- quantity;
- source inventory;
- destination inventory;
- due window;
- vehicle/access class.

Pipeline:

source loading -> route -> destination queue -> unloading -> inventory transfer.

Loading bays have throughput and queue capacity.

No inventory transfer occurs until logistics completion unless explicit local-delivery abstraction applies.

## Services/emergency

Service route profile may:

- use restricted lanes;
- receive junction priority;
- ignore some ordinary cost components;
- still suffer physical blockage.

Response time feeds effective service outcome.

## Benchmark design

Synthetic networks:

- 10k edges;
- 50k;
- 250k;
- 1m stretch.

Trip loads:

- 10k concurrent;
- 50k;
- 100k+.

Benchmarks report route expansions, cache, queue, memory and job time.

## Tasks

- IMP-MOB-001 — Define graph data schema.
- IMP-MOB-002 — Implement graph partition/build.
- IMP-MOB-003 — Implement local A*.
- IMP-MOB-004 — Implement portal hierarchy.
- IMP-MOB-005 — Implement corridor/regional hierarchy.
- IMP-MOB-006 — Implement route cache/version invalidation.
- IMP-MOB-007 — Implement integer generalised cost.
- IMP-MOB-008 — Implement trip lifecycle.
- IMP-MOB-009 — Implement link queue/capacity traffic.
- IMP-MOB-010 — Implement spillback.
- IMP-MOB-011 — Implement rendered-vehicle projection.
- IMP-MOB-012 — Implement freight order/loading.
- IMP-MOB-013 — Implement transit line/stops/frequency.
- IMP-MOB-014 — Implement transfers/capacity.
- IMP-MOB-015 — Implement emergency route profile.
- IMP-MOB-016 — Add bridge closure reroute fixture.
- IMP-MOB-017 — Add camera-LOD conservation test.
- IMP-MOB-018 — Benchmark pathfinding hierarchy.

## Exit criteria

- bridge closure reroutes only affected trips/caches;
- camera movement does not clear congestion;
- high-volume free-flow road differs from low-speed congestion overlay;
- freight physically reaches receiver before inventory transfer;
- emergency response worsens under congestion;
- 250k-city routing benchmark meets phase budget or blocks scale gate.
