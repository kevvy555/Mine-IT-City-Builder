# 04 — Causal and Directional Behaviour Tests

## Purpose

Many city-builder requirements are about cause and effect rather than an exact number.

A causal QA test changes one controlled input while keeping the reference fixture/seed constant and checks:

- direction;
- mechanism;
- time lag;
- bounded secondary effects;
- recovery.

## Test pattern

```text
Control run
Intervention run
Compare same observation window
Assert:
  primary metric direction
  causal trace
  secondary constraints
  no unrelated invariant break
```

## Mobility

### QA-CAU-001 — Road capacity

Intervention:
- increase capacity on constrained corridor.

Expect:
- immediate/near-term delay falls on affected corridor;
- route distribution may change;
- no population teleport;
- later induced demand MAY reduce benefit.

### QA-CAU-002 — Bridge closure

Intervention:
- close major bridge.

Expect:
- affected routes invalidate;
- alternate corridor volume rises;
- average affected travel time rises;
- inaccessible trips fail with reason.

### QA-CAU-003 — Transit frequency

Intervention:
- increase frequency on competitive line.

Expect:
- wait component falls;
- transit generalized cost falls;
- transit share/accessibility should not worsen absent capacity side effects.

## Housing/development

### QA-CAU-010 — Add housing supply

Intervention:
- permit/build significant accessible dwelling capacity.

Expect:
- vacancy/supply rises initially;
- affordability pressure should improve or grow more slowly;
- households may relocate;
- effect is not instantaneous citywide price reset.

### QA-CAU-011 — Improve accessibility

Intervention:
- add strong transport access to suitable underdeveloped area.

Expect:
- accessibility score rises;
- development suitability rises relative to control;
- development occurs subject to demand/construction capacity.

### QA-CAU-012 — Pollution externality

Intervention:
- introduce local pollution/noise.

Expect:
- exposure worsens;
- desirability/land-value pressure weakens subject to other factors;
- not an arbitrary immediate eviction event.

## Economy/industry

### QA-CAU-020 — Freight restriction

Intervention:
- reduce freight access to industry.

Expect:
- deliveries decline/delay;
- input inventory falls;
- production later falls;
- causal trace shows logistics/inventory.

### QA-CAU-021 — Construction capacity

Intervention:
- double demand while holding contractor/material capacity.

Expect:
- queue/lead time/cost pressure rises;
- projects do not all complete at original rate.

### QA-CAU-022 — Tax/policy

Intervention:
- change explicit economic policy.

Expect:
- only defined mechanisms change first;
- downstream outcomes follow with specified delay;
- no unrelated hidden multiplier.

## Utilities/services

### QA-CAU-030 — Power bottleneck

Intervention:
- restrict feeder while generation remains sufficient.

Expect:
- downstream delivered power falls;
- upstream generation remains adequate;
- service/business throughput follows power factor.

### QA-CAU-031 — Staff service

Intervention:
- increase clinic staffing with power/access unchanged.

Expect:
- effective capacity/queue improves until another factor becomes limiting.

### QA-CAU-032 — Congestion and emergency response

Intervention:
- create corridor congestion.

Expect:
- response travel time worsens;
- service nominal fleet count unchanged;
- effective coverage/outcome worsens.

## Environment/resilience

### QA-CAU-040 — Drainage capacity

Intervention:
- increase retention/drainage before same storm seed.

Expect:
- flooded area/depth/duration does not worsen without another explicit effect.

### QA-CAU-041 — Maintenance

Intervention:
- reduce maintenance for extended period.

Expect:
- condition degrades;
- failure risk/effective capacity worsens over time;
- restoration reduces risk subject to repair lag.

## Population/workforce

### QA-CAU-050 — Job access

Intervention:
- create jobs that are physically inaccessible.

Expect:
- headline vacant-job count may coexist with unemployment;
- geographic unemployment cause rises;
- no magical matching through disconnected network.

### QA-CAU-051 — Education capacity

Intervention:
- add staffed/powered accessible education capacity.

Expect:
- enrolment/wait improves;
- qualification effects appear only after programme duration.

## Governance/policy

### QA-CAU-060 — Transit priority policy

Expect chain:

```text
policy
→ junction/transit cost
→ transit reliability/travel
→ route/mode response
→ accessibility
```

No direct arbitrary land-value bonus is allowed.

## Recovery tests

Each destructive causal case should have a paired recovery run where the underlying cause is fixed.

QA-CAU-090 — A recoverable problem must show meaningful recovery within the modelled time horizon unless another explicit constraint prevents it.

## Acceptance envelopes

Directional tests use:

- must increase;
- must decrease;
- must not materially change;
- should remain within [min,max];
- should respond only after [lag];
- must preserve invariant.

Tolerance is versioned per fixture.

Tests must avoid asserting noisy exact values where design only specifies direction.
