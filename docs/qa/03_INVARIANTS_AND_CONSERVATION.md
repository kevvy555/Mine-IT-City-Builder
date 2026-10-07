# 03 — Invariants and Conservation Rules

## Purpose

Invariants are conditions that must never be violated regardless of tuning, strategy, city size or random seed.

They are the strongest form of simulation QA.

## Identity/world

QA-INV-001 — Persistent stable IDs are unique within namespace/save.

QA-INV-002 — A deleted persistent ID is never silently reused.

QA-INV-003 — Canonical atlas coordinates do not change across save/load/version without explicit migration.

QA-INV-004 — Runtime chunk ID deterministically maps to one atlas tile/local index.

QA-INV-005 — Persistent references resolve, are explicitly archived/tombstoned, or fail validation.

## Population

QA-INV-010 — Population ledger reconciles:

```text
start + births + inMigration - deaths - outMigration = end
```

QA-INV-011 — Citizen count cannot become negative.

QA-INV-012 — Active citizen belongs to at most one household.

QA-INV-013 — Household member references are reciprocal/valid.

QA-INV-014 — Occupied dwellings reconcile with households/occupancy.

QA-INV-015 — Employment cannot exceed job slots.

QA-INV-016 — Student enrolment cannot exceed effective configured capacity unless explicit overflow state exists.

## Economy

QA-INV-020 — Fixed-point money transfer never creates value except through explicit modelled source.

QA-INV-021 — Double-entry/source-sink reconciliation passes per reporting period.

QA-INV-022 — Inventory cannot become negative.

QA-INV-023 — Production output cannot exceed output implied by required inputs/capacity unless recipe explicitly has source generation.

QA-INV-024 — Capital commitment cannot be reported as uncommitted available cash.

QA-INV-025 — Debt/principal/interest operations reconcile.

## Buildings/development

QA-INV-030 — Active building has valid parcel/access or explicit grandfathered/easement state.

QA-INV-031 — Parcel polygons do not overlap illegally.

QA-INV-032 — Parcel area is positive and above minimum unless classified residual/nondevelopable.

QA-INV-033 — Occupancy cannot exceed capacity except explicit temporary overcapacity.

QA-INV-034 — Building simulation identity is independent of render representation.

## Mobility

QA-INV-040 — Routed edge permits traveller/vehicle mode.

QA-INV-041 — Trip conservation: traveller/freight cannot be simultaneously at two authoritative locations.

QA-INV-042 — Goods transfer at receiver only after defined delivery completion.

QA-INV-043 — Edge flow cannot exceed modelled capacity without queue/overflow representation.

QA-INV-044 — Route cache result is invalidated when required graph epoch changes.

QA-INV-045 — Camera/render LOD cannot delete authoritative queue/trip.

## Utilities/services

QA-INV-050 — Delivered utility cannot exceed available reachable source/network capacity.

QA-INV-051 — Storage cannot discharge below zero or charge beyond capacity.

QA-INV-052 — Effective service capacity cannot exceed nominal capacity unless specification explicitly allows surge/temporary capacity.

QA-INV-053 — Consumer receiving zero connectivity cannot receive non-local network delivery.

QA-INV-054 — Service queue count cannot become negative.

## Time/determinism

QA-INV-060 — Simulation time is monotonic except explicit load/rollback operation.

QA-INV-061 — Paused game advances no authoritative state.

QA-INV-062 — Scheduled event ordering survives save/load.

QA-INV-063 — Named RNG streams are stable for identical version/seed/input.

## Save/data

QA-INV-070 — Interrupted write preserves previous known-good save.

QA-INV-071 — Save never persists transient ECS `Entity` as durable identity.

QA-INV-072 — Derived render/route caches are never sole authoritative state.

QA-INV-073 — Save references content/canon version.

## Governance/canon

QA-INV-080 — No gameplay productivity/intelligence/profession bonus is derived solely from species identity.

QA-INV-081 — In-world AI cannot gain sovereign authority through generic system configuration.

## Runtime invariant execution

Three levels:

- **Always-cheap** — enabled in development and test runs.
- **Sampled-expensive** — periodic development checks.
- **Offline audit** — full save/fixture consistency scan.

A production build may disable expensive checks but cannot rely on them for correctness.

## Failure handling

Invariant failure records:

- invariant ID;
- simulation minute;
- entity IDs;
- relevant inputs;
- system;
- seed;
- build/content/Universe SHA.

Automated tests fail immediately unless the test explicitly exercises invariant detection.

## Release requirement

No known invariant failure is acceptable in a release candidate unless the affected system is explicitly non-production/deferred and unreachable.
