# 23 — Camera, Controls, Input and Construction Tools

## Input philosophy

The spec is engine/platform neutral. Controls must support mouse/keyboard and be designed so controller/touch mappings can be implemented without redesigning core tools.

KCB-INP-001 — All important actions MUST have an input-agnostic command representation behind the UI.

## Camera

Required camera modes:
- standard city camera;
- free camera;
- follow selected agent/vehicle optional;
- photo/cinematic.

Standard camera supports:
- pan;
- rotate;
- zoom;
- tilt within limits;
- focus selection;
- jump to alert/search result;
- reset north/orientation;
- saved viewpoints/bookmarks.

KCB-INP-010 — Camera motion must be frame-rate independent.
KCB-INP-011 — Zoom speed scales sensibly with altitude.
KCB-INP-012 — Camera collision avoids going below terrain/buildings unless free/photo mode allows it.
KCB-INP-013 — Edge scrolling is optional and disableable.

## Camera scale transitions

KCB-INP-020 — Labels/icons use semantic zoom:
- close: object labels on demand;
- district: key buildings/routes;
- metro: districts, external links, major alerts.

KCB-INP-021 — UI density must not explode when zooming out.

## Selection

Selection priority:
1. UI handle/tool preview;
2. explicit selected network segment/stop;
3. building;
4. vehicle/agent;
5. parcel/ground.

KCB-INP-030 — Overlapping targets show a cycling/context chooser rather than making selection impossible.
KCB-INP-031 — Selection outline works across normal and overlay modes.

## Construction interaction pattern

Every build tool follows:
1. choose tool;
2. choose variant/template;
3. point/draw;
4. preview geometry;
5. show cost/conflicts/consequences;
6. confirm;
7. optionally continue;
8. cancel safely.

KCB-INP-040 — No destructive operation occurs merely by hovering.
KCB-INP-041 — Right-click/Escape/back action cancels current step consistently.
KCB-INP-042 — Multi-stage tools show current step and remaining steps.

## Blueprint mode

KCB-INP-050 — Player can draw unfunded/uncommitted infrastructure/building plans.
KCB-INP-051 — Blueprint geometry participates in conflict checking but not normal operation.
KCB-INP-052 — Blueprint can be funded wholly or in stages.
KCB-INP-053 — Blueprint can be locked against accidental deletion/edit.

## Undo/redo

KCB-INP-060 — Planning/construction edits support command-based undo/redo where simulation consequences have not made reversal unsafe.
KCB-INP-061 — If undo is no longer safe, the UI explains and offers demolition/revert project if possible.

## Network editing

Tools:
- move node;
- adjust curve;
- insert/remove node;
- upgrade segment;
- change lane allocation;
- add transit track;
- add/remove stop;
- change intersection control;
- split/merge segment.

KCB-INP-070 — Editing preserves connected systems where possible rather than demolishing the entire corridor.

## Area tools

Brushes/polygons:
- zone;
- district;
- environment protection;
- tree/landscape;
- demolition;
- terrain;
- policy area.

KCB-INP-080 — Brush size and falloff are adjustable.
KCB-INP-081 — Area tools show affected object count before destructive confirmation.

## Precision

Advanced toggles:
- coordinates/grid;
- angle constraint;
- elevation;
- curve radius;
- parallel offset;
- slope;
- measurement;
- align/distribute.

KCB-INP-090 — Precision tools are optional, not required for ordinary building.

## Touch/controller design requirements

KCB-INP-100 — Tool controls cannot depend solely on hover.
KCB-INP-101 — Small handles need enlarged invisible hit targets.
KCB-INP-102 — Pinch/gesture conflicts with construction drawing must be explicitly resolved.
KCB-INP-103 — Controller uses focus/confirm/cancel conventions and radial/category menus only where they improve navigation.

## Shortcuts

Desktop should support configurable shortcuts for:
- pause/speeds;
- bulldoze;
- road;
- zoning;
- transit;
- utilities;
- overlays;
- search;
- rotate;
- screenshot/photo;
- undo/redo.

KCB-INP-110 — Shortcut conflicts are detected in settings.

## Search

Global search:
- district;
- building type;
- named building;
- citizen/featured person optional;
- road;
- station/line;
- policy;
- metric;
- alert.

KCB-INP-120 — Search result can focus camera and open relevant panel.

## Photo/cinematic

Features:
- free camera;
- field of view;
- focal distance/depth of field;
- exposure;
- time-of-day override for capture without changing simulation optional;
- hide UI;
- camera path/keyframes optional;
- resolution multiplier where platform permits.

KCB-INP-130 — Photo-only visual overrides do not mutate simulation state.

## Acceptance criteria

- Road can be drawn, previewed, cancelled and redrawn without side effects.
- A complex overlap can still select the intended object.
- Blueprint survives save/load.
- Controller/touch implementation can invoke all core commands without hover.
- Camera zoom keeps focus point stable enough for precise construction.
