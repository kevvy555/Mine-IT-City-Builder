# 15 — Power, Water, Waste and Utilities

## Utility philosophy

Koplin 3 is a mature high-technology world. Utilities are reliable by default because generations of investment made them reliable; gameplay comes from capacity, topology, redundancy, maintenance, cost, environmental trade-offs and growth.

KCB-UTIL-001 — Utilities MUST be represented as networks with sources, transport/distribution and consumers.

KCB-UTIL-002 — A road containing a utility conduit does not imply infinite utility capacity.

KCB-UTIL-003 — Utility failure MUST have a traceable network cause.

## Electricity

Power model:
- generation/import;
- transmission;
- substations/conversion;
- distribution;
- storage;
- demand;
- reserve margin.

Canonical technology supports mature fusion and advanced storage/power electronics.

KCB-UTIL-010 — Concordia’s default grid SHOULD begin low-carbon and highly reliable.

KCB-UTIL-011 — Generation types define output, ramp/flexibility, maintenance, footprint, fuel/input if any, heat/noise, capital and operating cost.

KCB-UTIL-012 — Power flow MAY use a simplified network-capacity model rather than full AC load flow, but local bottlenecks and islanding MUST be possible.

KCB-UTIL-013 — Storage has energy capacity and charge/discharge power as separate limits.

KCB-UTIL-014 — Reserve margin is displayed separately from current demand headroom.

### Power priority and shedding

Consumer classes:
1. emergency/life safety;
2. utility network operations;
3. healthcare;
4. transit/control;
5. essential civic;
6. residential;
7. commercial/industrial flexible load.

KCB-UTIL-020 — Automatic load shedding SHOULD prevent total grid collapse where possible.
KCB-UTIL-021 — Player may set district/facility priorities within legal constraints.
KCB-UTIL-022 — Critical facilities SHOULD support local backup/storage.

## Water

Water system:
- source/import;
- treatment;
- storage;
- trunk network;
- local distribution;
- consumption;
- wastewater collection;
- treatment/reuse/discharge.

KCB-UTIL-030 — Water quality and water quantity are separate properties.
KCB-UTIL-031 — Pressure/throughput MAY be simplified into capacity zones but must detect undersupply at network bottlenecks.
KCB-UTIL-032 — Leakage/condition increases production demand and maintenance cost.
KCB-UTIL-033 — Reclaimed water MAY serve suitable industrial/landscape uses.

## Stormwater

KCB-UTIL-040 — Rainfall runoff interacts with terrain, hard surfaces and green-blue infrastructure.
KCB-UTIL-041 — Drainage capacity can be exceeded locally.
KCB-UTIL-042 — Wetlands, retention basins, green roofs and permeable public realm can reduce runoff peaks.

## Waste and material recovery

Waste categories may be abstracted into:
- recyclable material;
- organics/biological;
- inert/construction;
- hazardous/specialist;
- residual.

Flow:
building -> collection/local system -> transfer -> sorting/recovery -> processing/export/disposal.

KCB-UTIL-050 — Waste does not disappear when a building produces it.
KCB-UTIL-051 — Recovery reduces raw-material/import demand where production chains support substitution.
KCB-UTIL-052 — Construction/demolition projects create temporary waste streams.
KCB-UTIL-053 — Collection routing competes for road/logistics capacity unless a dedicated system exists.

## Digital/communications network

A mature capital is highly networked.

KCB-UTIL-060 — Digital connectivity SHOULD be treated as a utility/access layer for remote work, civic services, automation and research.

KCB-UTIL-061 — The game MAY model backbone capacity/redundancy rather than individual household bandwidth.

KCB-UTIL-062 — High-capability civic AI services require power, compute and network continuity and obey canonical AI segmentation rules.

## District heating/cooling

Advanced thermal networks MAY recover heat from industry, compute or utilities.

KCB-UTIL-070 — Thermal networks, if implemented, use the same source-network-consumer pattern.
KCB-UTIL-071 — Heat recovery creates useful synergies but requires geographic proximity/network investment.

## Network condition

Every network asset has:
- install date;
- condition;
- design capacity;
- current loading;
- maintenance schedule;
- redundancy class.

KCB-UTIL-080 — High loading accelerates risk/maintenance pressure only where technically justified.
KCB-UTIL-081 — Age alone SHOULD not randomly fail well-maintained assets.

## Maintenance and outages

Outages:
- planned;
- component failure;
- construction damage;
- external supply disruption;
- extreme event.

KCB-UTIL-090 — Planned maintenance can reroute service when redundancy exists.
KCB-UTIL-091 — The UI shows affected customers and expected consequence before a planned shutdown.
KCB-UTIL-092 — Repairs consume staff/material capacity.

## Utility pricing

Policies can set:
- household tariff;
- commercial/industrial tariff;
- lifeline allowance;
- time-sensitive pricing optional;
- public-service exemptions.

KCB-UTIL-100 — Pricing affects municipal/utility finance and potentially demand, but essential access must be represented consistently with Commonwealth social norms.

## Utility overlays

Each network needs:
- topology;
- direction/flow where meaningful;
- loading/capacity;
- source contribution;
- outages;
- condition;
- redundancy;
- service quality.

KCB-UTIL-110 — The player can trace an undersupplied building upstream to the limiting network element.

## Acceptance criteria

- Adding generation does not fix a saturated local substation.
- Breaking one trunk line causes rerouting if alternate capacity exists.
- Storage can handle a short peak but not sustained energy deficit.
- Green-blue infrastructure reduces local drainage overload.
- Recycling/recovery produces measurable material flow.
- Planned maintenance shows predicted affected area before confirmation.
