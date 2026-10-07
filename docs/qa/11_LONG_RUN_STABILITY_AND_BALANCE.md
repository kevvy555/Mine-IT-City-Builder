# 11 — Long-Run Stability and Balance QA

## Purpose

A city builder can appear correct for hours and become mathematically broken after decades. Long-run QA is a release requirement.

## Simulation horizons

Run deterministic fixtures for:

- 1 year;
- 10 years;
- 100 years;
- 500 years stress;
- 1,000 years optional scenario/stress where performance permits.

## Core monitored metrics

Population:
- total;
- births/deaths;
- migration;
- age distribution;
- households.

Housing:
- units;
- occupancy;
- vacancy;
- burden/affordability;
- homelessness/temporary state where enabled.

Work:
- jobs;
- employment;
- unemployment causes;
- qualification mismatch.

Economy:
- money sources/sinks;
- household finances;
- business births/closures;
- prices/rents;
- municipal operating/capital/debt.

Mobility:
- trips;
- completion;
- mode share;
- travel time;
- queue/congestion;
- route failures.

Industry:
- production;
- inventories;
- freight;
- shortages.

Services/utilities:
- demand;
- nominal/effective capacity;
- outage frequency;
- queues.

Environment:
- pollution;
- flooding;
- habitat;
- incident frequency.

Technical:
- entity count;
- event queue;
- history buffers;
- cache sizes;
- save size;
- memory.

## Pathology tests

QA-LONG-001 — no unbounded free-money growth from rounding/source bug.

QA-LONG-002 — no unavoidable population extinction/explosion in Balanced District without defined external cause.

QA-LONG-003 — land value does not converge to all-min/all-max.

QA-LONG-004 — business creation/closure reaches plausible dynamic equilibrium rather than total extinction/explosion.

QA-LONG-005 — event queue does not grow without bound from processed events.

QA-LONG-006 — history buffers compact as designed.

QA-LONG-007 — stable IDs/counters do not overflow.

QA-LONG-008 — save size growth is related to real city/history growth and bounded by compaction.

QA-LONG-009 — route/cache memory remains bounded.

QA-LONG-010 — maintenance/condition system does not force inevitable universal collapse.

## Balance envelopes

Do not require one exact “correct city.”

Define acceptable outcome ranges for multiple strategies.

Examples:
- car-heavier;
- transit-heavy;
- compact;
- distributed;
- higher-service/high-tax;
- leaner-service/lower-tax;
- growth-oriented;
- ecological/resilience-oriented.

A healthy strategy should have trade-offs rather than one mathematically dominant solution.

## Recovery

QA-LONG-020 — Deliberately stressed city can recover when root causes are corrected.

Measure:
- time to service recovery;
- population stabilisation;
- fiscal recovery;
- queue clearance;
- housing recovery.

## Sensitivity

Vary key tuning +/- 5%, 10%, 20%.

Look for discontinuities where tiny changes cause catastrophic outcome.

Large nonlinear thresholds may be valid, but must be deliberate and explainable.

## Seed robustness

Run reference scenarios across a seed set.

Expected:
- variation;
- same broad behavioural envelope;
- no small subset of seeds consistently breaks invariants/economy.

## 100-year release gate

Before production foundation:
- all required 100-year fixtures finish;
- no invariant failure;
- no unexplained runaway;
- no unbounded technical structure growth;
- save/reload during soak remains valid.

## 500-year stress

May surface balance extremes not release-blocking if v1 expected play horizon is shorter, but technical corruption/overflow/data growth failures are blockers regardless of horizon.
