# 19 — UI, Explainability, Overlays and Dashboards

## Explainability architecture

Every major simulation domain exposes structured cause factors.

Example:

```text
BuildingPerformance
  value: 0.62
  factors:
    workforce       0.88
    power           0.70
    freightInputs   0.55
    access           0.93
  limitingFactor: freightInputs
  traceRef: freight-shortage:...
```

UI formats the same factors that calculation used.

No second “UI explanation formula.”

## Cause graph

For traceable network/system problems, create a lightweight cause graph:

- affected entity;
- metric;
- contributing factor;
- upstream entity/network;
- bottleneck;
- actionable controls.

Examples:

- house has no power -> local feeder overloaded -> substation capacity;
- factory output low -> ore inventory empty -> delivery delayed -> freight corridor congested;
- clinic queue high -> staffing shortfall + travel catchment demand.

## Context panel

Sections:

1. identity/status;
2. headline performance;
3. limiting factors;
4. inputs/outputs;
5. access/routes;
6. finance where applicable;
7. history;
8. actions.

Keep top three causes immediately visible.

## Alerts

Alert record:

- stable alert type;
- severity;
- affected area/entities;
- start time;
- current state;
- cause summary;
- recommended/available actions;
- deduplication key.

Use hysteresis and duration thresholds.

One systemic outage creates one meaningful aggregated alert, not 20,000 building popups.

## Notifications/news

Different channels:

- toast/status — short acknowledgement;
- notification — completed/scheduled action;
- alert — player attention;
- news — narrative interpretation/history.

## Overlays

Required v1 overlay framework supports:

- zoning;
- traffic speed;
- traffic volume;
- volume/capacity;
- public transit;
- power/utilities;
- service access;
- housing;
- jobs;
- land value;
- affordability;
- pollution/environment;
- districts;
- construction.

Overlay data is generated into chunk buffers/textures and does not clone materials.

## Overlay accessibility

Each overlay defines:

- colour palette;
- value bands;
- pattern/line/shape alternative;
- legend;
- textual area summary;
- min/max/units;
- no-data state.

## Dashboards

City dashboard cards:

- population;
- housing;
- economy;
- mobility;
- services;
- utilities;
- environment;
- development;
- resilience.

Each shows:

- current headline;
- trend;
- worst district/bottleneck;
- one recommended diagnostic jump;
- time range.

## Time series

History service provides downsampled fixed-window data:

- hourly short window;
- daily;
- monthly;
- yearly.

Older data compacts rather than storing per-minute metrics forever.

Event markers reference history records.

## Budget UI

Explicit rows:

- actual operating result;
- forecast operating result;
- cash;
- committed capital;
- available capital;
- debt/service;
- major projects.

Warnings prevent misleading “surplus” interpretation.

## Search

Search index covers:

- roads;
- districts;
- buildings;
- services;
- transit lines/stops;
- named citizens where exposed;
- policies;
- encyclopedia entries.

Search is asynchronous/indexed.

## Encyclopedia

Separate tabs/labels:

- Mechanics;
- Universe lore.

Game tuning text must never masquerade as canon.

## Performance

UI updates at human-readable cadence, not simulation micro-step.

Typical:

- selected object headline 4 Hz;
- charts 1–2 Hz;
- city dashboard 1 Hz or slower;
- long history on request.

Avoid binding thousands of UI elements to ECS queries.

## Tasks

- IMP-UX-001 — Define cause-factor schema.
- IMP-UX-002 — Define trace/cause graph.
- IMP-UX-003 — Implement context panel view model.
- IMP-UX-004 — Implement alert aggregation/hysteresis.
- IMP-UX-005 — Implement notification/news channels.
- IMP-UX-006 — Implement overlay registry.
- IMP-UX-007 — Implement chunk overlay buffers.
- IMP-UX-008 — Implement accessible legends/text summaries.
- IMP-UX-009 — Implement city dashboard.
- IMP-UX-010 — Implement history/downsampling.
- IMP-UX-011 — Implement charts/event markers.
- IMP-UX-012 — Implement budget UI.
- IMP-UX-013 — Implement search index.
- IMP-UX-014 — Implement mechanics/lore encyclopedia split.
- IMP-UX-015 — Add three-interaction diagnosis tests.

## Exit criteria

- underperforming building reveals top cause within three interactions;
- utility trace reaches bottleneck/source;
- traffic speed and volume are distinct;
- alert can focus affected area and overlay;
- colour-blind/monochrome overlay still communicates bands;
- UI does not query whole city each render frame.
