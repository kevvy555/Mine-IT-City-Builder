# 09 — Population, Households and Lifecycles

## Simulation goals

Population is the source of city demand, labour, travel, culture and political legitimacy. Citizens should feel persistent enough to create stories without requiring every citizen to be a heavyweight continuously simulated object.

KCB-POP-001 — The city MUST maintain a persistent population model whose totals reconcile with households, housing occupancy, employment and demographic flows.

KCB-POP-002 — Visible pedestrians are representations of simulated people/trips, not unrelated decorative crowds.

## Citizen record

A compact citizen record SHOULD contain:
- stable ID;
- birth date/age band;
- ancestry/cultural identity references;
- household ID;
- home building;
- education state;
- employment/student state;
- income band;
- health/well-being state;
- mobility/accessibility needs;
- preferred travel characteristics;
- current broad activity;
- significant life-event history;
- optional named/featured status.

KCB-POP-010 — Species/ancestry is identity and small physiological context, never a profession template.

## Households

Household is the principal residential/economic unit.

Household types may include single adult, couple, family with children, multigenerational, shared household, elderly, student/early-career and other mixed arrangements.

Household record:
- members;
- combined income;
- liquid budget;
- housing cost;
- preferred size/location;
- mobility assets if modelled;
- care/education needs;
- satisfaction;
- tenure;
- moving intention.

KCB-POP-020 — Housing decisions occur at household level while commute, education and leisure trips may occur at individual level.

## Lifecycle

Lifecycle states:
birth/adoption -> childhood -> education -> working-age/other activity -> retirement -> end of life.

KCB-POP-030 — Ageing MUST be continuous with age-band transitions, not random citizen replacement.

KCB-POP-031 — Education opportunities affect later qualifications rather than instantly converting a worker’s skill.

KCB-POP-032 — Long-lived games MUST allow multiple generations.

## Migration

In-migration attractiveness considers available housing, affordability, job opportunities, household matching, services, safety, environment, connectivity and the wider planetary economy.

Out-migration can result from chronic unaffordability, unemployment, poor services, household life changes, long-term dissatisfaction or external opportunity.

KCB-POP-040 — Migration MUST be rate-limited and smoothed.
KCB-POP-041 — A temporary service outage should not empty a district overnight.
KCB-POP-042 — Migration panels show push/pull reasons.

## Happiness, well-being and satisfaction

Avoid one magical happiness number internally.

Component domains:
- housing;
- income/security;
- health;
- education/care;
- travel burden;
- environment;
- recreation/culture;
- safety;
- service access;
- social/community;
- civic trust.

KCB-POP-050 — The UI MAY show a headline well-being score but MUST expose component contributions.

KCB-POP-051 — Different households weight components differently.

KCB-POP-052 — Satisfaction uses expectation/adaptation so a small service improvement can matter more in an underserved district than an already excellent one.

## Health

Population health is influenced by age, preventive care, environmental exposure, accidents, stress/travel burden, housing quality and outbreaks/events if enabled.

KCB-POP-060 — Routine health SHOULD mostly be aggregate; individual patient trips are instantiated when needed for service gameplay.

## Daily activity

Activity categories:
- home;
- work;
- education;
- shopping;
- service visit;
- leisure/culture;
- visiting;
- travel;
- medical;
- civic/event.

KCB-POP-070 — Citizens need not follow a second-by-second schedule for every day; use generated activity plans with event-driven variation.

KCB-POP-071 — Work/school trips create stable recurring demand but remote/flexible activity can reduce it where technology and role permit.

## Named citizens

Some citizens may become featured because they lead institutions, are civic representatives, found major businesses, participate in notable events, become long-lived city residents or are selected by the player.

KCB-POP-080 — Featured status increases history/UI detail but does not grant hidden statistical advantage.

## Aggregation

Population can be represented at several levels:
- persistent individual record;
- household;
- cohort for slow calculations;
- active trip agent;
- rendered pedestrian.

KCB-POP-090 — Aggregation must conserve population, household membership and economic totals.

KCB-POP-091 — Sampling for visual crowds must preserve broad demographic mix in the local area without fabricating impossible citizens.

## Demographic loss and household change

KCB-POP-100 — End-of-life events update population and household structure and may create service/cultural effects.

KCB-POP-101 — The exact ceremonial/service system is optional; the city must at minimum account for demographic loss and household consequences.

## Citizen information philosophy

KCB-POP-110 — Citizen panels expose simulation-relevant summaries rather than excessive personal detail by default.

## Population information

Panels:
- age pyramid;
- household composition;
- migration;
- births/end-of-life;
- income distribution;
- ancestry/cultural mix;
- education;
- employment;
- well-being domains;
- housing stress;
- average travel burden.

KCB-POP-120 — Statistics can be filtered by district and compared over time.

## Acceptance criteria

- Household move updates dwelling occupancy and commute possibilities.
- Child ages into education and later workforce.
- Housing shortage suppresses in-migration despite job demand.
- A transit improvement can improve well-being through reduced travel burden.
- Mixed-ancestry households are represented without special-case penalties.
- Population totals remain exact across simulation LOD transitions.
