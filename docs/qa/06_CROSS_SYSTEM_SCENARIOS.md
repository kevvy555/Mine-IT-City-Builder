# 06 — Cross-System Scenario Validation

## Purpose

Verify the city behaves coherently when multiple correct subsystems interact.

A system can pass unit tests and still create a broken city when connected.

## QA-XSYS-001 — Housing growth cascade

Initial:
- strong employment;
- constrained housing.

Action:
- enable well-connected residential development.

Expected chain:

```text
zoning/access
→ development
→ dwellings
→ migration/relocation
→ population
→ trips
→ service demand
→ municipal/economic effects
```

Validate:
- no stage bypassed;
- time lags sensible;
- new housing can increase traffic/services rather than being pure benefit.

## QA-XSYS-002 — Transit-oriented development

Action:
- add frequent transit to suitable corridor.

Expected:
- accessibility improves;
- mode share changes;
- development desirability changes;
- traffic effects depend on demand;
- land/housing effects lag.

## QA-XSYS-003 — Industrial logistics cascade

Action:
- expand production without freight infrastructure.

Expected:
- production demand rises;
- loading/freight bottleneck appears;
- inventory constraint limits output;
- economic/job effects follow.

Then add logistics capacity and verify recovery.

## QA-XSYS-004 — Utility-service cascade

Action:
- create power bottleneck serving clinic district.

Expected:

```text
network bottleneck
→ delivered power
→ clinic effective capacity
→ queue/wait
→ health/service metric
→ alert
```

UI trace must reflect this exact chain.

## QA-XSYS-005 — Congestion/service cascade

Action:
- overload major corridor.

Expected:
- journey times;
- freight;
- emergency/service response;
- job accessibility;
- possibly development pressure.

No subsystem may ignore congestion if specification says physical access matters.

## QA-XSYS-006 — Fiscal expansion trap

Action:
- build capital projects and service capacity faster than sustainable revenue.

Expected:
- committed capital shown;
- operating costs appear;
- cash/debt stress;
- projects may delay;
- city remains recoverable through explicit measures.

## QA-XSYS-007 — Flood resilience

Same deterministic storm:

Control:
- weak drainage.

Intervention:
- retention + drainage + resilient network.

Compare:
- flooded assets;
- closures;
- service disruption;
- repair cost;
- recovery time.

## QA-XSYS-008 — Ageing and workforce

Run decades.

Expected chain:
- ageing cohort;
- labour participation change;
- service demand;
- education/migration response;
- fiscal impact.

No abrupt annual population reclassification artefacts.

## QA-XSYS-009 — Policy mechanism

Apply policy with documented mechanism.

Verify:
- policy changes only intended intermediate variables;
- downstream outcomes result from those;
- removing policy reverses/relaxes effect subject to hysteresis/history.

## QA-XSYS-010 — Recovery from compound failure

Use QA-LAB-015.

Induce:
- traffic;
- power;
- housing;
- fiscal stress.

Repair root causes one at a time.

Expected:
- diagnostic limiting factor changes as each is fixed;
- city can recover;
- hidden permanent penalties absent unless specified.

## Scenario assertions

Each scenario defines:

- precondition;
- scripted intervention;
- observation period;
- expected direction;
- acceptable range;
- expected limiting/cause IDs;
- invariants;
- performance ceiling if relevant.

## Differential comparison

Run control and intervention with:

- same seed;
- same initial save;
- same unrelated commands;
- only one intended difference where possible.

This makes causal drift detectable.
