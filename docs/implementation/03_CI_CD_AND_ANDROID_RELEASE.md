# 03 — CI/CD and Android Release Pipeline

## Goal

A developer or agent must be able to create code in GitHub, push a branch and obtain test/build results without relying on a permanently configured local workstation.

GitHub Actions is the authoritative automated build path.

## Workflow set

### 1. pull-request.yml

Trigger:
- pull request to `main`;
- manual dispatch.

Jobs:

1. repository/spec validation;
2. generated-content cleanliness check;
3. Unity EditMode tests;
4. deterministic fast simulation tests;
5. content/canon contract tests using the pinned Universe lock;
6. optional PlayMode smoke tests;
7. compilation/build dry-run where licence time permits.

A full Android APK is not mandatory for every tiny PR once compile validation is trustworthy, but branches that alter platform/rendering/serialization must build Android.

### 2. main-build.yml

Trigger:
- push/merge to `main`;
- manual dispatch.

Jobs:

1. all required PR validation;
2. Android development APK;
3. package artefact;
4. publish test/benchmark reports;
5. record commit/package hashes.

The APK is an Actions artifact with versioned filename.

### 3. release.yml

Trigger:
- annotated/version tag such as `v0.1.0`;
- manual release dispatch with explicit version.

Jobs:

1. full validation;
2. signed release APK;
3. signed AAB;
4. checksum generation;
5. GitHub Release creation;
6. attach APK/AAB/release notes;
7. optional later Google Play internal-track upload.

### 4. soak.yml

Trigger:
- nightly or manual;
- not every commit.

Jobs:

- 1-year deterministic scenario;
- 10-year scenario;
- 100-year headless soak;
- benchmark metric comparison;
- save/reload midpoint equivalence;
- memory/entity-count leak checks.

### 5. benchmark.yml

Trigger:
- manual;
- selected main commits;
- package/architecture PRs.

Runs 10k/50k/250k headless benchmark profiles where CI capacity permits. Android hardware performance is tracked separately from editor/headless timing.

## Unity licence

GameCI requires a valid Unity activation/licence arrangement.

Secrets are configured by repository owner and never committed:

- Unity licence content/token/credentials required by the chosen GameCI activation flow;
- any Unity account credentials if required by that flow.

Implementation must document the exact current GameCI method during Phase 1 because activation workflows can change.

## Android signing

Development builds may use a CI-managed development keystore.

Release signing secrets:

- base64/secure keystore;
- keystore password;
- key alias;
- key password.

Secrets never appear in logs.

Release workflow fails if release signing secrets are absent; it must not silently fall back to development signing.

## Google Play deployment

V1 can initially distribute APKs through GitHub Releases/internal testing.

When Play deployment is enabled:

- service account credentials live in GitHub Secrets;
- workflow uploads AAB to Internal Testing first;
- promotion to closed/open/production track is an explicit action;
- production promotion is never automatic from ordinary merge.

## Caching

Cache:

- Unity Library where safe;
- package downloads;
- Gradle dependencies.

Cache key includes:

- Unity version;
- package-lock hash;
- project settings hash where relevant.

Never cache generated canonical runtime data without including Universe lock SHA and importer version.

## Artefacts

PR/main artefacts:

- test results;
- code coverage where valuable;
- benchmark JSON;
- validation report;
- Android APK on main/platform PR;
- log excerpts on failure.

Retention should be bounded to avoid storage waste.

## Failure handling

A failed required workflow blocks merge.

Failure classes:

- compile;
- test;
- content validation;
- deterministic mismatch;
- save migration;
- Universe import;
- benchmark regression;
- Android build/signing.

Flaky tests are treated as bugs. Do not simply retry indefinitely to obtain green status.

## Actions cost discipline

To avoid unnecessary build-minute consumption:

- fast non-Unity validation first;
- cancel superseded branch runs;
- full Android build on merge/main and platform-sensitive PRs;
- nightly soak only when useful;
- benchmark matrices manually or on milestone commits;
- avoid building APK on every documentation-only commit.

## CI proof milestone

IMP-CI-001 — Add GameCI workflow.
IMP-CI-002 — Configure Unity activation secrets.
IMP-CI-003 — Build blank Android APK.
IMP-CI-004 — Install APK on physical Android device.
IMP-CI-005 — Add EditMode test.
IMP-CI-006 — Add PlayMode smoke test.
IMP-CI-007 — Add version metadata screen.
IMP-CI-008 — Upload versioned APK artifact.
IMP-CI-009 — Verify failure logs are retrievable.
IMP-CI-010 — Configure main build.
IMP-CI-011 — Configure release tag build.
IMP-CI-012 — Add signed build only after keystore setup.

## Release gates

A tagged release cannot publish unless:

- repository clean at tag;
- required CI green;
- save format migration suite green;
- Universe import validation green;
- no missing content/localisation references;
- Android package installs;
- startup scene loads;
- main menu and city load smoke test passes;
- version metadata matches tag;
- signing is release signing;
- release notes identify save compatibility.

## Deployment definition

For this project, “deploy” can mean:

1. Actions artifact — fastest developer test;
2. GitHub Release APK — convenient direct install;
3. Google Play Internal Testing — store-managed test;
4. later staged production.

The implementation plan begins with levels 1 and 2 and leaves level 3 plumbing ready.
