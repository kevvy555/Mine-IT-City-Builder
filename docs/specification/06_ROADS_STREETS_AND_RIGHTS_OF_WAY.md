# 06 — Roads, Streets and Rights of Way

## Network model

A right of way is a corridor that can carry multiple layers:
- pedestrian space;
- cycle/micromobility;
- general vehicle lanes;
- service/emergency priority;
- bus lanes;
- tram/light transit;
- maglev/guideway where appropriate;
- utility conduits;
- planting/drainage;
- freight/loading space.

KCB-ROAD-001 — Road geometry and road configuration MUST be separate data so a corridor can be upgraded without rebuilding its entire alignment.

## Construction tools

Required modes:
- straight;
- simple curve;
- compound curve/spline;
- tangent continuation;
- grid/parallel generation;
- upgrade/replace;
- intersection placement;
- bridge;
- tunnel;
- elevated;
- pedestrian/shared street;
- path/greenway;
- blueprint-only.

KCB-ROAD-010 — Every tool MUST provide live cost, slope, conflict, demolition and capacity preview.

KCB-ROAD-011 — Snapping options MUST be individually togglable:
- endpoint;
- midpoint;
- tangent;
- angle;
- parallel;
- grid;
- parcel/block;
- contour;
- existing alignment.

KCB-ROAD-012 — Undo MUST work reliably for construction operations before simulation advances through irreversible consequences.

## Geometry

KCB-ROAD-020 — Curves SHOULD be represented by splines or equivalent continuous geometry, not staircase approximations.

KCB-ROAD-021 — Lane/track/path centre-lines MUST derive from corridor geometry so widening or lane changes remain aligned.

KCB-ROAD-022 — Intersection generation MUST validate turning radii, conflict points, pedestrian crossings and transit tracks.

KCB-ROAD-023 — Extreme gradients/curvature MUST be rejected or require specialised infrastructure.

## Hierarchy

Street classes are functional templates, not rigid rules:
- service lane;
- local street;
- collector;
- urban boulevard;
- arterial;
- limited-access corridor;
- pedestrian street;
- transit mall;
- greenway;
- freight/service corridor.

Each template defines defaults for:
- design speed;
- width;
- lane count;
- intersection spacing;
- allowed modes;
- frontage access;
- parking/loading;
- planting;
- noise treatment;
- maintenance cost.

KCB-ROAD-030 — Player customisation MAY deviate from templates within engineering constraints.

## Intersections

Control types:
- uncontrolled/priority;
- stop/yield equivalent;
- signalised;
- roundabout;
- grade-separated;
- transit-priority;
- pedestrian-priority.

KCB-ROAD-040 — Signalised intersections MUST support phase plans generated automatically from movements.

KCB-ROAD-041 — Advanced controls SHOULD allow priority, protected turns, pedestrian phase and transit pre-emption without requiring manual signal timing for ordinary play.

KCB-ROAD-042 — Junction performance MUST be measurable as delay, queue, throughput and safety risk.

## Parking and curb space

Even an advanced city needs curb/logistics allocation, though private-car dependence may be lower than present-day cities.

Curb uses:
- passenger pickup;
- deliveries;
- service access;
- accessible loading;
- short stay;
- micromobility;
- trees/drainage;
- transit stops.

KCB-ROAD-050 — Curb space is finite.
KCB-ROAD-051 — Conflicting curb demand can cause double-stopping, delivery delay or pedestrian impact.
KCB-ROAD-052 — District policy may reallocate curb use.

## Maintenance and condition

Road condition degrades with:
- age;
- axle load;
- climate;
- construction disturbance;
- utility works.

KCB-ROAD-060 — Poor condition can reduce speed/capacity and raise maintenance/vehicle costs.

KCB-ROAD-061 — Maintenance SHOULD be schedulable and create temporary lane closures.

KCB-ROAD-062 — Preventive maintenance is cheaper than severe reconstruction over long periods.

## Utilities in corridors

KCB-ROAD-070 — Common utility conduits MAY run under/along rights of way, but their capacity/connectivity is independent from the fact a road exists.

KCB-ROAD-071 — Utility upgrades should be possible without always demolishing the surface corridor.

## Safety

Road safety factors:
- speed;
- conflict points;
- crossing distance;
- lighting;
- maintenance;
- traffic mix;
- visibility;
- weather.

KCB-ROAD-080 — Accidents SHOULD be rare systemic events influenced by design, not purely random punishment.

KCB-ROAD-081 — Emergency incidents feed back into congestion and service response.

## Visual quality

KCB-ROAD-090 — Road surfaces, markings, barriers, planting and street furniture MUST use Koplin-specific art, not modern Earth asset packs unchanged.

KCB-ROAD-091 — Repeated props SHOULD use instancing and deterministic variation.

KCB-ROAD-092 — At distance, corridor ribbons and major transit movement remain legible even when individual lane detail is culled.

## Acceptance criteria

- Replace a four-lane boulevard with a transit-priority configuration while preserving alignment.
- Create a smooth curved boulevard around an existing park.
- Show exact buildings/trees/utilities affected before confirming widening.
- A damaged arterial measurably increases travel time.
- Transit priority changes intersection delay.
- Parcel frontage updates after a new street is created.
