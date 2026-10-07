# 12 — Blocks, Parcels, Zoning and Development

## Geometry foundation

Roads, paths, water and immutable boundaries define blocks.

Use a robust deterministic 2D polygon pipeline based on integer coordinates. The preferred library is **Clipper2** for polygon Boolean/offset operations, embedded or package-pinned after Android/IL2CPP licence and determinism proof.

Render meshes are derived from topology.

## Block generation

For a dirty region:

1. gather boundary segments;
2. build planar graph;
3. detect closed faces;
4. remove invalid/degenerate faces;
5. classify road/water/public-space boundaries;
6. compare with existing blocks;
7. preserve stable IDs where materially same;
8. mark parcel regeneration candidates.

## Stable geometry identity

Geometry changes should not churn identity unnecessarily.

Match old/new block/parcel using:

- overlap ratio;
- centroid distance;
- frontage continuity;
- parent block identity.

Thresholds are explicit and tested.

If materially changed, create new identity and archive old history relationship.

## Parcel generation

Inputs:

- block polygon;
- zoning;
- protected features;
- desired frontage/depth;
- access constraints;
- service alleys;
- existing parcels/buildings.

Algorithm:

1. identify valid frontage;
2. derive candidate split lines;
3. recursively split while minimum area/depth/frontage rules pass;
4. merge slivers;
5. preserve easements;
6. reserve residual public/service land;
7. score result;
8. pick deterministic best solution.

Random variation derives from block/parcel stable seed and grammar version.

## Parcel technology proof

Before production, benchmark:

- convex blocks;
- concave blocks;
- curved-road approximations;
- acute corners;
- narrow blocks;
- blocks with water holes;
- parcels around protected landmarks.

Fuzz 10,000 random legal block shapes and assert no negative/overlapping parcel area beyond tolerance.

## Zoning

Zoning is permissions/envelope, not an instant building type.

Zone definition includes:

- allowed uses;
- max/min height;
- frontage/coverage;
- density;
- setbacks;
- noise/environment limits;
- loading/access;
- heritage rules;
- mixed-use allowances.

A parcel may have zoning plus overlays/policy constraints.

## Development demand

Development candidate score consumes:

- use-specific demand;
- land value/affordability;
- access/travel;
- utilities;
- service availability;
- construction capacity/cost;
- parcel suitability;
- policy;
- nuisance/exposure;
- vacancy;
- expected profit/mission need.

The player creates conditions; ordinary private/cooperative development is not manually placed building-by-building unless mode/rule allows it.

## Building generation

Building simulation definition is separate from shape.

Inputs:

- parcel;
- use programme;
- allowed envelope;
- capacity target;
- archetype family;
- district grammar;
- seed.

Output:

- footprint;
- floor count;
- use allocation;
- entrances/loading points;
- roof/service slots;
- visual archetype;
- capacities/jobs/dwellings;
- construction requirements.

Initial shape uses PVG massing.

## Mixed use

Represent floor/use allocation explicitly enough that one building can contain:

- retail ground floor;
- offices;
- housing;
- service/education component if archetype allows.

Capacity calculations derive from floor-area allocation rather than a single building label.

## Development lifecycle

```text
vacant
→ candidate
→ permit/approved
→ construction
→ commissioning
→ active
→ renovation/redevelopment
→ vacancy/decline
→ demolition/archived
```

Each transition records date/history where relevant.

## Redevelopment

Existing building may be:

- renovated;
- extended;
- repurposed;
- replaced.

Decision considers expected value, condition, heritage, vacancy, zoning and market demand.

No instant deletion/replacement without construction lifecycle.

## Construction market

Projects consume:

- construction labour;
- materials/logistics;
- finance/budget;
- time.

City-wide construction capacity limits concurrent throughput.

## Tasks

- IMP-DEV-001 — Integrate/prove Clipper2.
- IMP-DEV-002 — Implement block planar face extraction.
- IMP-DEV-003 — Implement stable block matching.
- IMP-DEV-004 — Implement parcel splitter.
- IMP-DEV-005 — Implement sliver merge/access validation.
- IMP-DEV-006 — Implement stable parcel matching.
- IMP-DEV-007 — Define zone content schema.
- IMP-DEV-008 — Implement zone painting/editing.
- IMP-DEV-009 — Implement development suitability factors.
- IMP-DEV-010 — Implement demand candidate queue.
- IMP-DEV-011 — Implement building programme/envelope solver.
- IMP-DEV-012 — Generate PVG building massing.
- IMP-DEV-013 — Implement construction lifecycle.
- IMP-DEV-014 — Implement vacancy/redevelopment.
- IMP-DEV-015 — Implement construction-capacity limiter.
- IMP-DEV-016 — Add geometry fuzz/property tests.

## Exit criteria

- irregular curved-road block produces legal parcels;
- disconnected parcel cannot develop;
- minor road edit preserves unaffected parcel IDs;
- mixed-use building has traceable floor/use capacity;
- development reason can be explained through input factors;
- 10,000-shape fuzz suite has no overlap/infinite-loop failures.
