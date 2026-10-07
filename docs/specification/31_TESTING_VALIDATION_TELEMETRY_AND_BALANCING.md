# 31 — Testing, Validation, Telemetry and Balancing

## Testing philosophy

The game’s complexity demands tests at three levels:
- local rule correctness;
- cross-system integration;
- long-run emergent behaviour.

A beautiful ten-minute demo is insufficient if the economy diverges after fifty simulated years.

KCB-QA-001 — Every normative requirement in the specification must be traceable to one or more implementation work items and verification methods.

## Unit tests

Examples:
- fixed-point money calculations;
- demand factor;
- service capacity;
- parcel geometry;
- zoning eligibility;
- utility capacity;
- route cost;
- inventory conservation;
- migration eligibility;
- policy precedence;
- save migration.

KCB-QA-010 — Pure formulas are tested with boundary/zero/overflow cases.

## Property/invariant tests

Core invariants:
- population cannot become negative;
- occupied dwellings reconcile with households;
- employees reconcile with jobs;
- money ledger balances against recorded sources/sinks;
- inventories do not become negative;
- utility flow cannot exceed edge/source capacity;
- network path uses allowed modes;
- no building develops without access;
- canon IDs remain resolvable;
- deterministic run uses stable seed.

KCB-QA-020 — Invariant checks run in developer builds and long simulations.

## Integration scenarios

Required deterministic fixtures:
1. small balanced district;
2. disconnected suburb;
3. congested corridor;
4. transit-rich centre;
5. freight-starved industrial zone;
6. utility bottleneck;
7. housing affordability crisis;
8. understaffed service network;
9. flood/drainage event;
10. fiscal stress city;
11. long-term ageing city;
12. mod/content compatibility fixture.

KCB-QA-030 — Fixtures have expected metric ranges, not only “does not crash.”

## Golden city benchmarks

Maintain seeded benchmark saves at increasing scales.

Metrics:
- simulation step;
- route time;
- memory;
- frame performance;
- economic totals;
- trip completion;
- migration;
- service quality.

KCB-QA-040 — Performance regressions beyond agreed thresholds fail or flag CI.

## Long-run soak

Simulate:
- 1 year;
- 10 years;
- 100 years;
- stretch 500+ years at accelerated non-rendered test mode.

Check:
- runaway money;
- population explosion/collapse;
- land-value saturation;
- empty labour markets;
- accumulating orphan entities;
- unbounded event queues;
- save growth;
- ID overflow;
- trend-buffer growth.

KCB-QA-050 — Long-run test supports headless/fast simulation independent from rendering where engine permits.

## Determinism tests

KCB-QA-060 — Same seed/input script produces repeatable reference metrics.
KCB-QA-061 — Save/reload midpoint matches uninterrupted run within required tolerance.
KCB-QA-062 — Graphics quality changes do not alter authoritative macro outcomes.

## Content validation

CI validates:
- IDs;
- references;
- localisation;
- asset presence;
- building grammar compatibility;
- district theme references;
- recipe loops;
- upgrade dependencies;
- progression tree reachability.

KCB-QA-070 — No authored content ships with missing reference placeholders.

## Visual QA

For each canonical atlas tile:
- standard camera position;
- day;
- night optional;
- overlay capture;
- reference atlas comparison.

Check:
- district identity;
- palette;
- seam continuity;
- landmark location;
- performance;
- no stretched/missing materials.

KCB-QA-080 — Screenshot baselines detect accidental major visual regression where practical.

## Simulation balancing

Balance process:
1. define desired player experience;
2. define observable metrics;
3. run seeded batch simulations;
4. tune parameters;
5. playtest;
6. validate multiple strategies;
7. lock baseline;
8. regression test.

KCB-QA-090 — Do not balance solely by changing hidden multipliers until one save “feels right.”

## Telemetry

Optional player telemetry, if implemented, should collect only product-relevant aggregates with consent/appropriate policy.

Useful aggregate events:
- tool usage;
- tutorial abandonment;
- city scale/performance;
- common failure categories;
- progression choices;
- crash/performance diagnostics.

KCB-QA-100 — Simulation debug telemetry used in development is distinct from deployed player analytics.

KCB-QA-101 — No telemetry is required for core offline gameplay.

## Balance dashboards

Development tools SHOULD show:
- resource conservation;
- money sources/sinks;
- trip reason/mode;
- service demand/capacity;
- housing supply by tier;
- workforce mismatch;
- business births/closures;
- project costs;
- event frequency.

KCB-QA-110 — Every major balancing variable is data-driven where practical.

## Fuzz tests

Randomly mutate:
- network edits;
- zone boundaries;
- building removal;
- policy toggles;
- save/load timing;
- high-speed simulation.

KCB-QA-120 — Fuzz run checks invariants and crashes rather than expected exact city outcome.

## Accessibility QA

Test:
- keyboard-only;
- controller-only if supported;
- large UI scale;
- high contrast;
- colour-vision filters;
- narration;
- reduced motion;
- subtitles.

KCB-QA-130 — Accessibility tests are release criteria, not optional post-launch polish.

## Acceptance criteria

- 100-year soak finishes without invariant failure.
- Save/reload determinism fixture stays in tolerance.
- Canon reference validation catches a deliberately broken ID.
- Benchmark catches a deliberately introduced pathfinding slowdown.
- Multiple viable transport/housing strategies can reach scenario goals.
