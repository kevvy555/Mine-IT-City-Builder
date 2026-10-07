# 07 — Public Transit and Mobility

## Mobility philosophy

Concordia is canonically a rapid-transit-oriented capital. Public transport is therefore a baseline urban system, not a late-game novelty.

Modes may include:
- walking;
- cycling/micromobility equivalent;
- local shuttle/bus;
- tram/surface guideway;
- metro/subsurface;
- maglev/rapid regional transit;
- rail/regional;
- water transit where geography supports it;
- taxi/on-demand shared mobility;
- private vehicle;
- freight modes handled separately.

KCB-TRANS-001 — The starting Concordia scenario MUST contain at least one operational high-capacity transit spine consistent with atlas references.

## Transit components

Every transit mode defines:
- depot/yard or fleet base where applicable;
- vehicle class;
- guideway requirement;
- stop/station;
- line/service pattern;
- timetable/frequency;
- capacity;
- boarding/alighting time;
- operating cost;
- maintenance;
- accessibility;
- energy use.

KCB-TRANS-010 — Vehicles MUST originate from a valid fleet/depot abstraction unless the mode intentionally uses through-running external service.

## Lines and services

KCB-TRANS-020 — Player creates service lines by selecting stops/stations in order.

KCB-TRANS-021 — Lines MUST support:
- loop or bidirectional operation;
- service frequency/headway;
- operating hours;
- vehicle allocation;
- express/skip-stop pattern where mode supports it;
- depot assignment;
- colour/name purely as UI metadata.

KCB-TRANS-022 — Default automation SHOULD calculate a feasible timetable/fleet requirement.

## Passenger choice

Generalised trip cost includes:
- walk access;
- wait time;
- in-vehicle time;
- transfer time;
- fare;
- crowding;
- reliability;
- comfort;
- parking cost where relevant.

Different citizens weight factors differently by:
- age;
- income;
- mobility needs;
- trip purpose;
- time pressure;
- household vehicle access.

KCB-TRANS-030 — Mode choice MUST not be a hard nearest-mode rule.

KCB-TRANS-031 — Route choice can change when congestion, frequency or disruptions change.

## Capacity

KCB-TRANS-040 — Station platforms, concourses and vehicles have finite throughput/capacity.

KCB-TRANS-041 — Overcrowding increases wait or disutility before becoming a hard failure.

KCB-TRANS-042 — Crowding MUST be visible through both UI and agent representation where close enough.

## Transfers

KCB-TRANS-050 — Transfer penalty depends on walking distance, vertical change, waiting, wayfinding and reliability.

KCB-TRANS-051 — Integrated stations SHOULD allow multiple modes to share concourses and reduce transfer cost.

## Fare model

Because the exact Concordia fare system is not canon, gameplay supports policies:
- free-at-point-use;
- flat fare;
- distance/zone fare;
- subsidised groups;
- premium express.

KCB-TRANS-060 — Fare policy affects both municipal budget and ridership.

KCB-TRANS-061 — Default Commonwealth-capital scenario SHOULD bias toward affordable high-use public transport rather than car-centric pricing.

## Transit-oriented development

KCB-TRANS-070 — High-quality transit accessibility increases development viability and can support higher density where planning rules allow.

KCB-TRANS-071 — Station-area growth MUST still account for noise, capacity, land cost and local amenities.

## Disruption

Causes:
- maintenance;
- vehicle shortage;
- power issue;
- incident;
- construction;
- severe weather;
- overcrowding.

KCB-TRANS-080 — Disruption causes passenger rerouting rather than deleting trips.

KCB-TRANS-081 — Replacement/contingency service MAY be automatically proposed.

## Walking and public realm

KCB-TRANS-090 — Pedestrians use an explicit walk network including paths, crossings, plazas, building entrances and station concourses.

KCB-TRANS-091 — Walkability depends on route distance, crossings, slope, environment and barriers.

KCB-TRANS-092 — Major plazas such as Federal Forum MUST support very high pedestrian throughput without being treated as roads.

## Universal access

KCB-TRANS-100 — Every public transit station has an accessibility state.
KCB-TRANS-101 — Step-free or equivalent inclusive access MUST be standard in modern Commonwealth facilities unless a heritage constraint creates a gameplay upgrade need.
KCB-TRANS-102 — Route planner must avoid inaccessible links for citizens requiring accessible travel.

## Information views

Required:
- line map;
- stop usage;
- origin-destination flow;
- load factor by segment/time;
- wait time;
- reliability;
- transfer flows;
- coverage/accessibility;
- fleet utilisation.

KCB-TRANS-110 — Selecting a line shows where delay and crowding originate.

## Acceptance criteria

- Increasing frequency reduces waiting until fleet/depot/station constraints bind.
- A closed station causes plausible rerouting.
- A new interchange changes mode split in surrounding districts.
- A transit line can be popular but financially subsidised.
- High ridership is not equivalent to good service if crowding/reliability are poor.
