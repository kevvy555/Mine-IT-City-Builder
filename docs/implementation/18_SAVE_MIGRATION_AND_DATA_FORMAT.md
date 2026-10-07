# 18 — Save, Migration and Data Format

## Save contract

Save format is a versioned product API from the first playable build.

Do not use Unity scene serialization, ECS world dumps or raw memory snapshots as the durable player save format.

## Container

Proposed extension: `.micity`.

Header:

```text
magic                 MICB
containerVersion      uint32
saveSchemaVersion     uint32
gameVersion           semantic/string table ref
contentVersion        hash/version
universeCommit        40-char SHA / binary digest
rootSeed              uint64
scenarioId            stable ID
simulationMinute      int64
createdUtc            diagnostics only
lastSavedUtc          diagnostics only
featureFlags          bitset/table
sectionCount          uint32
headerChecksum
```

Following header: section table with type, schema version, offset, compressed length, uncompressed length, checksum and codec.

## Section families

Global:

- world metadata;
- scenario/governance;
- ID allocator;
- scheduler/event queue;
- global economy/external market;
- content/mod compatibility;
- history index.

Spatial chunk sections:

- mutable terrain/world state;
- roads/network topology owned locally;
- parcels/buildings;
- local environment fields;
- local render-derived references only where non-regenerable.

Domain sections:

- citizens/households;
- organisations/businesses/jobs;
- trips/transit;
- utilities/services;
- economy ledgers;
- events/history.

User settings live outside city save where possible.

## Serialization technology

Use project-owned, reflection-free binary codecs.

Rules:

- codecs explicitly read/write primitives;
- integer endianness fixed and documented;
- variable-length arrays validate count before allocation;
- stable IDs encoded through string table/typed binary form;
- each section owns its schema version;
- unknown optional sections can be skipped;
- required unknown sections produce compatibility screen.

This is more work than generic serializer use but avoids AOT/reflection surprises and gives long-term migration control.

## Compression

Container supports per-section codec ID.

Initial preferred compression: LZ4 after Android/IL2CPP proof.

If compression package is not accepted, codec 0 = uncompressed remains valid. Compression is not part of semantic schema.

## Integrity

Each section has checksum.

Container may have final SHA-256 for diagnostics/export.

Corrupt optional cache section can be regenerated. Corrupt authoritative section stops load with precise error and preserves original file.

## Atomic save

Process:

1. snapshot/lock consistent authoritative state boundary;
2. write `save.tmp`;
3. flush;
4. validate header/table/checksums;
5. rotate prior autosave if applicable;
6. atomically replace target where platform supports;
7. keep previous known-good file until replacement confirmed.

Never overwrite the only good save in place.

## Autosave

Default:

- 3 rotating autosave slots;
- interval configurable;
- save on significant milestone optionally;
- save on Android background/lifecycle where safe and not redundant.

Autosave is rate-limited.

Manual saves are separate.

## Snapshot strategy

Avoid freezing gameplay for large serialization.

Target architecture:

- at deterministic boundary, copy/version immutable snapshot pages or domain buffers;
- resume simulation;
- serialize snapshot in background/job-safe manner where possible;
- finalise file asynchronously;
- UI shows saving state.

If snapshotting memory cost is excessive, save in bounded chunks with authoritative mutation/version barriers.

## Save consistency

All sections carry one snapshot epoch/simulation minute.

No mix of citizen state from day N and building state from day N+1.

## Derived data

Do not save:

- HLOD meshes;
- primitive instance buffers;
- spatial query caches;
- route caches;
- UI panel state beyond preference;
- imported canonical source records already identified by Universe lock.

Regenerate these.

## Migration

Migration graph is linear for supported production versions unless explicit branch migration is necessary.

Each migration:

- source schema;
- target schema;
- transformation;
- validation;
- failure diagnostic.

Migration never silently invents replacement canonical identity.

## Compatibility

On load compare:

- save schema;
- game version;
- content version;
- Universe commit;
- required content IDs;
- required optional packs/mods.

Display a compatibility report before destructive migration when risk exists.

## Save size targets

Initial target at 250k:

- practical manual save well below 500 MB;
- target substantially lower through compact citizen/domain encoding;
- save time target under 10 seconds on baseline device, stretch under 5 seconds;
- autosave should not cause visible multi-second frame stall.

Actual threshold becomes a Gate F benchmark.

## Tasks

- IMP-SAV-001 — Define container magic/header.
- IMP-SAV-002 — Define section registry.
- IMP-SAV-003 — Implement binary primitive codec.
- IMP-SAV-004 — Implement checksums.
- IMP-SAV-005 — Implement stable ID/string tables.
- IMP-SAV-006 — Implement scheduler/RNG serialization.
- IMP-SAV-007 — Implement chunk/domain codecs.
- IMP-SAV-008 — Implement atomic writer.
- IMP-SAV-009 — Implement autosave rotation.
- IMP-SAV-010 — Implement compatibility report.
- IMP-SAV-011 — Implement migration registry.
- IMP-SAV-012 — Add v1 fixture save.
- IMP-SAV-013 — Add interrupted-write test.
- IMP-SAV-014 — Add midpoint determinism test.
- IMP-SAV-015 — Benchmark 10k/50k/250k save size/time.

## Exit criteria

- interrupted save preserves prior good slot;
- v1 fixture remains permanently loadable by migration test harness;
- save/reload midpoint reproduces reference run;
- changing render assets does not invalidate save;
- unknown required content produces clear compatibility failure;
- route/render caches regenerate instead of bloating save.
