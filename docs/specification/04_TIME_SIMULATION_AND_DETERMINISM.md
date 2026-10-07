# 04 — Time, Simulation and Determinism

## Time model

The game must support city-scale change across minutes, days, years and centuries without requiring every subsystem to update every rendered frame.

Time layers:
- render time;
- real-time input/UI time;
- simulation micro-step;
- operational interval;
- day;
- accounting period;
- season;
- year;
- long-term planning period.

KCB-TIME-001 — Rendering frame rate MUST NOT determine simulation outcomes.

KCB-TIME-002 — The core simulation MUST use deterministic or bounded fixed-step updates for systems whose ordering affects outcomes.

KCB-TIME-003 — UI animation and camera movement continue while the city simulation is paused.

## Speed controls

Required:
- pause;
- normal;
- fast;
- very fast;
- optional laboratory single-step.

KCB-TIME-010 — Speed changes multiply simulation throughput, not economic quantities.

KCB-TIME-011 — If the machine cannot keep up with requested speed, the game MUST reduce achieved simulation speed rather than silently dropping mandatory simulation steps.

KCB-TIME-012 — Visual agent animation MAY interpolate or simplify at high game speeds.

## Recommended scheduling

Example logical cadences, to be tuned:
- traffic movement: sub-second simulated intervals;
- transit operations: seconds;
- utility network balancing: seconds/minutes;
- service dispatch: minutes;
- household trip planning: event-driven plus periodic;
- business production: hourly;
- building condition: daily;
- household budget: daily/weekly;
- city budget accrual: daily with monthly reporting;
- education progression: daily/term aggregate;
- births/ageing: daily;
- migration: daily/weekly;
- land value: daily/weekly smoothed;
- policy effects: staged;
- long-term heritage/district identity: monthly/yearly.

KCB-TIME-020 — Each subsystem MUST document update cadence, trigger events and whether catch-up aggregation is safe.

KCB-TIME-021 — Slow systems MUST not be recalculated every micro-step.

## Event scheduling

Use a deterministic event queue for:
- construction completion;
- maintenance;
- lease/contract expiry;
- school terms;
- scheduled transit;
- festivals;
- policy effective dates;
- debt servicing;
- elections/mandate reviews if enabled;
- planned infrastructure outages;
- emergency recovery stages.

KCB-TIME-030 — Scheduled events MUST store simulation timestamp and stable tie-break order.

## Determinism

Determinism is important for:
- reproducible bugs;
- balance tests;
- replay;
- scenario scoring;
- save/load equivalence.

KCB-TIME-040 — A new game records a root random seed.

KCB-TIME-041 — Independent subsystems SHOULD derive named random streams from the root seed so adding randomness to one subsystem does not reorder all others.

KCB-TIME-042 — Random choice MUST NOT depend on iteration order of unordered collections.

KCB-TIME-043 — Given identical version, content, seed and player inputs, deterministic test mode MUST reproduce macro metrics within defined tolerance.

## Floating-point and platform variance

KCB-TIME-050 — The implementation plan MUST choose which state requires bitwise determinism and which only requires outcome tolerance.

KCB-TIME-051 — Monetary ledgers SHOULD use integer/fixed-point minor units rather than binary floating point.

KCB-TIME-052 — Long-running counters MUST be overflow-safe for centuries of play.

## Pause behaviour

While paused:
- inspect any entity;
- build/plan;
- edit policies;
- queue construction;
- inspect routes;
- use photo mode;
- save/load;
- configure overlays.

KCB-TIME-060 — Player edits made while paused become effective at a clearly defined simulation boundary when unpaused, except purely spatial previews.

## Construction time

Construction is physical and staged:
1. approved/planned;
2. site preparation;
3. foundations;
4. structural works;
5. systems/fitting;
6. commissioning;
7. operational.

KCB-TIME-070 — Major projects MUST not complete instantly in standard play.

KCB-TIME-071 — Each stage can generate jobs, deliveries, noise, road occupancy and budget draw.

KCB-TIME-072 — Small cosmetic/public-realm changes MAY use abbreviated construction.

## Long-term simulation

KCB-TIME-080 — Citizen age and building age use absolute simulation dates, not “ticks since creation” only.

KCB-TIME-081 — Historical records SHOULD survive aggregation: opening date, major renovation, heritage status, notable events.

KCB-TIME-082 — Long idle periods at high speed MUST use aggregate-safe algorithms rather than iterating millions of empty micro-steps.

## Save/load equivalence

KCB-TIME-090 — Saving and immediately loading MUST not advance time.

KCB-TIME-091 — Scheduled-event ordering MUST survive serialization.

KCB-TIME-092 — Random-stream state or derivation parameters MUST survive save/load.

KCB-TIME-093 — A deterministic validation test MUST compare N simulated days uninterrupted versus save/reload at midpoint.

## Trend windows

UI metrics require multiple windows:
- current;
- last simulated hour/day;
- 7-day rolling;
- monthly;
- yearly;
- user-selected historical range.

KCB-TIME-100 — Smoothed trends MUST be visually distinguished from raw values.

KCB-TIME-101 — Alerts SHOULD use hysteresis and duration thresholds to avoid oscillating notifications.

## Acceptance criteria

- Paused city produces zero economic or demographic changes.
- Frame-rate changes do not materially change outcomes.
- Same seed/input script reproduces a reference run.
- Save/reload does not reorder construction completions.
- 100 simulated years do not overflow timestamps or monetary values.
- Very-fast mode degrades representation before it compromises mandatory simulation consistency.
