# 11 — Housing, Land Value and Property

## Housing goals

Housing is both shelter and a location in the city network. The system must create meaningful affordability, density and accessibility trade-offs without treating high land value as automatically “good.”

KCB-HOUSE-001 — Every household occupies a dwelling unit or an explicit temporary-housing state.

KCB-HOUSE-002 — Residential building capacity is measured in dwelling units and household fit, not population points only.

## Dwelling attributes

- size/bedroom class abstractly;
- tenure;
- rent/ownership cost;
- building condition;
- accessibility;
- environmental quality;
- service access;
- transit/job access;
- shared amenities;
- energy/utility efficiency.

KCB-HOUSE-010 — Household matching considers both ability to pay and suitability.

## Tenure

Potential tenure categories:
- public;
- cooperative;
- private rental;
- owner-occupied/long lease;
- institutional/student;
- assisted.

Exact Commonwealth property law is not canon; categories are game abstractions until approved.

KCB-HOUSE-020 — Tenure may alter price dynamics, mobility and policy tools.

## Land value

Land value is an emergent price/attractiveness signal driven by:
positive:
- accessibility;
- services;
- public realm;
- low nuisance;
- jobs/amenities;
- prestige;
- environmental quality;
- scarcity of developable land.

negative:
- noise;
- pollution;
- congestion;
- hazard;
- poor services;
- isolation;
- dereliction.

KCB-HOUSE-030 — Land value MUST not be directly raised by simply placing a service building. It rises because households/businesses value the improved outcome.

KCB-HOUSE-031 — Land value and affordability are separate metrics.

## Rent/price formation

TUNING MODEL:
askingCost = baseStructureCost + locationComponent + demandPressure + qualityComponent + policyAdjustments

Effective burden = housingCost / householdDisposableIncome

KCB-HOUSE-040 — Housing stress uses income-relative burden, not absolute price.

KCB-HOUSE-041 — Price changes are smoothed and limited per period.

## Affordability tiers

The simulation SHOULD track demand/supply by broad income or affordability bands rather than one residential demand pool.

KCB-HOUSE-050 — A city can have a housing surplus overall while still having an affordable-housing shortage.

## Moving

Households evaluate a move when household composition changes, housing stress persists, job/education circumstances change, satisfaction is poor, redevelopment requires relocation or a materially better match remains available.

KCB-HOUSE-060 — Moving has friction and cost.
KCB-HOUSE-061 — Households do not continuously re-optimise every tick.

## Temporary accommodation

KCB-HOUSE-070 — Housing-access failure is treated as a service and housing-policy problem.

KCB-HOUSE-071 — Public emergency/temporary accommodation prevents instant citizen deletion from the simulation.

## Displacement

KCB-HOUSE-080 — Improvements may increase desirability and costs.
KCB-HOUSE-081 — Redevelopment can displace existing households.
KCB-HOUSE-082 — Policies may reduce displacement via public/cooperative housing, affordability requirements, relocation support or price-stability abstractions.

## Public housing and cooperative development

KCB-HOUSE-090 — The player may commission non-market housing.
KCB-HOUSE-091 — Public/cooperative housing trades capital/operating budget for affordability stability and policy objectives.
KCB-HOUSE-092 — It still requires land, maintenance, services and transport.

## Building condition

Condition depends on age, maintenance, incidents, utility problems and renovation.

KCB-HOUSE-100 — Low condition affects comfort, efficiency, safety and rent/value.
KCB-HOUSE-101 — Renovation can extend building life and improve systems without demolition.

## Density and household choice

KCB-HOUSE-110 — Higher density offers accessibility/land-efficiency benefits but may increase crowding/noise or reduce private space depending on building quality.

KCB-HOUSE-111 — High-quality public space and transit can make dense housing highly desirable.

## Information views

Required:
- dwelling supply;
- vacancy;
- rents/costs;
- affordability burden;
- household-size mismatch;
- public/cooperative share;
- displacement risk;
- condition;
- residential accessibility.

KCB-HOUSE-120 — Selecting a residential building shows units, occupancy, average burden, reasons for vacancy and key local factors.

## Acceptance criteria

- Adding jobs without housing raises commute/migration pressure.
- New transit can raise desirability and also affordability pressure.
- City-wide vacancy can coexist with an affordability shortage.
- Renovation improves condition without resetting building history.
- Redevelopment relocates real households instead of erasing them.
