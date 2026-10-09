# Phase 3 — Android UI Device Acceptance

**Status:** REQUIRED — pending re-test after UI Toolkit runtime-theme fix  
**Branch:** `feature/phase-3-universe-canonical-world`

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
