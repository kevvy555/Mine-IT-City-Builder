# 16 — Utilities, Services, Environment and Resilience

## Shared network principle

Utility/service outcomes distinguish:

- nominal capacity;
- available capacity;
- delivered/effective capacity;
- unmet demand;
- causal bottleneck.

## Utility graph

Network types:

- power;
- water;
- wastewater;
- reclaimed water where enabled;
- waste/material recovery;
- digital connectivity;
- optional district thermal.

Rights of way can host conduit edges.

Graph state is compact and topological; visible pipes/cables are optional representation.

## Power

V1 model:

- generation;
- storage;
- transmission/distribution capacity;
- demand;
- priority class;
- reserve margin;
- outages.

Do not attempt full AC power-flow physics.

Use deterministic capacity allocation:

1. available source/storage;
2. network reachability;
3. edge bottlenecks;
4. priority/load shedding;
5. delivered fraction.

Power failure trace identifies source/edge/capacity/priority cause.

## Water/wastewater

Water:

- source/import;
- treatment;
- storage;
- network capacity;
- quality;
- demand.

Wastewater:

- collection;
- treatment;
- discharge/reuse capacity.

Stormwater/drainage integrates with spatial environment rather than household water pipe flow.

## Waste/material recovery

Buildings generate waste categories at aggregate cadence.

Collection requires:

- facility;
- fleet/service capacity;
- route;
- receiving capacity.

Recovery can feed industrial inputs.

## Service facility model

All services use a common effective-capacity skeleton:

```text
effective = nominal
            × staffing factor
            × utility factor
            × condition factor
            × budget/input factor
            × access/queue factor
```

Domain-specific outcomes sit on top.

Services:

- health;
- education;
- emergency;
- maintenance;
- waste;
- cultural/community;
- other civic facilities.

## Catchments

No authoritative circular-radius coverage for mobile services.

Catchments are travel-time/network based, with optional coarse cached isochrones.

Static local amenities may use distance fields where physically justified.

## Queues

Facilities store demand/queue state.

Capacity shortfall causes:

- wait;
- deferred service;
- worse outcome;
- rerouting to alternatives.

UI exposes demand vs capacity vs access.

## Environment fields

Per runtime chunk, store low-resolution fields:

- air pollution;
- noise;
- heat;
- ground contamination;
- flood/water exposure;
- habitat quality/connectivity.

Start at 25m–50m cells or adaptive sparse grids; exact resolution benchmarked.

Sources emit into fields; diffusion/decay runs at slower cadence.

## Weather

Weather state:

- temperature;
- precipitation;
- wind;
- storm/flood trigger parameters.

V1 can use deterministic authored climate distributions rather than physically simulated atmosphere.

Weather affects:

- demand;
- drainage;
- travel;
- energy;
- environment;
- incidents;
- rendering.

## Drainage/flood

Use simplified terrain-flow graph/grid:

- catchment;
- capacity;
- retention;
- overflow paths;
- exposure.

Flood event creates spatial impact, route closures, building/service effects and recovery projects.

## Incidents/resilience

Incident lifecycle:

```text
risk
→ trigger
→ detection
→ response
→ containment
→ repair
→ recovery
→ after-action history
```

Incident severity depends on:

- redundancy;
- maintenance;
- access;
- staffing;
- backup;
- environment.

## Maintenance

Infrastructure/buildings have condition.

Maintenance work consumes:

- staff;
- budget;
- access;
- materials;
- time.

Deferred maintenance raises failure risk and lowers capacity.

## Tasks

- IMP-CIV-001 — Define utility graph contracts.
- IMP-CIV-002 — Implement power allocation.
- IMP-CIV-003 — Implement storage/reserve/load shedding.
- IMP-CIV-004 — Implement water capacity/quality.
- IMP-CIV-005 — Implement wastewater.
- IMP-CIV-006 — Implement waste/recovery.
- IMP-CIV-007 — Implement generic service capacity model.
- IMP-CIV-008 — Implement travel-time service catchment.
- IMP-CIV-009 — Implement service queues.
- IMP-CIV-010 — Implement environment field grid.
- IMP-CIV-011 — Implement pollution sources/decay.
- IMP-CIV-012 — Implement deterministic weather.
- IMP-CIV-013 — Implement drainage/flood abstraction.
- IMP-CIV-014 — Implement incident lifecycle.
- IMP-CIV-015 — Implement maintenance/condition.
- IMP-CIV-016 — Implement causal network trace.
- IMP-CIV-017 — Add utility-bottleneck fixture.
- IMP-CIV-018 — Add flood fixture.

## Exit criteria

- powered capacity differs from nominal when network bottleneck exists;
- utility failure can be traced source -> bottleneck -> consumer;
- hospital with staff but no power loses effective capacity;
- congestion reduces mobile service response;
- flood blocks routes and creates repair workload;
- environment calculations stay within memory/time budget.
