# 16 — Health, Safety, Emergency and Resilience

## Philosophy

Emergency systems should test the city the player actually built. A resilient city performs well because it has redundancy, access, maintenance, staffing and plans, not because the game grants a hidden resilience score.

## Hazard classes

- infrastructure failure;
- building incident;
- transport incident;
- fire/thermal event;
- severe weather;
- flooding;
- public-health surge;
- hazardous-material release;
- utility outage;
- communications disruption;
- major crowd/event pressure.

Planet-wide existential disasters are not normal Concordia gameplay.

KCB-RES-001 — Incident probability MUST be influenced by relevant conditions and bounded so normal operation does not feel arbitrarily punitive.

KCB-RES-002 — Difficulty modifies incident frequency/severity transparently.

## Emergency lifecycle

1. detection;
2. dispatch/automatic isolation;
3. travel/access;
4. on-site response;
5. stabilisation;
6. recovery;
7. investigation;
8. repair/lessons.

KCB-RES-010 — Each phase may be limited by different resources.

## Fire and rescue

KCB-RES-020 — Fire/rescue response depends on station readiness, staff, vehicles, route time, site access and water/suppression infrastructure.

KCB-RES-021 — Modern materials and sensors make major fires uncommon but not impossible.

KCB-RES-022 — Dense/tall/complex structures require suitable specialist capacity.

## Medical emergency

KCB-RES-030 — Emergency medical demand uses dispatch, travel, receiving capacity and treatment throughput.

KCB-RES-031 — Overloaded receiving facilities can divert demand to alternatives.

## Public health

Routine care is covered by health services; surge events may create:
- increased consultations;
- absenteeism;
- reduced activity;
- targeted public measures.

KCB-RES-040 — Public-health events MUST NOT be used to stereotype a people or district.
KCB-RES-041 — Prevention/health-system capacity should matter more than random citizen attrition.

## Flood and weather

KCB-RES-050 — Flood risk depends on terrain, drainage, water bodies and rainfall event.
KCB-RES-051 — Critical infrastructure can be protected by elevation, barriers, redundancy or local backup.
KCB-RES-052 — Recovery includes cleanup/repair, not instant reset.

## Infrastructure resilience

Resilience mechanisms:
- N+1/N+2 capacity where appropriate;
- alternate network routes;
- local storage;
- spare vehicles;
- mutual aid;
- emergency stock;
- preventive maintenance;
- monitoring;
- isolation switches.

KCB-RES-060 — Redundancy has capital/maintenance cost and therefore creates a strategic trade-off.

## Emergency operations centre

A civic emergency-operations capability MAY aggregate:
- incident map;
- resource availability;
- hospital/fire capacity;
- utility status;
- evacuation/shelter;
- temporary traffic control.

KCB-RES-070 — Major events SHOULD unlock a unified emergency UI rather than spamming unrelated alerts.

## Evacuation and shelter

KCB-RES-080 — Where local evacuation is needed, destinations and routes must have capacity.
KCB-RES-081 — Transit and walking can participate; the system must not assume universal private-car ownership.
KCB-RES-082 — Sheltering can occur in designated civic facilities.

## Repair and recovery

KCB-RES-090 — Damage creates repair projects that compete for construction/maintenance resources.
KCB-RES-091 — Temporary repairs can restore partial capacity faster than full reconstruction.
KCB-RES-092 — Insurance/fiscal recovery mechanics are optional but damage costs must enter the economy somewhere.

## Learning

KCB-RES-100 — After significant events, an after-action panel SHOULD show what failed, what worked and what investments would have reduced impact.

KCB-RES-101 — Repeated identical failure without player action may increase public/political pressure.

## Alerts

Alert severity:
- advisory;
- degraded;
- serious;
- critical.

KCB-RES-110 — Alerts require thresholds and persistence to avoid alarm fatigue.
KCB-RES-111 — Critical alerts identify location, affected systems and immediate recommended actions.

## Resilience metrics

- critical-service uptime;
- average response;
- reserve margin;
- network redundancy;
- maintenance backlog;
- recovery time;
- population exposed to hazard;
- emergency capacity.

KCB-RES-120 — A single resilience score MAY summarize but cannot replace component metrics.

## Acceptance criteria

- A bridge incident changes response routing.
- Backup power keeps a hospital functional through a local outage.
- Redundant water paths reduce affected customers after a trunk failure.
- Flooding follows low terrain/drainage limits.
- Recovery consumes real repair capacity.
- After-action review identifies the causal bottleneck.
