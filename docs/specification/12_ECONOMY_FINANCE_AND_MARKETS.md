# 12 — Economy, Finance and Markets

## Economic philosophy

The city economy is a network of households, organisations, public institutions and external regions exchanging labour, goods, services and Commonwealth Credits.

KCB-ECO-001 — Money and resources MUST have identifiable sources/sinks or explicit abstraction rules.

KCB-ECO-002 — The economy SHOULD self-stabilise under ordinary conditions so the player manages policy and bottlenecks rather than manually pricing every product.

## Economic actors

- households;
- private firms;
- cooperatives;
- public institutions;
- utilities;
- construction contractors;
- external regions/markets;
- municipal government.

Actor accounts may be aggregated for performance but must reconcile with displayed totals.

## Municipal ledger

Revenue categories can include local tax shares, land/property levies, commercial/sector taxes, service fees, transit fares, utility fees, grants/transfers, land leases/sales and other authorised charges.

Expense categories include staff/service operations, maintenance, transit, utilities, debt service, capital projects, subsidies, social programmes, emergency response and intergovernmental payments.

Exact tax law is game-only until canonised.

KCB-ECO-010 — Budget panel MUST separate recurring operating balance from capital expenditure.

KCB-ECO-011 — Cash balance, committed funds and forecast balance MUST be distinct.

## Accounting period

Transactions occur continuously or periodically; reports aggregate monthly and annually.

KCB-ECO-020 — Displayed monthly projection MUST state whether it is run-rate or completed-period actual.

## Debt

Municipal borrowing records term, principal, rate, repayment schedule and borrowing-limit constraints.

KCB-ECO-030 — Debt finances capital timing; it is not free money.
KCB-ECO-031 — Credit conditions can react to fiscal health in advanced difficulty.

## Household economy

Households receive income/transfers and spend on housing, transport, food/consumer needs abstractly, services/leisure, fees/taxes and savings.

KCB-ECO-040 — Household purchasing power influences commercial demand.

## Business economy

Businesses track:
- revenue;
- wages;
- rent;
- input costs;
- utilities;
- freight;
- taxes;
- maintenance;
- profit/loss;
- inventory.

KCB-ECO-050 — A business location decision SHOULD account for expected profit, workforce, customer access, inputs and rent.

KCB-ECO-051 — Businesses can reduce activity before closure.

## External market

The wider Commonwealth is represented through market connectors.

For each traded category:
- external price index;
- import capacity;
- export demand;
- volatility;
- freight cost;
- strategic constraints.

KCB-ECO-060 — External market movement MUST be bounded and explainable.

KCB-ECO-061 — Concordia is a capital in a planetary economy; the game MUST not pretend it is a sealed economy.

## Inflation and price indices

A full macroeconomic inflation model is optional.

If implemented:
- nominal values need consistent base-year display;
- wages/rents/construction costs may move together;
- UI should support real versus nominal trend interpretation.

KCB-ECO-070 — Do not implement inflation unless it creates meaningful gameplay and can be explained.

## Subsidies and incentives

Policies may subsidise affordable housing, target industries, transit, energy transition, heritage restoration or district regeneration.

KCB-ECO-080 — Subsidies have explicit budget cost and eligibility.

## Procurement

Major public projects use estimates or bids rather than instant fixed prices when advanced economy is enabled.

Bid cost depends on materials, construction labour, urgency, complexity and market workload.

KCB-ECO-090 — Emergency acceleration SHOULD cost more and consume scarce contractor capacity.

## Fiscal feedback

Poor fiscal state can cause deferred maintenance, slower capital programme, borrowing pressure, political pressure or service cuts.

KCB-ECO-100 — Fiscal difficulty SHOULD create choices before hard insolvency.

## Metrics

Required:
- revenue/expense history;
- operating balance;
- cash;
- commitments;
- debt;
- sector output;
- household income distribution;
- unemployment;
- import/export balance;
- construction price index;
- housing cost burden.

KCB-ECO-110 — Every budget line can drill down to its primary contributors.

## Balance invariants

- no silent creation/destruction of municipal money;
- avoid double-counting between capital and operating costs;
- internal transfers flagged as transfers;
- external money flows labelled;
- negative prices forbidden unless an explicit disposal market supports them;
- integer/fixed-point accounting for authoritative ledgers.

## Acceptance criteria

- Construction boom raises project prices if contractor capacity is saturated.
- Lower transit fares reduce fare revenue but can alter accessibility and household costs.
- A profitable business can become unviable if freight disruption persists.
- Budget projection changes when a project is authorised but actual cash draw follows construction schedule.
