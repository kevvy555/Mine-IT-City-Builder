# 13 — Population, Households, Jobs and Education

## Population architecture

Maintain persistent individual citizens while avoiding per-frame individual logic.

Four layers:

1. persistent citizen/household state;
2. scheduled activity plan;
3. active trip/activity state;
4. optional rendered representative.

A citizen can exist for centuries of simulation without ever owning a GameObject.

## Citizen lifecycle

Persistent fields include:

- birth date;
- household;
- home;
- ancestry/cultural references where relevant;
- education/qualification;
- employment;
- wellbeing/health bands;
- lifecycle stage;
- activity/mobility profile;
- history flags.

Daily/event systems handle:

- ageing;
- birth/death eligibility;
- household formation/split;
- education transitions;
- employment transitions;
- retirement;
- relocation/migration.

## Household model

Household is primary housing/finance unit.

Tracks:

- members;
- income;
- dwelling;
- tenure;
- housing cost;
- size/space need;
- preferences;
- move pressure;
- transport access.

Household budget aggregation avoids recalculating every citizen financial transaction separately where unnecessary.

## Population conservation

Every population change produces a typed cause:

- birth;
- death;
- in-migration;
- out-migration;
- scenario/import adjustment.

Daily ledger:

```text
start + births + inMigration - deaths - outMigration = end
```

Mismatch is invariant failure.

## Migration

Candidate migration considers:

- available housing;
- jobs;
- service quality;
- affordability;
- city attractiveness;
- external region conditions;
- policy.

External population is aggregated; migrants become persistent citizens only when entering detailed city population.

## Jobs

Job slot belongs to an organisation/business/facility and a physical workplace.

Fields:

- occupation/skill family;
- qualification floor/preference;
- wage;
- shift;
- workplace;
- active/vacant;
- remote/onsite allowance if supported.

## Employment matching

Do not run a global all-workers × all-jobs search.

Pipeline:

1. partition by skill/qualification;
2. index vacancies by chunk/district/access;
3. build candidate shortlist;
4. score wage, suitability, commute, schedule;
5. match in deterministic batches;
6. retain friction/hysteresis so people do not switch jobs daily for tiny gains.

Unemployment reason is classified:

- frictional;
- qualification;
- geographic/access;
- wage;
- cyclical;
- schedule/other.

## Education

Education entities:

- schools;
- colleges/universities;
- training programmes.

Students create real trips.

Capacity depends on:

- places;
- staff;
- utilities;
- access;
- condition/budget.

Progress is event/daily aggregated, not classroom-agent simulation.

## Activity plans

Typical citizen day is represented as scheduled activities:

- home;
- work;
- education;
- shopping;
- leisure;
- healthcare;
- civic;
- travel.

Plans are probabilistic but deterministic from named streams and profile inputs.

Not every optional activity becomes a trip every day.

## Trip interface

Population produces `TripRequest`:

- traveller;
- purpose;
- earliest/latest departure;
- origin;
- destination candidate set;
- mode constraints/preferences;
- return requirement.

Mobility returns planned route/expected arrival or failure cause.

Failure influences wellbeing/job/service outcomes through explicit contracts.

## Wellbeing

Maintain components, not one magic happiness value:

- housing;
- affordability;
- work/income;
- health;
- access;
- environment;
- safety/resilience;
- leisure/culture;
- stability.

UI can present headline index but source factors remain inspectable.

## Scale strategy

At 250k:

- citizen state compact;
- daily changes processed in batches;
- expensive matching only for affected candidates;
- activity generation staggered;
- route requests distributed by departure windows.

At 1m stretch:

- inactive/off-region groups may enter cohort scheduling while preserving individuals for identity/history where required;
- no camera-driven population deletion.

## Tasks

- IMP-POP-001 — Implement citizen components.
- IMP-POP-002 — Implement household entities/membership.
- IMP-POP-003 — Implement population ledger.
- IMP-POP-004 — Implement lifecycle daily system.
- IMP-POP-005 — Implement migration model.
- IMP-POP-006 — Implement job slot model.
- IMP-POP-007 — Implement vacancy indices.
- IMP-POP-008 — Implement deterministic employment matcher.
- IMP-POP-009 — Implement unemployment-reason classification.
- IMP-POP-010 — Implement education enrolment/progression.
- IMP-POP-011 — Implement activity planner.
- IMP-POP-012 — Implement TripRequest production.
- IMP-POP-013 — Implement wellbeing factors.
- IMP-POP-014 — Build 10k/50k/250k synthetic population fixtures.
- IMP-POP-015 — Add population conservation invariant.

## Exit criteria

- 250k synthetic citizens exist without per-citizen managed objects;
- jobs cannot exceed slot capacity;
- household/dwelling counts reconcile;
- commuting failures expose reason;
- save/reload preserves citizen/household identity;
- camera/graphics setting does not alter population totals.
