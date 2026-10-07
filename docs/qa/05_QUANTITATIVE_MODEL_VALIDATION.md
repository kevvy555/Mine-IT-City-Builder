# 05 — Quantitative Model Validation

## Purpose

Verify formulas, curves and tuning produce plausible bounded behaviour before relying on emergent whole-city results.

This QA layer tests the model mathematically.

## Validation methods

- boundary-value tests;
- monotonicity tests;
- dimensional/unit tests;
- sensitivity sweeps;
- parameter grids;
- equilibrium/stability tests;
- adversarial/extreme inputs;
- Monte Carlo with deterministic seeds;
- comparison to stated design envelopes.

## Service capacity

For:

```text
effective = nominal × staffing × utility × condition × budget/input × access
```

validate:

- every factor in [0,1] unless explicitly defined otherwise;
- zero hard dependency can produce zero/defined emergency minimum;
- increasing one factor cannot reduce effective capacity absent documented interaction;
- output never exceeds allowed nominal/surge limit.

QA-MOD-001.

## Land value

Validate:

- bounded index;
- no single ordinary factor forces max/min instantly;
- smoothing prevents oscillation;
- accessibility improvement tends positive;
- severe pollution tends negative;
- affordability remains distinct from land value.

QA-MOD-010.

## Housing matching

Sweep:

- household income;
- size;
- dwelling cost;
- distance/access;
- vacancy.

Validate:

- unaffordable unit is not preferred merely because high land value;
- appropriate households rank feasible units above impossible ones;
- no all-to-all runtime explosion.

QA-MOD-020.

## Employment matching

Sweep:

- qualifications;
- wages;
- distance;
- vacancies;
- shifts.

Validate:

- inaccessible jobs cannot satisfy employment;
- qualification mismatch is classified;
- small wage differences do not create daily churn because hysteresis exists.

QA-MOD-030.

## Route cost

Test:

- time;
- fare;
- transfers;
- reliability;
- comfort;
- restrictions.

Validate:

- prohibited edge is never chosen;
- lower generalized cost wins when all else equal;
- weights remain nonnegative/defined;
- emergency profile obeys its different permissions.

QA-MOD-040.

## Congestion

Validate link/queue model:

- flow below capacity does not create runaway queue;
- demand above capacity grows queue;
- spillback occurs only with downstream storage constraint;
- clearing demand drains queue;
- delays feed expected travel cost.

QA-MOD-050.

## Production

For each recipe:

- required input conservation;
- labour/utilities/capacity limitations;
- no negative inventory;
- no output when hard-required input absent;
- partial factors scale as specified.

QA-MOD-060.

## Household finance

Validate:

- income/expense order;
- housing burden;
- fixed-point rounding;
- long-run no rounding money creation.

QA-MOD-070.

## Migration

Sweep city attractiveness components.

Validate:

- no unlimited migration into zero housing;
- severe unemployment/affordability constrain inflow;
- migration response has lag/capacity;
- external pool/capacity constraints respected.

QA-MOD-080.

## Development demand

Validate:

- demand alone does not develop invalid parcel;
- zoning/access/utilities can block;
- improved expected viability can raise rank;
- construction capacity limits throughput.

QA-MOD-090.

## Utility allocation

Validate network topologies:

- tree;
- mesh;
- bottleneck;
- disconnected island;
- storage.

Check conservation/capacity/priorities.

QA-MOD-100.

## Parameter safety

Every data-driven numeric setting defines:

- units;
- valid range;
- default;
- whether zero is meaningful;
- whether negative allowed;
- overflow handling.

Content validation fails invalid values.

## Balance changes

Any tuning change that materially shifts baseline reference-city envelopes requires:

- reason;
- before/after charts;
- affected QA IDs;
- approval as design/balance change rather than hidden test-golden update.
