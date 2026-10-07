# 08 — Save, Migration and Determinism QA

## Purpose

Protect long-running player cities and prove reproducible authoritative simulation.

## Permanent fixture saves

Every production save schema version creates immutable fixtures:

- minimal city;
- vertical slice;
- representative mid-size city;
- stress/edge-case city where necessary.

Fixtures are retained indefinitely for supported migration paths.

## QA-SAV-001 — Immediate roundtrip

Save -> load without simulation advancement.

Assert:
- simulation timestamp identical;
- stable IDs identical;
- authoritative checksum identical;
- scheduler/RNG state identical.

## QA-SAV-002 — Midpoint equivalence

Run N days continuously.

Run same seed/input:
- N/2 days;
- save/load;
- N/2 days.

Compare exact/tolerance-defined authoritative metrics/checksum.

## QA-SAV-003 — Event ordering

Schedule many same-time events across domains.

Save before execution.

Load and verify deterministic priority/tie-break order.

## QA-SAV-004 — Interrupted write

Interrupt/fail temp save at controlled stages.

Assert previous good slot loads.

## QA-SAV-005 — Autosave rotation

Verify:
- expected number slots;
- newest valid selection;
- corrupt newest can fall back with clear warning where supported;
- manual save not overwritten.

## QA-SAV-006 — Schema migration

For every migration:

- load old fixture;
- migrate;
- validate;
- compare expected semantic state;
- save new version;
- reload new version.

## QA-SAV-007 — Content display rename

Change display name with stable ID.

Old save loads same entity identity.

## QA-SAV-008 — Missing content

Remove/disable required content pack/mod in test.

Expected:
- compatibility screen;
- precise missing IDs;
- no silent substitution that corrupts city.

## QA-SAV-009 — Universe version change

Update lock to compatible newer Universe commit.

Verify:
- canonical IDs map;
- save remains valid;
- changed display metadata does not rewrite player state;
- removed/changed IDs require explicit migration/compatibility decision.

## QA-SAV-010 — Renderer replacement

Switch PVG -> HQ visual or quality level.

Simulation checksum unchanged.

## QA-SAV-011 — Frame-rate independence

Run same scripted input at controlled 30/60/variable render rates where applicable.

Authoritative outcome equal within deterministic contract.

## QA-SAV-012 — Worker-count/order robustness

Where Jobs scheduling can vary, repeated runs must satisfy chosen exact/tolerance deterministic contract.

## QA-SAV-013 — Background lifecycle

On Android:
- start autosave;
- background/resume;
- lock/unlock;
- kill after confirmed save;
- relaunch.

No corrupt save.

## Save performance

Record for B10K/B50K/B250K:

- snapshot main-thread stall;
- encode time;
- write time;
- size;
- load time;
- migration time.

Compare against implementation performance budgets.

## Data forensic tool

Save inspector should report:

- header/schema;
- game/content/Universe versions;
- sections;
- checksum;
- counts;
- migration history.

It must not modify file unless explicit repair/migration command.

## Release blocker

Any reproducible supported-save data loss/corruption or migration failure is Blocker severity.
