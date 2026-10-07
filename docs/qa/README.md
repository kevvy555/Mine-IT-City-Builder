# MineIT City Builder — QA and Specification Acceptance Plan

**Status:** QA plan defined before implementation; executed continuously during implementation and comprehensively before release  
**Source of truth:** `docs/specification/`  
**Implementation plan:** `docs/implementation/`

## Purpose

This catalogue defines how MineIT City Builder will prove that the implemented game matches the approved specification.

The implementation plan answers **how the game will be built**.  
The QA plan answers **how we will prove it was built correctly**.

QA is therefore not a separate design authority. The test oracle is:

1. normative `KCB-*` requirements in `docs/specification/`;
2. implementation contracts in `docs/implementation/`;
3. canonical MineIT Universe data/lore;
4. explicit quantitative envelopes and invariants defined in this QA catalogue.

If implementation and QA expose a flaw in the specification, the specification must be deliberately revised rather than changing the test to make incorrect behaviour pass.

## QA principles

### Requirement-driven

Every normative `KCB-*` requirement must have:

- an implementation owner;
- a QA verification method;
- an execution stage;
- evidence;
- a final status.

A requirement is not complete merely because code exists.

### Behaviour over screenshots

A city builder is an emergent simulation. Most tests must verify:

- conservation;
- direction of response;
- causal mechanism;
- bounded ranges;
- stability;
- recovery;
- determinism.

Exact numeric equality is used only where the specification requires it.

### Deterministic laboratory cities

Fixed seeded reference cities reproduce difficult conditions so a change can be compared against a known baseline.

### Cross-system validation

Many failures only appear through interactions:

```text
housing -> population -> jobs -> trips -> congestion -> services -> land value -> economy
```

QA therefore includes scenario tests spanning multiple domains.

### Android is the production environment

Editor/headless tests are necessary but insufficient. Performance, lifecycle, rendering, input, accessibility, save behaviour and thermal performance require Android evidence.

### Evidence is retained

A gate result must point to reproducible evidence:

- commit/build;
- test run;
- scenario ID/seed;
- device;
- metrics;
- screenshots where useful;
- failure/exception notes.

## QA document map

1. [Requirement Acceptance and Coverage](01_REQUIREMENT_ACCEPTANCE_AND_COVERAGE.md)
2. [Reference Laboratory Cities](02_REFERENCE_LABORATORY_CITIES.md)
3. [Invariants and Conservation Rules](03_INVARIANTS_AND_CONSERVATION.md)
4. [Causal and Directional Behaviour Tests](04_CAUSAL_AND_DIRECTIONAL_TESTS.md)
5. [Quantitative Model Validation](05_QUANTITATIVE_MODEL_VALIDATION.md)
6. [Cross-System Scenario Validation](06_CROSS_SYSTEM_SCENARIOS.md)
7. [Canon and Content Validation](07_CANON_AND_CONTENT_VALIDATION.md)
8. [Save, Migration and Determinism QA](08_SAVE_MIGRATION_AND_DETERMINISM.md)
9. [Android Performance and Device QA](09_ANDROID_PERFORMANCE_AND_DEVICE_QA.md)
10. [Mobile UX, Accessibility and Explainability QA](10_MOBILE_UX_ACCESSIBILITY_AND_EXPLAINABILITY.md)
11. [Long-Run Stability and Balance QA](11_LONG_RUN_STABILITY_AND_BALANCE.md)
12. [Execution, Evidence and Defect Process](12_EXECUTION_EVIDENCE_AND_DEFECT_PROCESS.md)
13. [Final Specification Acceptance Gate](13_FINAL_SPECIFICATION_ACCEPTANCE_GATE.md)

## QA identifier conventions

QA artefacts use stable IDs:

- `QA-REQ-*` — requirement coverage;
- `QA-LAB-*` — laboratory cities;
- `QA-INV-*` — invariants;
- `QA-CAU-*` — causal tests;
- `QA-MOD-*` — quantitative model tests;
- `QA-XSYS-*` — cross-system scenarios;
- `QA-CAN-*` — canon/content tests;
- `QA-SAV-*` — persistence/determinism;
- `QA-PERF-*` — performance/device;
- `QA-UX-*` — UX/accessibility/explainability;
- `QA-LONG-*` — long-run/balance;
- `QA-REL-*` — final/release acceptance.

QA IDs are test identities, not replacements for KCB requirements.

## Verification status

A KCB requirement can be:

- **Uncovered** — no QA method assigned; not acceptable after QA planning.
- **Planned** — method exists but feature not implemented.
- **Runnable** — implementation exists and test can execute.
- **Passing** — current build passes.
- **Failing** — current build does not meet the requirement.
- **Blocked** — external/prerequisite issue prevents execution.
- **Deferred** — only valid when the specification explicitly defers the requirement.
- **Accepted** — final evidence approved for release.

## Automation target

Automate everything deterministic and repeatable.

Manual QA remains appropriate for:

- perceived touch usability;
- visual identity;
- soundscape;
- TalkBack quality;
- player diagnosis tasks;
- aesthetic/canon review;
- physical-device thermal observations.

Manual does not mean undocumented. Manual checks still use IDs, procedure and recorded result.

## Relationship to implementation gates

Implementation Gates A–G remain engineering milestones. Each gate now requires linked QA evidence.

Final release adds **Gate QA — Specification Acceptance**, defined in `13_FINAL_SPECIFICATION_ACCEPTANCE_GATE.md`.

The game is not considered specification-complete until Gate QA passes.
