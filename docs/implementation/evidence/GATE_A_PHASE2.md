# Gate A — Phase 2 Simulation Kernel Evidence

**Status:** Candidate — automated Gate A tests pass; Android IL2CPP build pending  
**Implementation branch:** `feature/phase-2-core-architecture`  
**Tested code SHA:** `059e66234fa5b7acc123fca772022524728355ba`

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

## Remaining before PASS

- Android IL2CPP build for the tested code SHA must complete successfully.
- PR checks must be green.
- Final merge SHA must be recorded after integration.

Phase 2 should not be declared complete until those remaining checks are closed.
