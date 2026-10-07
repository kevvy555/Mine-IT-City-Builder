# 10 — Camera, Input and Mobile UI Shell

## Mobile-first rule

Every essential workflow must be comfortable on a phone touchscreen without hover or precision mouse input.

Landscape is the primary orientation.

## Camera

Camera modes:

- city free camera;
- selected-entity follow;
- overview/metro zoom;
- construction focus;
- photo/cinematic mode.

Core state:

- focus world point;
- yaw;
- pitch;
- zoom/height;
- movement velocity;
- bounds/constraints.

Camera state is user preference/session metadata, not authoritative city simulation.

## Gestures

Default proposal:

- tap — select;
- tap selected item — open/expand context panel;
- one-finger drag on empty world — pan;
- pinch — zoom;
- two-finger rotation — rotate;
- two-finger vertical gesture or optional UI control — tilt;
- long press — context action/inspect;
- double tap — focus selected/point;
- construction drag — geometry placement with handles;
- two-finger gesture while constructing always prioritises camera.

Gesture conflict rules are explicit and testable.

## Construction alternatives

For accessibility and accuracy, every drag-heavy tool provides:

- numeric/stepper adjustment where relevant;
- snap toggles;
- undo;
- cancel;
- confirm;
- move-handle mode;
- angle lock/grid lock.

## Safe areas

UI respects:

- display cutouts;
- gesture navigation;
- rounded corners;
- tablet variations.

Safe-area service publishes layout padding to UI Toolkit.

## UI shell

Permanent/primary controls:

- pause/speed;
- build/tools;
- overlays/info views;
- city summary;
- alerts;
- search;
- settings;
- current money/date;
- selected-context panel.

Avoid covering most of map on phone. Panels use compact, expandable sheets.

## Context panel

One reusable shell displays:

- identity;
- status;
- key metrics;
- cause factors;
- actions;
- history/details;
- route/network trace where applicable.

Domain supplies typed view model rather than custom ad-hoc panel implementation for each entity.

## Selection

Selection layers:

- terrain;
- road/network;
- parcel;
- building;
- citizen/vehicle;
- district;
- utility/transit element.

Tap resolver ranks candidates by screen relevance/tool state and supports cycling when ambiguous.

Selection uses cheap spatial/render picking rather than physics colliders on every distant entity.

## Build mode

States:

1. choose tool/template;
2. preview;
3. edit geometry;
4. validate;
5. show cost/impacts;
6. commit or cancel;
7. command applies at deterministic boundary.

Invalid placement explains why.

## Blueprint mode

Planning can exist without immediate construction.

Blueprint geometry:

- has stable proposed ID;
- does not consume full operational capacity;
- can reserve space;
- can be edited/cancelled;
- converts to construction command when funded/approved.

## Undo/redo

Construction/editor commands store reversible operations where safe.

Undo is not “rewind simulation.” Once a project has entered irreversible simulated history, later removal is a new action rather than arbitrary history deletion.

## Haptics

Use optional Android haptics for:

- snap;
- valid commit;
- invalid action;
- alert severity.

Every haptic cue has visual equivalent and can be disabled.

## Touch performance

No UI action performs a whole-city scan synchronously.

Search/queries are indexed and cancellable.

Expensive overlays build incrementally/off-thread and show current timestamp.

## Tasks

- IMP-INP-001 — Configure Input System actions.
- IMP-INP-002 — Implement camera rig.
- IMP-INP-003 — Implement safe-area service.
- IMP-INP-004 — Implement gesture recogniser/conflict rules.
- IMP-INP-005 — Implement selection resolver.
- IMP-INP-006 — Implement context panel shell.
- IMP-INP-007 — Implement build tool state machine.
- IMP-INP-008 — Implement preview/validation messaging.
- IMP-INP-009 — Implement blueprint state.
- IMP-INP-010 — Implement undo/redo command stack.
- IMP-INP-011 — Add haptic abstraction.
- IMP-INP-012 — Test 150–200% UI scaling.
- IMP-INP-013 — Test touch-only complete vertical-slice workflow.

## Exit criteria

On a supported Android phone, the user can:

- navigate city;
- select building/road/parcel;
- open/close panels;
- draw/edit/confirm/cancel a road;
- zone an area;
- toggle overlays;
- pause/change speed;
- save;
- do all of the above without hover or physical keyboard.
