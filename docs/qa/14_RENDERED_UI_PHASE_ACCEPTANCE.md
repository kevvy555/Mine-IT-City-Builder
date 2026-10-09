# 14 — Rendered UI Phase Acceptance

## Purpose

Prevent presentation defects from escaping a phase simply because logic tests, hierarchy tests or Android packaging passed.

This document covers **rendered UI integrity**: whether the player can actually see, read and operate the UI on the production Android runtime.

It complements:

- `10_MOBILE_UX_ACCESSIBILITY_AND_EXPLAINABILITY.md` — usability, explainability and accessibility behaviour;
- `09_ANDROID_PERFORMANCE_AND_DEVICE_QA.md` — device/performance/lifecycle;
- implementation phase plans — what each phase changes.

A UI test that proves a `Label` object exists is not proof that its text is visible in the Android player.

## Core rule

**Every implementation phase, including backend-only phases, MUST contain an explicit UI test plan and UI acceptance result.**

The phase plan/evidence must state:

1. UI impact level;
2. screens/panels/overlays affected;
3. automated rendered-UI checks;
4. physical Android checks;
5. required screenshots/video where applicable;
6. result: PASS / FAIL / NOT APPLICABLE WITH REGRESSION PASS.

A phase cannot be closed with the UI field omitted.

## UI impact levels

### UI-0 — No intended presentation change

Examples:
- simulation kernel;
- persistence internals;
- data import with no visible output change.

Required:
- declare UI-0 explicitly;
- boot the production APK when the phase produces one;
- verify the existing diagnostic/game shell still renders;
- no unexpected blank, clipped or missing UI;
- retain at least one regression capture at milestone/gate boundaries.

UI-0 does **not** mean “skip UI testing.”

### UI-1 — Diagnostic/read-only presentation

Examples:
- build metadata;
- canon status;
- debug HUD;
- read-only inspection text.

Required:
- UI hierarchy/content test;
- theme/font/resource validation;
- rendered screenshot or physical-device inspection;
- safe-area/clipping check;
- empty/null/error state check.

### UI-2 — Interactive UI

Examples:
- buttons;
- selection panels;
- toolbars;
- construction confirmation;
- context menus.

Required:
- all UI-1 checks;
- interaction-state tests;
- touch target check;
- disabled/pressed/selected/error states;
- physical Android touch test.

### UI-3 — Gameplay-critical visualisation

Examples:
- overlays;
- map modes;
- route/utility diagnostics;
- alerts;
- placement previews;
- charts that drive player decisions.

Required:
- all UI-2 checks;
- state-to-visual correctness test;
- representative screenshots/captures;
- at least one failure/edge state;
- colour-independent signal check;
- physical Android acceptance.

### UI-4 — Production accessibility/localisation UI

Examples:
- final HUD/settings;
- TalkBack focus;
- scaling;
- localisation;
- release UI.

Required:
- all UI-3 checks;
- 100/150/200% scale;
- pseudo-localisation;
- accessibility semantics/focus;
- contrast/non-colour checks;
- supported-device matrix.

## Required automated layers

### QA-UI-001 — Phase UI declaration

Every phase evidence file states:
- impact level UI-0..UI-4;
- affected surfaces;
- automated checks;
- device checks.

Fail if missing.

### QA-UI-010 — Runtime theme/font resources

Any runtime UI framework dependency required to render text/controls must be packaged and validated.

For UI Toolkit this includes, where applicable:
- `PanelSettings`;
- runtime `ThemeStyleSheet`;
- font/text resources;
- required USS/UXML/resources.

Fail if a player build can construct the panel but render blank text/controls because a runtime resource is absent.

### QA-UI-011 — Non-empty visible content contract

Automated tests verify critical labels/buttons contain expected non-empty content and required state fields.

This is structural evidence only; it does not replace rendered-device evidence.

### QA-UI-012 — Error-state visibility

A required startup/import/runtime UI error must be visible to the player/developer rather than only emitted to logs.

### QA-UI-020 — Render capture smoke

For UI-1+ phases, produce at least one rendered capture from a representative scene/state when automation supports it.

Capture should prove:
- panel visible;
- text/control glyphs visible;
- expected hierarchy/layout;
- no full-panel blanking.

A capture test must not silently update its expected baseline after failure.

### QA-UI-021 — Layout bounds

Critical controls/text must remain within visible/safe-area bounds at the reference resolution and target device aspect ratios.

### QA-UI-022 — Overlap/clipping

Critical text and controls do not overlap, disappear behind other layers, or clip essential values.

### QA-UI-030 — Physical Android rendered smoke

For every UI-1+ phase that produces an APK:
- install/open on a physical Android device;
- verify required text/control rendering;
- verify no development-console exception overlay;
- capture screenshot(s).

This is mandatory until automated Android screenshot testing is proven equivalent.

### QA-UI-031 — Physical interaction smoke

