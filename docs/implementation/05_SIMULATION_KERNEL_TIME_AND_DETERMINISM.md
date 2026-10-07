# 05 — Simulation Kernel, Time and Determinism

## Goal

Create a simulation clock and scheduler that can advance a living city for centuries, run headless in tests, scale across mobile CPU cores and remain independent from rendering frame rate.

## Authoritative time representation

Use a signed 64-bit integer **simulation minute** counted from the scenario epoch.

V1 epoch:

- Year 5300, day 1, 00:00;
- calendar conversion is presentation/domain logic;
- absolute entity dates are stored as simulation-minute/date values, never frame counts.

A 64-bit minute counter is effectively unbounded for project purposes.

## Base quantum

The deterministic scheduler uses a **1 simulated minute base quantum**.

Systems register a cadence or event trigger rather than receiving every render frame.

Initial cadence table:

| Domain | Baseline authoritative cadence |
|---|---:|
| command application | each simulation minute boundary |
| network flow/traffic aggregate | 1 minute active; batchable offscreen/high speed |
| transit schedule/vehicle progress | 1 minute |
| service dispatch | 1 minute |
| utility balancing | 1 minute |
| trip generation/departure | event-driven + 5 minute scan |
| household activity-plan reconciliation | 15 minutes/event-driven |
| business production/inventory | 60 minutes |
| construction progress | 60 minutes |
| household finance | daily |
| municipal accrual | daily; monthly report |
| migration | daily |
| building condition | daily |
| land value/rent pressure | daily with smoothing |
| education progression | daily/term events |
| ageing/birth/death eligibility | daily |
| policy staged effects | daily/event-driven |
| district identity/heritage | monthly/yearly |

Cadences are data/configuration, but changing a cadence that changes outcomes requires deterministic-baseline review.

## Player speed

Initial tuning:

- pause;
- normal: target 5 simulation minutes per real second;
- fast: target 20 simulation minutes per real second;
- very fast: target 80 simulation minutes per real second;
- laboratory: unrestricted headless/batch speed.

If the device cannot execute mandatory steps, achieved game speed falls. Mandatory steps are never silently dropped.

At very fast speed, systems marked aggregate-safe may process a bounded time range in one job while producing the same domain-level result expected by their contract.

## Scheduler phases

Each minute is ordered:

1. apply queued player/scenario commands;
2. process scheduled events at timestamp;
3. world/network mutations;
4. route/cache invalidation;
5. utilities/network allocation;
6. mobility/trip movement;
7. service dispatch/outcomes;
8. production/inventory/logistics;
9. population/household events;
10. development/construction;
11. environment/incidents;
12. governance/budget/policy;
13. derived metrics/history;
14. invariant checks in development/test mode;
15. publish presentation/UI snapshot markers.

Not every system runs every phase every minute; phase order defines deterministic ownership when it does run.

## Scheduled event queue

Event key:

```text
(timestampMinute, priorityClass, stableSequence)
```

`stableSequence` is monotonically assigned by deterministic command/event production order.

No comparison may fall back to object hash, memory address or unordered container order.

## Deterministic random

Implement project-owned Burst-compatible **PCG32** (or equally specified stable algorithm) wrapper rather than relying on unspecified package behaviour.

Root seed stored in save.

Named stream derivation:

```text
streamSeed = Hash(rootSeed, streamNameStableId, optionalEntityStableId, periodKey)
```

Examples:

- migration;
- household lifecycle;
- business creation;
- procedural building grammar;
- citizen naming;
- incident generation.

Adding a random draw to one subsystem must not reorder all other domains.

## Determinism tiers

### Exact/bitwise-authoritative

- simulation timestamp;
- stable IDs;
- money/account ledgers;
- inventories/counts;
- household membership;
- job occupancy;
- building state;
- scheduled event ordering;
- RNG stream state/derivation;
- policy state;
- save schema state.

### Quantised deterministic

Use integer/fixed-point units for:

- currency minor units;
- utility quantities where feasible;
- capacities;
- demand scores;
- land-value indices;
- route generalised costs;
- pollution/exposure grid values.

### Tolerance-based

Permitted for:

- render interpolation;
- camera;
- particles;
- visual vehicle offsets;
- floating terrain geometry;
- generated mesh vertex positions where stored geometry/state removes outcome dependence.

## Fixed-point money

Commonwealth Credit ledger stores signed 64-bit **micro-credit** or another chosen minor unit sufficient for fractional pricing without floating point.

All ledger entries are double-entry/source-sink traceable.

Overflow guards operate in development builds and tests.

## Catch-up rules

Every scheduled system declares one of:

- **NoBatch** — execute each required quantum;
- **BatchExact** — equivalent aggregated calculation exists;
- **BatchApproxWithBound** — only permitted for non-authoritative presentation;
- **EventDriven** — no scan needed if no event.

No authoritative system may use approximate catch-up without a specification decision.

## Background/pause

Android background event:

1. stop accepting simulation advancement;
2. finish or abandon current noncommitted presentation work;
3. optionally request lifecycle autosave;
4. persist settings/build metadata;
5. pause render/audio.

The game does not simulate real-world elapsed time while closed in v1.

## Tasks

- IMP-SIM-001 — Implement SimulationClock.
- IMP-SIM-002 — Implement deterministic phase scheduler.
- IMP-SIM-003 — Implement cadence registry.
- IMP-SIM-004 — Implement scheduled event heap with stable tie-break.
- IMP-SIM-005 — Implement command boundary.
- IMP-SIM-006 — Implement deterministic PCG stream service.
- IMP-SIM-007 — Implement fixed-point money type.
- IMP-SIM-008 — Implement speed controller/catch-up budget.
- IMP-SIM-009 — Implement headless laboratory runner.
- IMP-SIM-010 — Add deterministic checksum snapshot.
- IMP-SIM-011 — Add pause/background hooks.
- IMP-SIM-012 — Add 100-year timestamp/overflow test.
- IMP-SIM-013 — Add uninterrupted vs save/reload equivalence test.

## Exit criteria

- frame rate changes do not affect reference metrics;
- same seed/input stream reproduces checksum sequence;
- scheduled events retain order after save/load;
- pause advances no authoritative state;
- very-fast mode slows or batches safely rather than dropping work;
- 100-year headless run has no time/money overflow.
