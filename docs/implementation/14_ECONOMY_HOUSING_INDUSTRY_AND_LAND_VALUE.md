# 14 — Economy, Housing, Industry and Land Value

## Monetary model

All authoritative money uses fixed-point integer Commonwealth Credit units.

Every transfer records:

- timestamp;
- source account/category;
- destination account/category;
- amount;
- reason/reference entity.

A citywide source/sink report can reconcile money creation/removal.

## Account domains

- households;
- businesses;
- municipal operating budget;
- municipal capital/project budget;
- debt;
- external region/market;
- scenario grants/transfers.

Do not model every bank transaction if no gameplay depends on it; aggregate within domain while preserving traceable ledgers.

## Household economy

Household state:

- gross income;
- taxes/fees;
- housing cost;
- transport cost;
- essential consumption;
- discretionary budget;
- savings/debt abstraction.

Budget updates daily/weekly using deterministic schedules.

## Housing supply

Building use allocation creates dwelling units by category.

Each dwelling group stores:

- count;
- size band;
- quality/condition;
- tenure;
- asking/effective cost;
- occupancy;
- accessibility features if relevant.

Households match to housing through candidate indices rather than all-to-all search.

## Affordability

Affordability is separate from land value.

Metrics:

- housing-cost/income burden;
- available units by price/size;
- overcrowding;
- homelessness/temporary state if applicable;
- displacement risk.

## Land value

Per spatial cell/parcel smoothed score consumes:

- access to jobs/services;
- transit;
- public realm;
- environment;
- nuisance/pollution;
- safety/resilience;
- demand pressure;
- nearby vacancy/condition;
- policy/tax effects.

Update daily/weekly with hysteresis.

Land value is a signal to rent/price/development, not a direct happiness bonus.

## Business model

Business/organisation site tracks:

- workplace;
- employees/jobs;
- product/service;
- input inventory;
- output inventory/capacity;
- revenue;
- costs;
- profitability/mission budget;
- freight/loading requirement.

Birth/closure/relocation are rate-limited and explainable.

## Industry

Production recipe:

```text
inputs + labour + utilities + facility capacity
→ outputs + waste/externalities
```

Use 3–4 meaningful stages rather than excessive item micro-simulation.

Production is hourly/daily aggregate.

Physical goods require inventory and freight unless explicitly classified as abstract/local.

## External market

Off-map connectors expose:

- import/export categories;
- capacity;
- travel cost;
- price bands;
- reliability.

External market cannot magically absorb infinite goods.

## Construction economy

Construction projects consume:

- labour;
- materials;
- contractor capacity;
- finance;
- logistics.

Construction demand competes with other projects and can raise cost/lead time.

## Municipal budget

Separate:

- operating revenue/spend;
- capital commitments;
- cash;
- debt;
- forecast.

UI must never equate positive monthly operating balance with available capital cash.

## Economic tick ordering

Suggested daily ordering:

1. production/inventory accumulated hourly;
2. freight deliveries settle;
3. wages/income;
4. household expenses;
5. business revenue/cost;
6. taxes/fees;
7. municipal service spend;
8. housing/rent pressure;
9. business viability;
10. development demand;
11. report ledger reconciliation.

## Tasks

- IMP-ECO-001 — Implement fixed-point ledger/account model.
- IMP-ECO-002 — Implement daily reconciliation.
- IMP-ECO-003 — Implement household budgets.
- IMP-ECO-004 — Implement dwelling inventory.
- IMP-ECO-005 — Implement housing matcher.
- IMP-ECO-006 — Implement affordability metrics.
- IMP-ECO-007 — Implement land-value field.
- IMP-ECO-008 — Implement rent/price pressure.
- IMP-ECO-009 — Implement business model.
- IMP-ECO-010 — Implement production recipes.
- IMP-ECO-011 — Implement inventory.
- IMP-ECO-012 — Connect freight demand.
- IMP-ECO-013 — Implement external market capacity.
- IMP-ECO-014 — Implement municipal operating/capital budget.
- IMP-ECO-015 — Implement construction-market capacity.
- IMP-ECO-016 — Add money/inventory conservation tests.

## Exit criteria

- money source/sink ledger reconciles;
- inventory never becomes negative;
- unaffordable households can be distinguished from low-land-value areas;
- freight-starved business loses output for a traceable reason;
- positive operating balance does not imply spendable capital;
- 100-year soak does not exhibit unbounded money creation from rounding.
