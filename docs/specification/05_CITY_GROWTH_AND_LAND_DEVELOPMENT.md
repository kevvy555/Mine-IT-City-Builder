# 05 — City Growth and Land Development

## Growth philosophy

The player creates conditions; households, cooperatives, institutions and firms generate much of ordinary development.

KCB-GROW-001 — Generic residential/commercial/workplace development SHOULD emerge on permitted parcels when demand and viability are sufficient.

KCB-GROW-002 — Civic landmarks, major infrastructure and strategic projects are commissioned directly.

KCB-GROW-003 — Development MUST react to both city-wide demand and parcel-specific feasibility.

## Development pipeline

For each candidate parcel:
1. determine legal use permissions;
2. calculate access;
3. check utilities/service prerequisites;
4. calculate market demand;
5. evaluate land/rent cost;
6. evaluate environmental and nuisance factors;
7. evaluate workforce/customer/freight access as relevant;
8. choose building archetype;
9. generate massing/facade from architectural grammar;
10. create development proposal;
11. wait through approval/construction;
12. open and begin occupancy;
13. upgrade/renovate/redevelop over time.

KCB-GROW-010 — The parcel suitability score MUST expose its top positive and negative contributors.

## Demand

Demand classes:
- household formation;
- incoming migration;
- residential by affordability/density preference;
- retail/services;
- office/knowledge;
- light industry;
- heavy/utility industry;
- civic/institutional;
- cultural/leisure;
- logistics.

Demand is not a single bar.

KCB-GROW-020 — Demand MUST be segmented enough that a city can simultaneously have, for example, high affordable-housing demand and low luxury-housing demand.

KCB-GROW-021 — Demand MUST respond to vacancies and unmet need with damping.

KCB-GROW-022 — New zoning permission alone MUST NOT guarantee construction.

## Density

Density is an outcome of:
- allowed envelope;
- access capacity;
- land value;
- household/business demand;
- infrastructure;
- local policy;
- construction economics;
- heritage/environment constraints.

KCB-GROW-030 — Low/medium/high density labels MAY exist for usability but should compile to explicit envelope rules.

Envelope parameters:
- floor-area ratio;
- site coverage;
- maximum/minimum height;
- setbacks;
- frontage activation;
- open-space requirement;
- parking/logistics requirement;
- mixed-use allowance.

## Mixed use

KCB-GROW-040 — The system MUST support multiple uses in one building or parcel.

Example stack:
- ground floor retail/community;
- middle office/education;
- upper residential;
- basement/edge logistics and utilities.

KCB-GROW-041 — Mixed-use buildings allocate capacity by use and generate separate trip/resource profiles.

## Infill and redevelopment

Mature Concordia requires change inside an existing city.

KCB-GROW-050 — Underused parcels MAY redevelop when expected value/utility exceeds current use plus demolition/relocation cost.

KCB-GROW-051 — Heritage status, tenant protection, public ownership, contamination and infrastructure constraints may block or slow redevelopment.

KCB-GROW-052 — Redevelopment MUST generate displacement/relocation effects rather than deleting occupants without consequence.

## Vacancy and abandonment

Vacancy can result from:
- unaffordability;
- weak demand;
- poor access;
- nuisance;
- building condition;
- oversupply;
- local decline.

KCB-GROW-060 — A vacant building does not instantly become abandoned.
KCB-GROW-061 — Long vacancy reduces maintenance and may trigger repurposing, sale, civic intervention or demolition.
KCB-GROW-062 — “Abandoned” is a late condition with visible reasons.

## Building evolution

Buildings can:
- improve efficiency;
- renovate;
- add modules;
- change use;
- subdivide/merge;
- modernise systems;
- receive heritage protection;
- decline.

KCB-GROW-070 — Visual change SHOULD accompany major state change.

## Construction market

Construction requires:
- funding;
- labour;
- materials/logistics;
- contractor capacity;
- site access.

KCB-GROW-080 — City-wide construction capacity MUST limit simultaneous large projects.

KCB-GROW-081 — Overheating construction demand SHOULD increase cost/time rather than spawning infinite crews.

KCB-GROW-082 — Major imported materials can couple city growth to freight capacity.

## Public land and acquisition

KCB-GROW-090 — Public projects on occupied private/cooperative land require purchase, negotiated easement, land swap or compulsory acquisition where legal.

KCB-GROW-091 — Acquisition has fiscal and political cost.

KCB-GROW-092 — Sandbox may bypass acquisition.

## Growth boundaries

Policies may:
- protect greenbelt;
- preserve farmland;
- set urban-growth boundary;
- intensify around transit;
- cap district density;
- encourage brownfield reuse.

KCB-GROW-100 — Growth boundary pressure SHOULD raise land values inside the boundary while reducing sprawl, creating a real trade-off.

## Development visualisation

Required overlays:
- demand by use/price tier;
- vacant parcels;
- buildable envelope;
- recent permits;
- construction activity;
- vacancy;
- redevelopment pressure;
- protected land;
- affordability.

KCB-GROW-110 — Hovering a proposed development MUST show why that form/use was chosen.

## Tuning baselines

To avoid instant oscillation:
- demand calculations use rolling windows;
- land value changes are capped per period;
- building redevelopment has minimum age/cooldown unless emergency;
- occupancy changes have moving/friction costs;
- construction projects reserve resources before starting.

Exact values are balance data, not canon.

## Acceptance criteria

- Two equally zoned parcels with different access can develop differently.
- Transit improvement can raise development pressure without manual rezoning.
- Oversupply creates vacancies before mass abandonment.
- Removing utility access can stop new development even with demand.
- Heritage protection prevents automatic demolition.
- Mixed-use building produces both residents and jobs/trips.
