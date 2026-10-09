# 13 — Final Specification Acceptance Gate

## Gate QA — Specification Acceptance

This is the final answer to:

> Did we implement the approved MineIT City Builder specification correctly?

Gate QA is separate from implementation completion. It is possible for all planned code to exist and Gate QA to fail.

## Preconditions

Before running final Gate QA:

- implementation Gates A–G passed;
- release-candidate build produced;
- specification frozen for candidate except defect corrections;
- requirement registry current;
- QA mappings current;
- supported device list current;
- supported save versions declared.

## Required acceptance

### Requirement coverage

QA-REL-001:
- 100% KCB requirements mapped to QA;
- 100% non-deferred MUST requirements executed;
- no Uncovered requirement;
- no unexplained Blocked requirement.

### Automated correctness

QA-REL-010:
- unit/system/invariant suite green;
- deterministic suite green;
- reference laboratory scenarios green;
- causal tests green;
- content/canon automated validation green.

### Persistence

QA-REL-020:
- all supported save fixture migrations green;
- interrupted-save tests green;
- midpoint deterministic roundtrip green;
- PVG/HQ/graphics-tier save compatibility green.

### Long run

QA-REL-030:
- required 100-year suite green;
- no runaway/invariant failures;
- technical structures remain bounded;
- recovery scenarios pass.

### Android

QA-REL-040:
- clean install;
- upgrade install;
- minimum/reference/high device smoke;
- B250K Gate F evidence current;
- thermal protocol current;
- signed APK/AAB valid;
- lifecycle/background/save checks pass.

### Rendered UI / UX / accessibility

QA-REL-050:
- all phase UI impact declarations and required `QA-UI-*` evidence present;
- no unresolved blank/missing-font/missing-theme/clipped critical UI defect;
- supported-device rendered UI smoke current;
- touch-only core workflow;
- diagnosis tasks;
- UI scaling;
- colour independence;
- reduced motion;
- TalkBack/accessibility semantics target;
- localisation/pseudo-localisation;
- no Critical accessibility issue.

### Canon

QA-REL-060:
- Universe SHA recorded;
- canonical IDs validated;
- Concordia/canon visual/content review;
- species/AI/technology safeguards pass.

### Defects

QA-REL-070:
- zero open Blocker;
- zero open Critical;
- Major defects explicitly reviewed against release criteria.

## Acceptance report

Generate:

```text
Build:
Game version:
Commit:
Universe SHA:
Content version:
Save schema:
Devices:
Requirements total:
Requirements accepted:
Deferred:
Failed:
Blocked:
Automated tests:
Manual tests:
Rendered UI device evidence:
100-year suites:
B250K result:
Open defects:
Gate QA result:
Sign-offs:
```

## Possible results

### PASS

All mandatory acceptance conditions satisfied.

### PASS WITH ACCEPTED MAJOR ISSUES

Allowed only if:
- no Blocker/Critical;
- affected KCB requirement still passes;
- issue is documented and does not undermine product contract.

### FAIL

Any mandatory condition fails.

A FAIL returns work to the owning implementation phase/domain. We do not weaken QA merely to release.

## Post-release

Gate QA is repeated for every production release against:
- changed requirements;
- regression suite;
- all supported save versions;
- current supported device matrix.

A minor patch may use a scoped regression run, but hard invariants, save integrity and changed-area KCB acceptance remain mandatory.
