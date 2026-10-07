# 01 — Requirement Acceptance and Coverage

## Goal

Prove that every normative KCB requirement is implemented and verified, or explicitly deferred by the specification.

## Requirement registry

The implementation plan already defines a generated requirement registry. QA extends each derived row with:

```text
requirement_id
source_document
source_line
implementation_epic
implementation_task
qa_id
verification_type
fixture/scenario
automation
execution_stage
status
evidence_uri
last_verified_commit
notes
```

The registry is generated from Markdown plus QA mapping metadata. Requirement prose remains in the specification only.

## Verification classes

### UNIT

Pure formula/value/data validation.

Examples:
- fixed-point money;
- cost functions;
- capacity factors;
- date logic.

### INVARIANT

Property that must always hold.

Examples:
- population reconciliation;
- no negative inventory;
- IDs unique.

### DETERMINISTIC

Same input/seed/build produces the defined result/tolerance.

### SCENARIO

Reference city and scripted input produces an expected behavioural envelope.

### CAUSAL

Changing one controlled factor causes an expected directional/system response.

### PERFORMANCE

Measured timing/memory/throughput/device target.

### VISUAL

Presentation/identity/rendering acceptance.

### ACCESSIBILITY

Touch, focus, narration, contrast, reduced-motion or equivalent.

### CANON

Comparison against MineIT-Universe and canon precedence.

### MANUAL-PLAY

Human task/experience verification where automation cannot establish quality.

## Requirement-family QA mapping

| KCB family | Primary QA |
|---|---|
| CROSS | QA-CAU / QA-XSYS / QA-UX |
| VIS | QA-UX / QA-LONG / manual play |
| CAN | QA-CAN |
| ROLE/MODE | QA-XSYS / QA-UX |
| MAP | QA-INV / QA-SAV / QA-XSYS |
| TIME | QA-SAV / deterministic |
| GROW | QA-CAU / QA-MOD / QA-XSYS |
| ROAD | QA-INV / QA-CAU / performance |
| TRANS/TRAF | QA-CAU / QA-XSYS / QA-PERF |
| POP/WORK | QA-INV / QA-MOD / QA-LONG |
| HOUSE/ECO/IND | QA-INV / QA-MOD / QA-LONG |
| SERV/UTIL/RES | QA-CAU / QA-XSYS |
| GOV/TECH | QA-CAN / QA-CAU / QA-UX |
| ENV | QA-MOD / QA-XSYS / QA-LONG |
| CULT | QA-CAN / manual review |
| BLD | QA-INV / QA-CAU / QA-PERF |
| ART | QA-PERF / QA-CAN / visual QA |
| INP/UI | QA-UX |
| AUD | QA-UX / manual device |
| EVT/PROG | QA-XSYS / deterministic |
| DATA | QA-SAV / QA-CAN |
| ARCH | QA-PERF / deterministic |
| ACC/LOC | QA-UX |
| QA | QA infrastructure self-tests |
| CONT | QA-CAN / content validator |
| HAND | implementation gate evidence |
| IDX | coverage tooling |

## Coverage rule

QA-REQ-001 — Every normative `KCB-*` ID must resolve to at least one QA verification ID or explicit deferred status.

QA-REQ-002 — Every `MUST` requirement must have at least one objective verification method.

QA-REQ-003 — Requirements whose quality cannot be fully automated must have a documented manual acceptance procedure.

QA-REQ-004 — One QA case may cover multiple KCB requirements only when evidence genuinely verifies each requirement.

QA-REQ-005 — No requirement can become `Accepted` solely because another requirement in the same prefix passed.

## Coverage thresholds

Before first release candidate:

- 100% KCB requirements mapped;
- 100% non-deferred MUST requirements executed;
- 100% Blocker/Critical acceptance cases passing;
- no `Uncovered`;
- no unexplained `Blocked`;
- deferred requirements agree with specification.

## Change handling

When a KCB requirement is added or changed:

1. requirement-index CI identifies change;
2. QA coverage becomes missing/stale;
3. PR must add/update QA mapping;
4. tests/fixtures updated where necessary;
5. existing acceptance evidence is invalidated if semantics changed.

## Evidence granularity

Automated:
- workflow/run ID;
- test result;
- commit;
- fixture seed.

Manual:
- tester;
- build;
- device;
- procedure;
- result;
- screenshot/video/notes where useful.

## Initial tooling

QA-REQ-010 — Extend `tools/RequirementIndex` to emit QA columns.

QA-REQ-011 — Fail CI for KCB requirement with no QA mapping after its implementation phase begins.

QA-REQ-012 — Generate coverage summary by requirement family.

QA-REQ-013 — Generate release acceptance report of Passed/Failed/Blocked/Deferred.

QA-REQ-014 — Detect stale evidence after requirement semantic change.

## Final acceptance

The final report must allow us to answer:

> For KCB-X, where is it implemented, how was it tested, when did it last pass, and what evidence proves it?

If that cannot be answered, the requirement is not accepted.
