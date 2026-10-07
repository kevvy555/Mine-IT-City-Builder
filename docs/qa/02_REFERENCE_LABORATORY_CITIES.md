# 02 — Reference Laboratory Cities

## Purpose

Reference laboratory cities are deterministic fixtures designed to produce known city conditions.

They are QA assets, not player scenarios and not Universe canon.

Each fixture has:

- stable fixture ID;
- root seed;
- source build/content version;
- city topology;
- initial state;
- scripted player/system actions;
- observation windows;
- expected metric envelope;
- relevant KCB/QA IDs.

## Core fixtures

### QA-LAB-001 — Balanced District

Purpose:
- healthy baseline.

State:
- adequate housing;
- jobs approximately aligned with workforce;
- functioning roads/transit;
- sufficient power/services;
- balanced budget.

Expected:
- no systemic shortage;
- bounded vacancy/unemployment;
- stable finances/population within configured envelope.

Used as control for many causal tests.

### QA-LAB-002 — Disconnected Suburb

State:
- residential area with weak network connection;
- jobs/services elsewhere.

Expected:
- longer/unfulfilled trips;
- lower job/service access;
- development pressure weaker than connected control.

Repairing access should improve those measures.

### QA-LAB-003 — Congested Corridor

State:
- concentrated OD demand through one constrained corridor.

Expected:
- queue/delay;
- slower effective service;
- route/departure/mode adaptation;
- speed and volume overlays differ.

### QA-LAB-004 — Transit-Rich Centre

State:
- high-density centre;
- competitive frequent transit;
- constrained road capacity.

Expected:
- meaningful transit share;
- crowding/capacity visible;
- better accessibility than equivalent no-transit control.

### QA-LAB-005 — Freight-Starved Industry

State:
- productive facility with low/no effective freight connection.

Expected:
- input inventory falls;
- production falls;
- revenue/job effects lag appropriately;
- no magical replenishment.

### QA-LAB-006 — Power Bottleneck

State:
- total generation sufficient;
- downstream distribution bottleneck.

Expected:
- affected consumers underpowered;
- upstream generation not falsely reported as shortage cause;
- causal trace reaches bottleneck edge.

### QA-LAB-007 — Housing Affordability Crisis

State:
- strong jobs/demand;
- constrained housing supply.

Expected:
- vacancy falls;
- housing-cost burden rises;
- displacement/out-migration pressure increases;
- land value and affordability remain distinct metrics.

### QA-LAB-008 — Job Crisis

State:
- housing/population exceeds accessible job capacity.

Expected:
- unemployment rises;
- classification identifies cyclical/geographic/skill causes;
- migration/wellbeing react over realistic delay.

### QA-LAB-009 — Service Capacity Crisis

State:
- insufficient clinic/school capacity.

Expected:
- queues/waits increase;
- effective service worsens;
- adding staffed/powered capacity improves outcome.

### QA-LAB-010 — Flood and Recovery

State:
- drainage/retention insufficient for deterministic event.

Expected:
- exposed routes/buildings affected;
- response/repair demand;
- temporary service/logistics disruption;
- recovery after mitigation.

### QA-LAB-011 — Fiscal Stress

State:
- recurring service/project costs exceed sustainable revenue.

Expected:
- cash/forecast/commitment indicators diverge correctly;
- debt/cuts/delays as configured;
- no free money.

### QA-LAB-012 — Ageing City

State:
- long-lived stable city with low migration.

Expected:
- age distribution changes;
- workforce/service demands change;
- no ID/time overflow.

### QA-LAB-013 — Bridge Closure

State:
- two areas connected by major bridge plus slower alternate route.

Script:
- establish steady state;
- close bridge.

Expected:
- affected routes invalidate;
- alternate load rises;
- travel time rises;
- congestion is conserved off-camera.

### QA-LAB-014 — Growth Corridor

State:
- undeveloped/underdeveloped land;
- transport/accessibility intervention.

Expected:
- accessibility improves;
- suitability/development shifts toward corridor over time;
- not instant arbitrary construction.

### QA-LAB-015 — Recovery City

State:
- simultaneous but recoverable housing, utility, traffic and fiscal stress.

Script:
- apply targeted fixes in stages.

Expected:
- root causes improve;
- city recovers through specified mechanisms;
- no permanent hidden doom loop.

## Scale fixtures

### QA-LAB-B10K

Synthetic 10k population benchmark.

### QA-LAB-B50K

Synthetic 50k integration benchmark.

### QA-LAB-B250K

Production-scale 250k benchmark.

### QA-LAB-B1M

One-million stretch benchmark; not a v1 release blocker unless promoted.

## Fixture integrity

QA-LAB-100 — Fixture input is version-controlled.

QA-LAB-101 — Fixture generation is deterministic.

QA-LAB-102 — Synthetic fixture content is clearly marked non-canonical.

QA-LAB-103 — Expected envelopes are versioned and changed only with documented design/spec reason.

QA-LAB-104 — Baseline update PR shows before/after metrics rather than blindly overwriting goldens.

## Required outputs

Each fixture exports:

- population;
- households;
- jobs;
- unemployment;
- dwellings/vacancy;
- money/ledger summary;
- business/inventory;
- trips/completion/mode;
- traffic delay/queue;
- utility demand/delivery;
- service demand/capacity;
- environment;
- construction/development;
- performance when relevant.

These become reusable QA dashboards.
