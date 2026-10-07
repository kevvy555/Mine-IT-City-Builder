# 06 — Entity Data Model and Stable IDs

## Principle

Unity ECS `Entity` values are runtime handles, not persistent identity.

Every entity that can survive save/load, be referenced by history, or be targeted by another persistent object has a stable ID.

## Stable ID classes

### Canonical IDs

Imported verbatim from MineIT-Universe, for example:

- `planet-koplin-prime`;
- `settlement-concordia`;
- atlas tile IDs;
- organisation/species/material IDs.

### Game-authored content IDs

Namespaced lower-case IDs:

- `building.residential.garden-midrise`;
- `road.local.standard`;
- `policy.transit.fare-integration`;
- `visual.building.garden-midrise.pvg`.

### Save-created IDs

Use deterministic 128-bit logical IDs or a collision-safe compound:

```text
SaveEntityId = (worldInstanceId, entityType, monotonicSequence)
```

The allocator state is persisted.

Do not use random GUID creation in hot loops if monotonic deterministic IDs satisfy the domain.

## Runtime registry

At load:

1. load stable records;
2. allocate ECS entities/components;
3. build StableId -> Entity lookup;
4. resolve references in a validation phase;
5. fail/repair according to migration policy.

The registry itself is derived and not authoritative save data.

## High-count entity representations

### Citizen

Compact components may include:

- StableCitizenId;
- BirthDate;
- HouseholdId;
- HomeBuildingId;
- CurrentActivity;
- Education/qualification compact code;
- EmploymentId;
- Health/wellbeing compact state;
- Mobility profile;
- lifecycle flags.

Do not attach managed strings or individual names as managed objects to every citizen. Names resolve through compact name IDs/pools.

### Household

- stable ID;
- member span/reference;
- dwelling;
- income/budget summary;
- tenure;
- preferences;
- move pressure;
- vehicles/mobility access where relevant.

### Building

- stable ID;
- archetype ID;
- parcel ID;
- lifecycle state;
- floor/use allocation;
- capacity/occupancy;
- utility connections;
- condition;
- construction/history references;
- visual archetype ID.

### Business/facility

Use separate domain record/entity from physical building where one building may contain multiple occupants/uses.

### Trip

Most trips are ephemeral and need not have permanent save identity. Trips crossing a save boundary require serializable logical state:

- traveller/freight owner;
- purpose;
- origin/destination;
- route token;
- progress;
- scheduled departure/arrival.

### Network elements

Road segment, junction, transit stop, utility node and external connector use stable network IDs because they are referenced by saves/routes/history.

## Static content data

Immutable archetype data is baked into BlobAssets or equivalent compact read-only stores:

- building definitions;
- road templates;
- service definitions;
- production recipes;
- primitive visual grammar;
- policy definitions.

ECS entities reference compact runtime content handles resolved from stable IDs.

## Component size discipline

High-count components are designed to:

- remain blittable/Burst compatible;
- avoid references to managed objects;
- minimise false sharing;
- split hot/cold data;
- avoid storing duplicate derivable values;
- use compact enums/bitsets where safe.

Performance tests report bytes per citizen/household/building/trip.

## Archetype/chunk churn

Avoid frequently adding/removing components for rapidly changing state if it creates excessive structural changes.

Prefer:

- flags;
- enableable components;
- state enums;
- buffered state transitions applied in batches.

Structural changes are grouped through EntityCommandBuffer.

## Cross-domain references

Persistent relationships use typed stable IDs in save state. During a running session, systems may maintain derived fast Entity handles, graph indices or array offsets, but those must be rebuildable.

## Tombstones/history

If a persistent entity with historical references is deleted:

- authoritative active entity is removed;
- minimal historical record/tombstone may preserve ID, type, dates and display-name snapshot;
- stale references resolve to archived history, not a new entity that reused the ID.

IDs are never reused inside a save.

## Data conservation invariants

- each citizen belongs to at most one household;
- occupied dwelling count reconciles with households;
- employed person maps to valid job slot;
- building occupancy cannot exceed configured capacity without explicit over-capacity state;
- inventory cannot be negative;
- active trip owner must exist or have defined orphan recovery;
- persistent reference must resolve or carry explicit missing/deprecated state.

## Tasks

- IMP-ID-001 — Define typed ID structs.
- IMP-ID-002 — Define save entity allocator.
- IMP-ID-003 — Build stable-ID runtime registry.
- IMP-ID-004 — Build canonical/game/save namespaces.
- IMP-ID-005 — Add collision validation.
- IMP-ID-006 — Define citizen component layout.
- IMP-ID-007 — Define household component layout.
- IMP-ID-008 — Define building/business/facility separation.
- IMP-ID-009 — Define network element identity.
- IMP-ID-010 — Add tombstone/history record.
- IMP-ID-011 — Add component-size benchmark.
- IMP-ID-012 — Add persistent-reference validation.

## Exit criteria

- save/load never persists raw ECS Entity values as identity;
- display-name changes do not alter identity;
- deleted IDs are not reused;
- 250k synthetic citizen data fits within agreed memory budget;
- reference validator catches broken household/job/building relations.
