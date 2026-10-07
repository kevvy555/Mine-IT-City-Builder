# 08 — Traffic, Pathfinding and Logistics

## Shared routing principle

Passengers, service units and freight share physical networks but have different objectives and constraints.

KCB-TRAF-001 — All routed agents MUST use a common network representation or interoperable graphs so closures and infrastructure changes propagate consistently.

## Graph layers

Possible graph layers:
- walking;
- micromobility;
- road lanes;
- transit guideways;
- regional rail/maglev;
- freight-only routes;
- service/emergency permissions;
- building access/load bays.

Nodes represent:
- junctions;
- lane transitions;
- stops/stations;
- entrances;
- transfer points;
- external connections.

Edges store:
- mode;
- length;
- free-flow time;
- current cost;
- capacity;
- restriction;
- grade;
- toll/fare;
- reliability;
- schedule where applicable.

## Path cost

Generalised path cost:

cost = timeWeight * expectedTime
     + moneyWeight * monetaryCost
     + transferWeight * transfers
     + reliabilityWeight * uncertainty
     + comfortWeight * discomfort
     + restrictionPenalty

Weights depend on agent/trip class.

KCB-TRAF-010 — Route planning MUST consider expected congestion/travel time, not only geometric distance.

KCB-TRAF-011 — Emergency response may use different restrictions/priorities.

KCB-TRAF-012 — Freight routing accounts for vehicle size, load limits, loading access and cargo-terminal compatibility.

## Hierarchical pathfinding

Large cities cannot run a full fine-grained search from scratch for every trip.

KCB-TRAF-020 — Implementation SHOULD use hierarchical routing:
- local access graph;
- corridor/arterial graph;
- regional/external graph.

KCB-TRAF-021 — Common origin/destination corridor results SHOULD be cacheable with invalidation when network costs materially change.

KCB-TRAF-022 — Exact algorithm is deferred, but implementation planning MUST benchmark A-star-family routing against hierarchical/preprocessed alternatives.

## Trip generation

Trips arise from real purposes:
- commute;
- education;
- shopping;
- leisure;
- healthcare;
- civic;
- delivery;
- maintenance;
- emergency;
- freight transfer;
- construction.

KCB-TRAF-030 — Decorative traffic MUST not be mixed into measured traffic unless clearly excluded from capacity calculations.

KCB-TRAF-031 — Not every theoretical trip needs an individual rendered agent. Aggregate trip demand may instantiate representative agents based on simulation LOD.

## Departure timing

KCB-TRAF-040 — Trips SHOULD be distributed by schedule and flexible departure windows, producing peaks without everyone departing on the same tick.

KCB-TRAF-041 — Congestion can cause agents to alter departure time, route or mode over repeated days.

## Road traffic

Vehicle dynamics can be simplified compared with a driving simulator but must model:
- lane choice;
- car following;
- acceleration/braking abstraction;
- junction priority;
- lane changes;
- queues;
- blocked lanes;
- turning movements.

KCB-TRAF-050 — Vehicles MUST not routinely block intersections when downstream storage is unavailable.

KCB-TRAF-051 — Lane assignment SHOULD anticipate upcoming turns rather than changing at the last possible point.

KCB-TRAF-052 — Accidents/closures trigger rerouting with a delay appropriate to information availability.

## Freight

Freight flow:
source -> consolidation/warehouse optional -> network -> receiving dock -> inventory.

KCB-TRAF-060 — Goods do not appear inside factories/shops without transport or a deliberately abstracted local-delivery rule.

KCB-TRAF-061 — Buildings have finite loading throughput.

KCB-TRAF-062 — Freight vehicles may queue if receiving capacity is saturated.

KCB-TRAF-063 — Warehouses can buffer supply but create land, cost and traffic trade-offs.

## Service routing

Ambulances, maintenance, fire/rescue, waste and other civic fleets need:
- dispatch point;
- travel route;
- service time;
- return/reload state;
- priority.

KCB-TRAF-070 — A service building’s nominal vehicle count is not equivalent to effective coverage if routes are congested.

## Outside connections

External connectors have:
- mode;
- destination/region label;
- capacity;
- base travel time;
- pricing;
- reliability;
- market linkage.

KCB-TRAF-080 — External trip demand MUST be capacity constrained.

KCB-TRAF-081 — A new faster external connection may induce additional travel/trade.

## Traffic information views

Required:
- flow speed;
- volume;
- volume/capacity ratio;
- queue length;
- intersection delay;
- freight flow;
- origin-destination;
- travel-time isochrones;
- incident locations;
- route inspection.

KCB-TRAF-090 — Flow and volume MUST be separate metrics; a free-flowing high-volume corridor is not displayed as a problem merely because it is busy.

## Simulation LOD

Near camera:
- visible lane-level vehicles;
- local behaviour.

Mid distance:
- reduced animation/decision frequency;
- representative agents.

Far/offscreen:
- corridor flow packets/aggregate queues.

KCB-TRAF-100 — LOD transitions MUST preserve conserved quantities such as people/goods and expected travel time.

KCB-TRAF-101 — Camera movement MUST NOT clear congestion.

## Performance invariants

- path requests are budgeted per simulation step;
- route invalidation is local;
- traffic density does not allocate heavyweight objects per vehicle;
- network edits invalidate only affected graph regions when possible;
- instrumentation reports pathfinding queue, cache hit rate and average search cost.

## Acceptance criteria

- Closing one bridge reroutes affected trips and increases alternative-corridor load.
- A factory without freight access runs out of inputs.
- Emergency response time worsens under congestion.
- Camera zoom does not change city-wide completed-trip totals beyond defined tolerance.
- Flow and volume overlays identify different issues.
