# 20 — Accessibility, Localisation, Audio and Settings

## Accessibility target

Accessibility is part of v1 acceptance.

Primary Android touch must work without:

- rapid tapping;
- hover;
- tiny handles;
- colour-only information;
- audio-only alerts.

## Text and scaling

Support UI scale presets through at least 200% where physical screen permits.

Rules:

- reflow before clipping;
- minimum touch target approximately 44–48 dp;
- scroll within panels when necessary;
- critical confirm/cancel remains reachable;
- no essential text baked into textures.

## Contrast

Default important text/control target: 4.5:1 where practical.

High-contrast mode target: 7:1 where practical.

Test automated palette checks plus device screenshots.

## Colour

No critical red/green binary signal without icon/pattern/text.

Overlay palette service supplies:

- standard;
- common colour-vision presets;
- high contrast;
- monochrome-test mode.

## Motion

Settings:

- motion blur off/on, default off;
- camera shake strength/off;
- reduced UI motion;
- reduced animated overlay transitions;
- safe emergency flash style;
- optional follow-camera smoothing.

## Precision

Settings/tools:

- snapping strength;
- grid lock;
- angle lock;
- larger handles;
- slow camera mode;
- destructive confirmation.

These do not change economic difficulty.

## Narration/accessibility semantics

Every interactive control has:

- accessible label;
- role;
- state;
- value;
- focus order.

Prototype Android TalkBack compatibility early.

If UI Toolkit's runtime accessibility bridge is insufficient for required semantics, implement a focused native Android accessibility bridge rather than abandoning the requirement.

Maps/overlays provide textual summaries.

## Keyboard/controller

Touch is primary.

Keyboard/mouse should work in editor and Android accessory scenarios.

Controller support is implemented through the same Input System action abstraction; UI focus navigation must not depend on hover.

All remappable actions store binding overrides outside city save.

## Localisation

No user-facing string in simulation logic.

Use localisation keys with:

- pluralisation;
- variables;
- locale number/date/currency formatting;
- text expansion;
- Unicode player names.

English ships first unless content plan expands languages, but architecture supports later localisation.

Right-to-left full production may be deferred, but data/UI architecture must not make it impossible.

## Units

Internal simulation units fixed.

Presentation can choose unit formatting without changing simulation.

Commonwealth Credit remains canonical currency identity; numeric formatting is locale-aware.

## Audio architecture

AudioMixer groups:

- master;
- music;
- ambience;
- traffic/transit;
- city/service;
- UI;
- alerts;
- voice/news if added.

Simulation emits audio events; presentation decides whether/how many sounds to render.

## Audio LOD

Near:

- local traffic;
- service building;
- crowd;
- water/construction.

Mid:

- grouped ambience.

Far:

- district/city bed.

Do not spawn one AudioSource per simulated entity.

## Music

Adaptive state may react to:

- time of day;
- city condition;
- major event;
- camera district.

Avoid constant crisis music from minor alerts.

## Haptics

Optional channels:

- snap;
- confirm;
- warning;
- emergency.

Independent toggle/intensity.

## Settings persistence

Separate settings file/profile:

- gameplay;
- graphics;
- UI;
- accessibility;
- audio;
- controls;
- camera;
- autosave;
- privacy.

Settings are accessible before loading/creating a city.

## Tasks

- IMP-ACC-001 — Implement UI scaling service.
- IMP-ACC-002 — Implement theme/contrast tokens.
- IMP-ACC-003 — Implement colour/pattern overlay palettes.
- IMP-ACC-004 — Implement reduced-motion controls.
- IMP-ACC-005 — Implement precision-assistance settings.
- IMP-ACC-006 — Prototype TalkBack semantics.
- IMP-ACC-007 — Implement input rebinding persistence.
- IMP-ACC-008 — Implement localisation key pipeline.
- IMP-ACC-009 — Implement locale format helpers.
- IMP-ACC-010 — Implement AudioMixer architecture.
- IMP-ACC-011 — Implement audio LOD/event limiter.
- IMP-ACC-012 — Implement haptic settings.
- IMP-ACC-013 — Implement settings profile/schema.
- IMP-ACC-014 — Automated missing-localisation validation.
- IMP-ACC-015 — 200% UI scale phone/tablet QA.

## Exit criteria

- vertical slice can be played touch-only;
- main navigation has accessibility semantics;
- critical overlay works without colour;
- 200% scale retains critical actions;
- motion/flash/haptics can be reduced/disabled;
- audio alert has visual equivalent;
- locale switch does not alter simulation checksum.
