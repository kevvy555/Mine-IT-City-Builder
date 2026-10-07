# 14 — City Services and Facilities

## Shared service model

Services are implemented through a common capability model rather than bespoke radius bonuses.

A service provider has:
- service type;
- fixed/passive influence if any;
- processing capacity;
- staff requirement;
- mobile units if any;
- catchment/travel network;
- queues;
- utilities/inputs;
- maintenance/condition;
- upgrades/modules;
- service quality;
- district assignment;
- budget.

KCB-SERV-001 — Nominal capacity and effective capacity MUST be separate.

effectiveCapacity = baseCapacity x staffing x utilities x condition x budget x inputAvailability

## Service families

- healthcare;
- education;
- emergency/fire/rescue;
- community support;
- waste/recycling;
- road/infrastructure maintenance;
- parks/public realm;
- culture/libraries/museums;
- administration/civic access;
- transit operations;
- environmental management;
- housing support.

## Coverage

Service access uses one or more:
- walk/travel time;
- dispatch response time;
- network reach;
- district eligibility;
- remote/digital availability.

KCB-SERV-010 — A simple circular radius MUST NOT be the authoritative coverage model for mobile/networked services.

KCB-SERV-011 — Radius-like heatmaps MAY be used for passive local effects such as park amenity if their meaning is explicit.

## Queues

KCB-SERV-020 — Services with appointments/processing MUST support finite queues or waiting times.

KCB-SERV-021 — A city can have enough annual capacity but poor peak-time performance.

## Upgrades

Facility modules may add capacity, specialist service, fleet, resilience, energy efficiency, local amenity or research/training.

KCB-SERV-030 — Upgrades require space/compatibility where physically relevant.

KCB-SERV-031 — Upgrade construction can temporarily reduce operation.

## Staffing

KCB-SERV-040 — Service buildings require appropriate workforce.
KCB-SERV-041 — Understaffing reduces effective capacity/quality rather than instantly switching off.
KCB-SERV-042 — Critical services may impose minimum staffing thresholds.

## Maintenance

KCB-SERV-050 — Facilities degrade and need preventive maintenance.
KCB-SERV-051 — Deferred maintenance increases failure risk and reduces efficiency.

## District assignment

Mobile fleets/services may be city-wide, preferred-district, exclusive-district or mutual-aid backup.

KCB-SERV-060 — District assignment affects dispatch but SHOULD allow mutual aid during major incidents unless policy forbids it.

## Service quality

Quality combines wait/response time, outcome effectiveness, accessibility, capacity pressure, condition and user satisfaction.

KCB-SERV-070 — High spending with poor accessibility can still produce low effective service.

## Public realm

Parks, plazas and greenways provide recreation, cooling, ecological connectivity, walking routes, event space and land desirability.

KCB-SERV-080 — Public realm has maintenance and capacity.
KCB-SERV-081 — Federal Forum and Ceremonial Axis begin with high-quality civic public realm.

## Culture and museums

KCB-SERV-090 — Cultural institutions can preserve history, create leisure trips, tourism/prestige, education and district identity.

KCB-SERV-091 — Cultural service is not represented only as generic happiness.

## Administrative services

Citizens/businesses may require permits or civic interactions, but routine bureaucracy should be highly digital in Year 5300.

KCB-SERV-100 — Administrative gameplay focuses on institutional capacity/policy, not making the player process individual paperwork.

## Information views

For every service:
- demand;
- nominal/effective capacity;
- utilisation;
- waiting/response;
- staffing;
- budget;
- condition;
- access/catchment;
- incidents;
- district comparison.

KCB-SERV-110 — Selecting an underserved area offers the top causal reason: insufficient capacity, distance, congestion, staffing, outage, eligibility or budget.

## Acceptance criteria

- Two hospitals with equal nominal capacity can deliver different outcomes due to access/staffing.
- Increasing budget cannot fix a disconnected service.
- A local clinic reduces demand on a distant hospital.
- Service upgrade adds capacity without erasing the facility’s history.
- Maintenance deferral visibly degrades service over time.
