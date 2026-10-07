# 10 — Mobile UX, Accessibility and Explainability QA

## Purpose

Prove the game can actually be controlled, understood and diagnosed on a phone.

## Core task-based UX tests

### QA-UX-001 — Navigate and inspect

Task:
- find named area/building;
- zoom;
- select;
- inspect key state.

Pass:
- no hover;
- no keyboard;
- no tiny precision control required.

### QA-UX-002 — Build a road

Task:
- choose road;
- draw curve;
- correct geometry;
- understand cost/invalid state;
- confirm/cancel.

Pass:
- touch-only;
- handles reachable;
- undo/cancel obvious.

### QA-UX-003 — Diagnose underperforming building

Give player an intentionally failing building.

Pass if within three interactions from building panel they can identify:
- primary cause;
- location/upstream cause where relevant;
- at least one relevant action or trace.

### QA-UX-004 — Diagnose power failure

Expected user path:
building -> power factor -> network trace -> bottleneck/source.

### QA-UX-005 — Diagnose congestion

Player must distinguish:
- high volume but flowing;
- low speed/queue;
- source/destination/routes.

### QA-UX-006 — Budget understanding

Player must correctly answer:
- operating result;
- cash;
- committed capital;
- free/available capital.

## Explainability consistency

QA-UX-010 — UI top causes are emitted by authoritative calculation.

QA-UX-011 — UI never claims a cause that is not currently contributing.

QA-UX-012 — If limiting factor changes, UI updates within defined read-model cadence.

QA-UX-013 — causal trace terminates at actionable/external source or clearly states no player action.

## Touch target/accessibility

QA-UX-020 — critical interactive targets approximately 44–48dp minimum.

QA-UX-021 — destructive action has clear confirmation/undo where specified.

QA-UX-022 — drag tools have precision/non-drag assistance.

## UI scaling

Test 100%, 150%, 200%.

QA-UX-030 — confirm/cancel and primary controls remain reachable.

QA-UX-031 — text reflows rather than critical clipping.

## Colour/contrast

QA-UX-040 — critical text/control contrast meets target where specified.

QA-UX-041 — no critical overlay depends on hue alone.

QA-UX-042 — monochrome/high-contrast interpretation remains possible.

## TalkBack/narration

Test:
- main menu;
- city HUD;
- context panel;
- alert;
- settings;
- charts/overlay textual summary.

QA-UX-050 — control names/roles/states meaningful.

QA-UX-051 — focus order logical.

QA-UX-052 — map/overlay has useful nonvisual summary.

## Motion/audio/haptics

QA-UX-060 — motion blur/shake/reduced motion settings work.

QA-UX-061 — critical audio cue has visual equivalent.

QA-UX-062 — haptics can be disabled.

QA-UX-063 — emergency visual does not require unsafe flashing.

## Localisation

Pseudo-localisation tests:
- +30–50% text expansion;
- Unicode;
- plural forms;
- long values.

QA-UX-070 — no essential simulation logic contains user-facing hardcoded English.

QA-UX-071 — locale formatting does not alter authoritative numeric value.

## Phone/tablet matrix

At least:
- small supported phone;
- reference phone;
- large phone;
- tablet.

Check safe areas/cutouts/navigation modes.

## Human usability sessions

When vertical slice exists, give testers tasks without explaining mechanics.

Observe:
- success/failure;
- wrong assumptions;
- number of interactions;
- where they look;
- whether wording explains mechanism.

A technically correct UI that consistently causes incorrect player diagnosis fails QA and requires UX/spec revision.
