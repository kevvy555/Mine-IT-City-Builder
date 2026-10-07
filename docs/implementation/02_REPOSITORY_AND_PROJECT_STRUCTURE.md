# 02 — Repository and Unity Project Structure

## Repository root

Proposed structure:

```text
/
├─ AGENTS.md
├─ README.md
├─ .github/
│  └─ workflows/
├─ docs/
│  ├─ specification/
│  ├─ implementation/
│  ├─ architecture/
│  └─ adr/
├─ MineITCityBuilder/
│  ├─ Assets/
│  │  ├─ Game/
│  │  │  ├─ Bootstrap/
│  │  │  ├─ Content/
│  │  │  ├─ Generated/
│  │  │  ├─ Presentation/
│  │  │  ├─ Simulation/
│  │  │  ├─ Platform/
│  │  │  ├─ UI/
│  │  │  ├─ Tests/
│  │  │  └─ Editor/
│  │  ├─ Settings/
│  │  └─ ThirdParty/
│  ├─ Packages/
│  └─ ProjectSettings/
├─ tools/
│  ├─ UniverseImporter/
│  ├─ RequirementIndex/
│  ├─ SaveInspector/
│  └─ BenchmarkReports/
├─ config/
│  ├─ universe.lock.json
│  ├─ build-version.json
│  └─ benchmark-thresholds.json
└─ scripts/
```

The Unity project lives one level below repository root so repository tooling/docs do not pollute Unity's Assets tree.

## Assembly boundaries

Use explicit asmdefs.

### Simulation assemblies

- `MineIT.Sim.Core`
- `MineIT.Sim.Time`
- `MineIT.Sim.World`
- `MineIT.Sim.Development`
- `MineIT.Sim.Population`
- `MineIT.Sim.Economy`
- `MineIT.Sim.Mobility`
- `MineIT.Sim.Utilities`
- `MineIT.Sim.Services`
- `MineIT.Sim.Environment`
- `MineIT.Sim.Governance`
- `MineIT.Sim.History`
- `MineIT.Sim.Persistence`

### Data/content assemblies

- `MineIT.Content.Schema`
- `MineIT.Content.Runtime`
- `MineIT.Canon.Runtime`

### Presentation assemblies

- `MineIT.Presentation.Core`
- `MineIT.Presentation.Primitives`
- `MineIT.Presentation.World`
- `MineIT.Presentation.Agents`
- `MineIT.Presentation.Overlays`

### App/UI/platform

- `MineIT.App`
- `MineIT.UI`
- `MineIT.Platform.Android`

Dependencies flow inward. Presentation may read simulation snapshots but simulation must not depend on presentation/UI.

## Source ownership rules

### Assets/Game/Simulation

Contains authoritative runtime systems/components. No textures, UI panels or Android platform code.

### Assets/Game/Presentation

Contains render archetypes, mesh/material bindings, display interpolation and effects. It may store an entity's presentation handle but not authoritative city state.

### Assets/Game/Generated

Generated files only. Every generated folder includes a header/readme stating generator/version/source. Files must be reproducible. Do not hand-edit.

### Assets/Game/Content

Game-owned authored definitions: building archetypes, road templates, policies, primitive visual grammars, scenario definitions, localisation tables.

### ThirdParty

Only externally sourced code/assets. Each dependency requires licence/provenance metadata.

## Namespace convention

`MineIT.CityBuilder.<Domain>`

Examples:

- `MineIT.CityBuilder.Simulation.Population`
- `MineIT.CityBuilder.Presentation.Primitives`
- `MineIT.CityBuilder.Tools.UniverseImport`

Avoid generic global namespaces.

## Stable IDs

Use typed stable IDs at domain boundaries, not raw strings everywhere.

Recommended logical shape:

```text
StableId
  namespaceHash / namespace key
  local stable key
```

Human-authored IDs remain lower-case namespaced slugs in content. Runtime may map to compact integers/hashes through a deterministic registry but save files retain enough mapping/version metadata to detect collisions and migrations.

Transient ECS Entity values never become persisted identity.

## Command/query pattern

UI and tools submit commands:

- BuildRoadCommand
- ZoneAreaCommand
- ChangePolicyCommand
- DemolishBuildingCommand

Simulation emits domain events/snapshots:

- RoadNetworkChanged
- BuildingCompleted
- HouseholdMoved
- UtilityOutageStarted

This avoids UI code reaching directly into internal component storage.

## Configuration

No hidden constants in MonoBehaviours for simulation tuning.

All important tuning belongs in:

- validated ScriptableObject authoring definitions that bake to immutable runtime blobs; or
- JSON/YAML/CSV source processed into runtime data where easier to review/diff.

Build output must record content version/hash.

## Editor tooling

Create dedicated editor tools for:

- content validation;
- canonical atlas preview;
- primitive archetype preview;
- road template preview;
- parcel diagnostics;
- ECS entity inspection;
- benchmark generation;
- save inspection/migration;
- deterministic replay.

Tools cannot become required dependencies of runtime assemblies.

## Branching application

Normal implementation:

1. update local/remote view of `main`;
2. create feature branch;
3. update specification in same branch if behaviour changes;
4. implement/test;
5. push logical commits;
6. open PR;
7. CI runs;
8. merge when acceptance gate passes;
9. next phase branches from new `main`.

## Versioning

Maintain:

- semantic game version;
- save format version;
- content schema version;
- Universe lock SHA;
- generator versions;
- build number/commit SHA.

Development APK filename:

`MineIT-City-Builder-<gameVersion>-<shortSha>-dev.apk`

Release:

`MineIT-City-Builder-<gameVersion>.apk`
`MineIT-City-Builder-<gameVersion>.aab`

## Initial tasks

- IMP-FND-020 — Create root Unity project directory.
- IMP-FND-021 — Add Unity .gitignore/.gitattributes.
- IMP-FND-022 — Create asmdef dependency skeleton.
- IMP-FND-023 — Create domain namespaces.
- IMP-FND-024 — Add build/version metadata service.
- IMP-FND-025 — Create content/generated folder rules.
- IMP-FND-026 — Create ADR template.
- IMP-FND-027 — Create architecture doc template.
- IMP-FND-028 — Add third-party licence inventory.
- IMP-FND-029 — Add editor validation menu/CLI entrypoint.

## Exit criteria

- clean Unity import from fresh checkout;
- no circular asmdef dependencies;
- generated and authored content clearly separated;
- headless test assemblies runnable in CI;
- build metadata identifies repo SHA, game version, content version and Universe SHA.