For UI-2+:
- perform representative touch actions;
- confirm visible pressed/selected/result state;
- confirm no input/render mismatch.

### QA-UI-040 — State coverage

Every changed UI surface must test:
- normal;
- empty/no-data;
- loading where applicable;
- error/invalid where applicable;
- disabled/unavailable where applicable.

### QA-UI-050 — Regression shell

Every phase executes the current shell regression appropriate to its build:
- app boots;
- primary HUD/panel text visible;
- existing navigation/input still works;
- no new blank panel;
- no missing font/theme;
- no exception overlay.

## Physical-device evidence

A manual rendered-UI pass records:

```text
qaId:
phase:
buildSha:
apkVersion:
device:
androidVersion:
resolution:
uiImpactLevel:
screensChecked:
interactionsChecked:
result:
screenshots:
notes:
```

A statement such as “APK boots” is insufficient for UI acceptance.

## Phase-by-phase UI acceptance matrix

| Phase | UI level | Mandatory UI plan |
|---|---:|---|
| 0 Planning integration | UI-0 | Documentation-only; verify every future phase has an assigned UI level/plan. |
| 1 Unity + Android + CI proof | UI-2 | Diagnostic card, build/version text, reset button, touch camera; physical Android screenshot + touch proof. |
| 2 Core architecture/IDs/time/save | UI-0 | Existing Phase 1 shell regression on Android; no presentation change expected. |
| 3 Universe import/canonical world | UI-1 | Canon/build/provenance panel must visibly show Koplin 3 / Concordia / Federal Forum; physical Android screenshot required. |
| 4 PVG renderer + touch shell | UI-3 | Full rendered Federal Forum blockout, camera/touch, selection/context shell, HUD, safe area and visual capture. |
| 5 Roads/graph | UI-2 | Road tool, spline handles, valid/invalid preview, confirm/cancel, local rebuild feedback on touch device. |
| 6 Blocks/parcels/zoning/buildings | UI-3 | Block/parcel/zoning visual states, building preview/construction state, selection and invalid parcel feedback. |
| 7 Population/housing/jobs/education | UI-2 | Inspection surfaces for household/dwelling/job/education state plus empty/error states; shell regression. |
| 8 Mobility/traffic/transit | UI-3 | Route/trip/traffic/transit overlays and selection; congestion visual distinction; camera-independent outcomes. |
| 9 Power/service causality | UI-3 | Power/service overlays, limiting-factor trace, upstream cause path, failure/recovery states. |
| 10 Economy/housing/industry/freight | UI-3 | Budget/affordability/inventory/freight panels, large values, shortage/error states, traceability. |
| 11 UX/overlays/dashboard/save | UI-4 | Dashboard, charts, alerts, search, overlays, save UI; scale, pseudo-localisation and accessibility checks begin at production depth. |
| 12 Federal Forum vertical slice | UI-4 | End-to-end phone UX, canonical visual identity, all primary gameplay flows, device screenshots/video. |
| 13 Environment/resilience/services | UI-3 | Weather/pollution/flood/incident/service overlays, warnings and recovery states. |
| 14 Governance/policy/tech/events | UI-3 | Policy/mandate/research/event/news interfaces, unavailable/locked/decision states. |
| 15 Multi-tile + 50k | UI-3 | Tile transitions, district/context continuity, HLOD/proxy transitions without UI loss or stale selection. |
| 16 250k scale hardening | UI-3 | UI responsiveness under B250K, backlog/thermal reporting, no disappearing/lagging critical HUD. |
| 17 Accessibility/localisation/audio | UI-4 | Full UI scaling, TalkBack/focus, contrast, pseudo-localisation/locales, captions/settings. |
| 18 First 100 km² content | UI-4 | Representative visual QA captures across atlas, landmark/district consistency, clipping/selection regression. |
| 19 Long-run balance/production | UI-4 | Long-save UI integrity, large/edge numeric values, old-save/migration surfaces, accessibility regression. |
| 20 Android release hardening | UI-4 | Supported-device visual matrix, clean/upgrade install, release settings/store-facing UI, no debug-only presentation defects. |

## Phase completion rule

A phase can be marked complete only when:

- implementation tests pass;
- its assigned UI plan has executed;
- UI defects rated Blocker/Critical/Major for the phase are resolved or explicitly returned to implementation;
- required physical-device evidence exists;
- screenshots/video are linked where the UI level requires them.

If a physical test exposes a defect that automated tests missed, the fix must include a regression test where practical and the QA plan must be improved if the gap was systemic.

## Current Phase 3 lesson

The initial Phase 3 candidate passed:
- data/canon validation;
- Unity EditMode tests;
- Android build/package validation.

It still failed rendered UI acceptance because the runtime-created UI Toolkit panel lacked a packaged runtime theme. The card background rendered while labels/buttons did not.

This defect is the motivating example for separating:
- **hierarchy exists**;
- **content exists**;
- **player can actually see/use it on Android**.
