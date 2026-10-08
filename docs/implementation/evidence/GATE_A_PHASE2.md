# Gate A — Phase 2 Simulation Kernel Evidence

**Status:** PASS  
**Implementation branch:** `feature/phase-2-core-architecture`  
**Tested code SHA:** `059e66234fa5b7acc123fca772022524728355ba`  
**Integration PR:** #3  
**Merged head:** `7876fce4f8b9e4211480a89aab519bf1ef926b04`  
**Merge commit:** `15eeeade90bf80d6a1933e198a56f19cf7698ee1`

## Implemented Gate A foundation

- Core, Simulation and Persistence assemblies with enforced dependency direction.
- Canonical/game-authored stable string IDs.
- Deterministic 128-bit save-created IDs with persisted monotonic allocation state.
- Runtime stable-ID collision registry.
- Structured diagnostic IDs and invariant exceptions with domain/time/entity context.
- Exact micro-credit money primitive.
- Project-owned PCG32 and named random stream service.
- Signed 64-bit simulation-minute clock.
- Deterministic cadence and phase scheduler.
- Stable command ordering and scheduled-event heap.
- Command/event/read-model presentation contracts.
- Frame-partition-independent speed budget with no silent mandatory-step dropping.
- Headless laboratory runner and deterministic checksum snapshots.
- `.micity` MICB v1 header/container.
- Explicit little-endian primitive codec.
- Per-section schema, bounds and CRC32 validation.
- Scheduler, allocator and named RNG state serialization.
- Midpoint kernel save/reload equivalence.

## Automated evidence

GitHub Actions run: `37820338112`

EditMode result artifact: `unity-editmode-results-31`

Result:

- 21 tests executed;
- 21 passed;
- 0 failed;
- 0 skipped/inconclusive;
- test duration 0.705 s after Unity startup/compilation.

Gate A-specific passing cases include:

- architecture assembly boundary enforcement;
- stable ID allocator restore without ID reuse;
- stable ID collision rejection;
- exact checked money arithmetic;
- PCG32 replay;
- named RNG stream independence;
- deterministic command/event/system ordering;
- pause advances no authoritative state;
- same-seed headless checksum replay;
- frame partition independence;
- 100-year simulation-minute capacity;
- headless execution without presentation;
- MICB header round-trip;
- corrupted save-section detection;
- midpoint save/reload equivalence;
- scheduled-event ordering preserved across save/reload.

## Gate A acceptance mapping

| Gate A requirement | Evidence |
|---|---|
| deterministic scheduler | scheduler order/replay tests |
| stable IDs | allocator + collision tests |
| root seed/streams | PCG replay + named-stream independence |
| save header | MICB header round-trip |
| instrumentation | structured diagnostics + deterministic checksum |
| headless simulation | presentation-free runner test |
| stable event order | scheduler order + save/reload event order |
| rendering not required | headless test and dependency boundary test |

## Android build evidence

The same GitHub Actions run `37820338112` completed the Android IL2CPP build successfully after the 21/21 EditMode suite passed.

Result:

- Unity EditMode job: PASS;
- Android APK job: PASS;
- no required job failure on the tested code SHA.

## Final integration evidence

PR #3 executed the required workflow on integration head `7876fce4f8b9e4211480a89aab519bf1ef926b04`.

Result:

- pinned Unity/project configuration: PASS;
- Unity EditMode tests: PASS;
- Android IL2CPP APK build: PASS;
- PR state before merge: mergeable;
- merge commit: `15eeeade90bf80d6a1933e198a56f19cf7698ee1`.

**Gate A result: PASS. Phase 2 is complete.**
