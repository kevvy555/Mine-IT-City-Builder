# 24 — UI, UX, Info Views and Explainability

## UX principle

The simulation can be deeper than the default interface. Complexity is revealed progressively.

KCB-UI-001 — The first screen for a problem states:
- what is wrong;
- where;
- how severe;
- why;
- trend;
- likely actions.

KCB-UI-002 — Expert drill-down remains available without forcing it on every player.

## Main HUD

Persistent minimal elements:
- date/time/speed;
- treasury and operating trend;
- population and trend;
- major unresolved alert count;
- build/management entry points;
- active tool state.

KCB-UI-010 — HUD MUST not permanently display every resource/service metric.

## Context panel

Selecting an entity opens one reusable shell:
- identity;
- current status;
- key capacity;
- key demand;
- top causes;
- route/network links;
- history/trend;
- actions;
- deeper tabs.

KCB-UI-020 — Similar concepts appear in consistent positions across building types.

## Causal explanation format

Every major calculated state SHOULD provide structured factors:
- factor ID;
- sign/impact;
- current value;
- normal/reference value;
- link to source entity/overlay;
- recommended action category.

Example:
“Clinic effectiveness 62%”
- staffing: -18%
- power quality: -5%
- patient load: -12%
- building condition: -3%

KCB-UI-030 — The explanation is generated from the same authoritative calculation inputs, not manually duplicated prose that can drift.

## Info views / overlays

Core overlays:
- zoning/land use;
- development demand;
- land value;
- affordability;
- population;
- jobs/workforce;
- traffic flow;
- traffic volume;
- transit;
- pedestrian accessibility;
- freight;
- power;
- water/wastewater;
- drainage/flood;
- waste;
- digital network;
- health;
- education;
- emergency response;
- service access;
- air/noise/heat/water environment;
- ecology;
- building condition;
- maintenance;
- construction;
- heritage;
- district policy.

KCB-UI-040 — Overlay legends show units and whether values are instantaneous, averaged or forecast.

KCB-UI-041 — Overlay can filter to district/time-of-day where meaningful.

## Heatmap design

KCB-UI-050 — Colour is never the only information channel for critical state.
KCB-UI-051 — Patterns, contour lines, icons or numeric labels are available where required.
KCB-UI-052 — Positive/negative palettes are consistent across the game.

## Alerts

Alert structure:
- severity;
- category;
- affected count;
- first occurrence;
- duration;
- location;
- cause;
- trend;
- recommended inspections.

KCB-UI-060 — Similar alerts aggregate.
KCB-UI-061 — Repeated alert spam is rate-limited.
KCB-UI-062 — Resolved alerts move to history.

## Notifications versus alerts

Notification: informative milestone or completed action.
Alert: needs attention.
News: narrative interpretation.

KCB-UI-070 — These channels are visually distinct.

## Dashboards

City overview:
- population;
- economy;
- housing;
- mobility;
- services;
- environment;
- development;
- resilience.

Each dashboard:
- headline;
- trend;
- district comparison;
- worst bottleneck;
- time-series;
- relevant actions.

KCB-UI-080 — Default dashboard tells a story instead of presenting an unranked wall of numbers.

## Time-series charts

KCB-UI-090 — Charts support hover/focus values, time range, comparison and event markers.
KCB-UI-091 — Significant policy/project/event markers can be overlaid on trends.
KCB-UI-092 — Axes/units do not change misleadingly without clear indication.

## Budget UI

Must separate:
- actual;
- forecast;
- recurring;
- one-off;
- committed;
- available cash.

KCB-UI-100 — Positive operating balance does not hide unaffordable committed capital spending.

## Network tracing

For utility/service/freight problems:
“trace cause” can highlight:
source -> route/network -> bottleneck -> consumer.

KCB-UI-110 — Trace must work from both affected consumer and infrastructure source.

## Tooltips

KCB-UI-120 — Tooltips explain terms, not only restate labels.
KCB-UI-121 — Advanced formula/tuning detail can be shown in expanded tooltip or encyclopedia.

## Encyclopedia/help

Entries:
- mechanics;
- canon/lore;
- buildings;
- services;
- policies;
- resources;
- transport;
- metrics.

KCB-UI-130 — Lore and mechanic text are clearly separated so game tuning is not mistaken for canon.

## Onboarding

Tutorial method:
- contextual;
- goal-based;
- skippable;
- replayable;
- never disables basic exploration for long.

KCB-UI-140 — Tutorial teaches diagnosis before optimisation.

## Layout scalability

KCB-UI-150 — UI supports scalable text and density.
KCB-UI-151 — Panels adapt from wide desktop to narrower layouts without hiding critical actions.
KCB-UI-152 — Important map area remains visible while inspecting; panels may pin/collapse.

## Accessibility hooks

Every interactive control stores:
- accessible name;
- role;
- state;
- value;
- focus order;
- optional narration description.

KCB-UI-160 — Charts and maps have text summaries of key information.

## Acceptance criteria

- Player can diagnose why a building is underperforming in three interactions or fewer from its panel.
- Traffic flow and traffic volume are distinguishable.
- Budget panel cannot confuse committed spend with free cash.
- An alert can focus camera and open correct overlay.
- A colour-blind mode retains meaningful overlay differences.
