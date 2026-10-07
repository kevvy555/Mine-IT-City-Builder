# 30 — Accessibility, Localisation and Settings

## Accessibility principle

A city builder communicates through dense text, maps, colour overlays, timing and precision input. Accessibility must therefore be designed into UI and tools rather than bolted on after content is finished.

## Text

KCB-ACC-001 — UI text size is scalable.
KCB-ACC-002 — Layout reflows rather than clipping at supported scale factors.
KCB-ACC-003 — Critical text is not baked into background images.
KCB-ACC-004 — Font choice prioritises legibility.

Microsoft accessibility guidance reviewed for this spec recommends strong contrast for meaningful text/UI, including 4.5:1 for standard-size important elements and 3:1 for large elements, with stronger high-contrast modes.

KCB-ACC-005 — Default UI SHOULD meet at least 4.5:1 for standard important text/controls where practical.
KCB-ACC-006 — High-contrast mode targets 7:1 for important UI elements where practical.

## Colour

KCB-ACC-010 — No critical overlay uses red/green or hue alone.
KCB-ACC-011 — Colour-vision presets and custom palette options SHOULD be supported.
KCB-ACC-012 — Icons/patterns/line styles reinforce colour categories.

## Narration

KCB-ACC-020 — Important menus, values, alerts and controls have accessible names/roles/state metadata.
KCB-ACC-021 — Key map/heatmap information has a textual summary.
KCB-ACC-022 — Real-time alerts can be narrated when narration is enabled.
KCB-ACC-023 — Decorative imagery is not over-described.

## Input

KCB-ACC-030 — Full core play is possible without requiring rapid repeated input.
KCB-ACC-031 — Keybindings are remappable.
KCB-ACC-032 — Hold/toggle alternatives exist for continuous actions where relevant.
KCB-ACC-033 — Pointer sensitivity and camera speed are configurable.
KCB-ACC-034 — Touch targets/controller focus targets are large enough for reliable use.

## Precision assistance

Options:
- stronger snapping;
- slower camera modifier;
- grid lock;
- angle lock;
- larger handles;
- confirmation for destructive actions.

KCB-ACC-040 — Precision assists do not alter simulation difficulty unless explicitly labelled.

## Motion

KCB-ACC-050 — Motion blur can be disabled.
KCB-ACC-051 — Camera shake can be disabled.
KCB-ACC-052 — flashing/strobing is avoided; emergency lights need safe alternatives.
KCB-ACC-053 — UI animation can be reduced.

## Audio

KCB-ACC-060 — Meaningful spoken content has subtitles/captions.
KCB-ACC-061 — Gameplay cues have non-audio equivalents.
KCB-ACC-062 — Separate volume controls exist for major categories.
KCB-ACC-063 — Critical alerts can use visual/haptic equivalents on supported devices.

## Cognitive load

KCB-ACC-070 — Tutorials are replayable and pausable.
KCB-ACC-071 — Alerts aggregate and can be filtered.
KCB-ACC-072 — Tooltips define jargon.
KCB-ACC-073 — Recommended/default automation allows players to avoid repetitive micromanagement.

## Difficulty accessibility

KCB-ACC-080 — Pausing does not carry a gameplay penalty in standard modes.
KCB-ACC-081 — Time pressure is rare and can be paused unless a challenge explicitly tests real-time response.
KCB-ACC-082 — Assistance settings are independent from economic difficulty where possible.

## Localisation architecture

All user-facing strings:
- use keys;
- support plural rules;
- support variable substitution;
- avoid concatenated sentence fragments;
- separate lore from tuning text.

KCB-LOC-001 — No essential user-facing string is hardcoded in simulation logic.
KCB-LOC-002 — Units, numbers, currency and dates format through locale-aware helpers.
KCB-LOC-003 — UI allows text expansion.
KCB-LOC-004 — Right-to-left layout support is an implementation-plan decision but data/components must not make it impossible.

## Names

Citizen/place/building names may use generated pools.

KCB-LOC-010 — Name generation should respect Koplin cultural canon without using caricatured modern-Earth ethnic stereotypes.
KCB-LOC-011 — Player-created names support Unicode.
KCB-LOC-012 — Search handles locale-appropriate case/normalisation.

## Units

Display options may include metric-like Commonwealth standard with familiar player units as accessibility/usability choice.

KCB-LOC-020 — Internal simulation units are fixed and independent from display-unit preference.

## Settings persistence

Categories:
- gameplay;
- difficulty;
- accessibility;
- graphics;
- audio;
- controls;
- UI;
- camera;
- autosave;
- privacy/telemetry if used.

KCB-ACC-090 — Accessibility settings are reachable before starting a city.
KCB-ACC-091 — Settings can reset by category.
KCB-ACC-092 — Graphics presets expose advanced overrides without silently resetting them unless user chooses preset reset.

## Acceptance criteria

- Critical traffic overlay is interpretable in monochrome/high contrast.
- 150–200% UI scaling does not hide confirm/cancel actions on supported layouts.
- Main menus are navigable with keyboard/controller focus.
- Motion blur/shake can be fully disabled.
- Locale change does not alter simulation data.
- Narration can announce a critical utility alert and its affected area summary.
