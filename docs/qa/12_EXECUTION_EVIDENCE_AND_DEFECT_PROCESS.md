# 12 — Execution, Evidence and Defect Process

## Test layers and cadence

### Every PR

- phase UI impact declaration (UI-0..UI-4) and linked `QA-UI-*` cases;
- requirement coverage for changed KCB/IMP;
- unit tests;
- invariants relevant to change;
- deterministic short fixture;
- content validation;
- compilation;
- rendered-UI structural/resource checks when presentation is touched.

### Domain integration PR

Add:
- affected QA-LAB scenarios;
- causal tests;
- save roundtrip if persistent;
- benchmark if hot path.

### Main/nightly

- broader fixture suite;
- B10K;
- 1/10-year runs;
- migration fixtures;
- screenshot/visual captures where relevant.

### Milestone

- full gate-specific QA;
- physical Android rendered-UI acceptance for all UI-1+ work since the previous milestone;
- B50K or B250K where required;
- physical Android;
- manual UX/accessibility;
- long soak.

### Release candidate

- all non-deferred requirement acceptance;
- full supported-save migration;
- supported device matrix;
- 100-year suite;
- Android thermal;
- canon/manual review;
- final Gate QA.

## Evidence record

Suggested JSON/Markdown record:

```text
qaId
buildSha
gameVersion
contentVersion
universeSha
fixtureId
seed
device
startedUtc
duration
result
metrics
artifacts
uiImpactLevel
screensChecked
screenshots
notes
```

Time is evidence metadata, not simulation input.

## Golden/envelope updates

Never update expected results solely because a test failed.

A baseline change requires one of:

- bug fixed and expected returns to original;
- deliberate spec/design/tuning change;
- previous envelope proven invalid.

PR must show:
- old;
- new;
- reason;
- affected KCB/QA IDs.

## Defect severity

### Blocker

- data loss/corruption;
- cannot install/start/load;
- invariant corruption affecting city truth;
- supported-save migration failure;
- severe canon contradiction in shipped core content.

### Critical

- core mechanic materially contradicts specification;
- frequent crash;
- major accessibility blocker;
- deterministic corruption;
- economy/population unrecoverably broken under ordinary play.

### Major

- significant wrong behaviour with workaround;
- large performance regression below target;
- important UI diagnosis failure.

### Minor

- local/cosmetic/noncritical issue.

## Defect record

Must include:
- KCB requirements;
- QA case;
- build;
- save/fixture/seed;
- steps/input script;
- expected;
- actual;
- evidence;
- severity.

## Flaky tests

A flaky test is a defect.

Rules:
- record frequency;
- determine nondeterminism/environment cause;
- may quarantine only with owner/issue/expiry;
- quarantined required test cannot count as release pass.

## Manual QA

Manual runs use checklists with pass/fail evidence.

For UI-1+ phases, physical Android rendered-UI acceptance follows `14_RENDERED_UI_PHASE_ACCEPTANCE.md` and records the exact build/device plus screenshots where required.

“Tester played for a while” and “APK boots” are not sufficient acceptance evidence.

## Reproduction bundles

For complex simulation defect, export:

- save;
- seed;
- input/command log;
- Universe/content versions;
- metric trace;
- relevant subsystem diagnostics.

This enables deterministic replay.

## QA dashboard

Generate:

- requirement coverage;
- pass/fail by family;
- blocked tests;
- defects by severity;
- benchmark trend;
- migration coverage;
- device coverage;
- last successful 100-year run.

## Sign-off roles

Initially the project owner may provide product acceptance while automated evidence supplies technical acceptance.

As team grows, separate:
- engineering sign-off;
- QA sign-off;
- canon/content sign-off;
- product sign-off.

No single manual opinion overrides failing hard invariants or Blocker defects.
