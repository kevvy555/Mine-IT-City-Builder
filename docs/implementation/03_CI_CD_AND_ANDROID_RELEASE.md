# 03 — CI/CD and Android Release Pipeline

## Goal

A developer or agent must be able to create code in GitHub, push a branch and obtain test/build results without relying on a permanently configured local workstation.

GitHub Actions is the authoritative automated build path.

## Resolved Phase 1 CI actions

The first production CI workflow pins:

- `game-ci/cli@v0.1.72` for Android builds;
- `game-ci/unity-test-runner@v4.4.0`;
- `actions/checkout@v4`;
- `actions/upload-artifact@v4`.

The Unity workflow lives at `.github/workflows/unity-ci.yml`. Phase 1 executes jobs sequentially: configuration sanity -> EditMode tests -> Android APK build, so a failed test cannot publish a misleading build artifact. The Android build uses the GameCI CLI directly because Unity 6 Personal entitlement-based activation works with account credentials while the legacy `unity-builder@v5` wrapper still requires a `.ulf` or serial during its preflight.

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

Development APKs use one stable, project-owned **development-only** signing identity defined by `config/android-dev-signing.json`.

The development signing identity is deliberately not treated as a secret: it exists only so APKs built by different GitHub runners have the same certificate and can update one another during testing. Current alias/fingerprint:

- alias: `mineit-dev`;
- SHA-256: `F9:F5:6A:F8:5C:06:27:FA:C3:99:AE:16:FC:C4:4C:D3:14:33:F4:EE:C5:DC:8F:70:F7:84:65:F4:8E:FB:BE:9E`.

CI verifies the produced APK certificate with Android `apksigner` before uploading the artifact. A mismatched or unsigned APK fails the job.

**Transition rule:** APKs produced before stable development signing used transient runner/debug identities. A tester may need to uninstall one old APK once. After installing the first stable-signed development APK, subsequent development APKs with the same package ID and non-decreasing version code must install as updates.

The development key must never be used for Play Store or production release signing.

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

## Caching and build-time optimisation

The initial cold Unity 6.3 Android pipeline was intentionally simple and therefore expensive. Measured Phase 3 cold evidence:

- EditMode Unity execution: about 4 minutes 15 seconds;
- Android job: about 30 minutes wall-clock;
- Unity `BuildPipeline.BuildPlayer` portion: 23 minutes 51 seconds.

The major cost is Unity import/IL2CPP/Bee work on an ephemeral runner, not Git checkout or the Universe import.

The workflow now caches, separately for EditMode and Android:

- `MineITCityBuilder/Library`;
- `MineITCityBuilder/Assets/Game/Generated` so generated assets can retain stable Unity metadata across warm builds while their content is deterministically overwritten.

Cache identity includes:

- OS;
- Unity version;
- package/project settings hash;
- Universe lock hash;
- current commit as the exact key with a compatible-prefix restore fallback.

This allows a changed commit to restore the most recent compatible Unity state and incrementally rebuild it. The first cache-enabled run remains effectively cold; later compatible runs are the useful comparison.

Do not use `AssetDatabase.Refresh(ForceUpdate)` in the normal build path unless a defect proves it necessary; forcing all imported assets to refresh defeats the cache.

Further optimisation, if required after measuring warm-cache results:

- split Android APK builds from every development push and run them only for phase candidates/PRs/manual requests;
- persist or cache additional Gradle/Unity package state where GameCI exposes it safely;
- consider a persistent/self-hosted Android Unity runner only if hosted-cache performance remains unacceptable.

Never trade deterministic/reproducible build correctness for speed.

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
