# Phase 3 — Android UI Device Acceptance

**Status:** PASS — physical Android rendered-UI re-test accepted  
**Branch:** `feature/phase-3-universe-canonical-world`  
**Accepted executable SHA:** `cad0d1c75af70680189cdae1caf85b83dd6feb3f`  
**Acceptance date:** 2026-10-09

## Why this gate exists

The first Phase 3 Android candidate successfully booted and rendered the 3D bootstrap city, but the top-left UI Toolkit card was blank on a physical Android device.

The defect exposed a gap in the previous automated acceptance approach: tests verified data, UI hierarchy construction and APK packaging, but did not prove that text/fonts and themed controls rendered in the Android player.

Root cause: `BootstrapUi` created `PanelSettings` at runtime without assigning a packaged runtime `ThemeStyleSheet`. Unthemed plain `VisualElement` backgrounds could render while labels/buttons had no usable runtime font/theme.

## Fix

- package `bootstrap-runtime-theme.tss` in Resources;
- inherit Unity's default runtime theme via `unity-theme://default`;
- assign the theme explicitly to runtime-created `PanelSettings`;
- fail startup loudly if the packaged theme is missing;
- add EditMode coverage that verifies the runtime theme, title, labels, button and canonical status hierarchy.

## Physical-device acceptance

Before Phase 3 is merged, a tester must confirm on the replacement Android APK:

- top-left card title is readable;
- build/version line is readable;
- canon line shows Koplin 3 / Concordia / Federal Forum (0,0);
- atlas/chunk/provenance lines are readable;
- ECS/render/FPS/lifecycle lines are readable;
- RESET CITY VIEW button is visible;
- pan, pinch zoom and two-finger rotate still work;
- no development-console exception overlay appears.

A successful CI build alone does not close this gate.


## Accepted physical-device evidence

The replacement APK was installed and tested on a physical Android device after the runtime-theme fix.

Observed PASS:

- title `MINEIT // CONCORDIA BOOTSTRAP` visibly renders;
- build/version row visibly renders;
- canonical row visibly shows `Koplin 3 / Concordia / Federal Forum (0,0)`;
- atlas row shows 100 tiles and 16 origin chunks;
- Universe/canon hash row visibly renders;
- ECS/Burst/PVG metrics visibly render;
- lifecycle/FPS/control guidance visibly renders;
- `RESET CITY VIEW` button visibly renders;
- city scene remains visible behind the UI;
- tester reported the replacement test as good.

The screenshot supplied during acceptance is the human-rendered proof that the blank-panel defect is fixed.

## Follow-up build-pipeline acceptance

Stable development signing and build-cache hardening were added after this visual acceptance. Those changes do not alter the UI layout, but the first stable-signed APK still requires an install/update smoke test before Phase 3 integration closes.
