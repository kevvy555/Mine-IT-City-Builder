# MineIT City Builder Repository Rules

Read this file in full before implementing code, changing the game specification, importing Universe content, generating assets, or altering game architecture.

## Repository role

This repository is the authoritative home of the **MineIT City Builder** game specification, implementation planning, source code, tests, game-specific authored data, derived build assets, and mutable gameplay systems.

The game is set on Koplin 3 and initially centred on Concordia.

This repository is **not** the canonical source of shared MineIT Universe lore.

## Canonical ownership

Shared MineIT Universe canon is owned by:

- `kevvy555/MineIT-Universe`

The Universe repository is the single authored source of truth for persistent shared universe content such as worlds, settlements, organisations, people, species, substances, materials, technologies, historical events, named ships, economic structures and the Koplin 3 world atlas.

Do not create independently authored replacement copies of canonical Universe entities in this repository.

The City Builder may import, transform, cache, index or package canonical Universe data for runtime use, but those copies are derived game artefacts and must never become authoritative.

Game-specific mechanics, balancing, simulation state, scenario state, saves, generated citizens, generated businesses, procedural buildings and player-created city history belong here.

## Canon source precedence

When interpreting Koplin canon, use the source precedence defined by MineIT-Universe.

The current key precedence is:

1. `data/lore/Koplin_Universe_Expanded_Backstory_Lore_Bible.md`
2. `data/lore/Koplin_Universe_Materials_And_Substances.md`
3. `data/lore/Koplin_Universe_World_Surface.md`
4. `data/lore/Koplin_Scenario_II_Deep_Reach_Mining_Charter.md`
5. Structured canonical JSON under `data/`

Long-form lore is canonical authored content, not commentary.

If imported or structured data conflicts with higher-precedence lore, resolve the conflict in favour of the higher-precedence source rather than inventing a new interpretation locally.

## Branching and integration

`main` is the stable integration branch. Do not use `main` as the normal working branch for feature development, fixes, refactors, balancing work, asset work, or substantial documentation changes.

Create a fresh branch from the latest `main` for each coherent unit of work.

Use clear branch names such as:

- `feature/<short-description>`
- `fix/<short-description>`
- `docs/<short-description>`
- `refactor/<short-description>`
- `chore/<short-description>`

Before starting a new implementation slice, refresh from the latest `main` and branch from that current head. Do not continue unrelated work on a stale feature branch merely because it already exists.

Keep a branch focused on one coherent goal. Do not mix unrelated features or opportunistic redesigns into the same branch.

Commit progress in logical, reviewable increments. A long implementation should have meaningful intermediate commits rather than one very large final commit.

Push work regularly so progress is recoverable and visible.

Open a pull request back to `main` when a branch reaches an integration-ready state. The PR should state the implemented scope, relevant `KCB-*` requirements, specification changes, tests/validation performed, and any known deferred work.

Do not merge a branch with known failing required tests or validation unless the failure is explicitly documented and the merge is an intentional recovery action.

After merge, treat `main` as the new source point. New work should branch from the updated `main`, not from the previously merged feature branch.

Direct commits to `main` should be limited to repository bootstrap/administrative changes, trivial emergency corrections, or an explicit user instruction. Normal development belongs on branches.

## Specification authority

The City Builder specification lives under:

- `docs/specification/`

The master index is:

- `docs/specification/README.md`

The specification is normative unless explicitly labelled as research, example, recommendation, open question or deferred decision.

Stable requirements use `KCB-*` identifiers. Implementation tasks, architecture decisions, tests and acceptance criteria should trace back to those requirement IDs wherever practical.

Do not silently implement behaviour that contradicts the specification. If implementation reveals that a requirement is wrong or impossible, update the specification and record the decision rather than allowing code and specification to drift.

## Keeping the specification current

The specification must describe the game that is actually being built.

Any implementation change that adds, removes, narrows, expands, rebalances, or materially reinterprets specified behaviour must update the affected specification documents in the **same branch and pull request**.

Do not postpone specification updates to a later cleanup task when the implementation has already changed the design.

When implementation discovers a missing requirement, ambiguity, contradiction or better design:

1. update the relevant specification document;
2. add or revise the relevant `KCB-*` requirement where appropriate;
3. update linked implementation/architecture documentation;
4. implement against that revised requirement;
5. update tests and acceptance criteria so they verify the same behaviour.

If the code and specification disagree, the work is not complete. Either change the code to match the specification or deliberately change the specification and record the new decision.

A feature, phase or milestone must not be marked complete until its normative specification, implementation, tests and acceptance criteria agree.

Keep the master index, cross-references, glossary and requirement index current when documents or requirement IDs are added, renamed, split, merged or retired.

Do not leave obsolete normative text in place beside a newer production behaviour. Replace or explicitly deprecate it so there is one authoritative current design.

## Research is not canon

Files under `docs/specification/research/` support design decisions but do not override either:

1. MineIT Universe canon, or
2. normative City Builder specification documents.

External game research may inform mechanics, architecture, UX or rendering techniques. Do not copy proprietary game content, assets, text, maps or implementation details.

## Stable identity

Persistent imported canonical entities retain their Universe stable IDs.

Game-created persistent entities must also use stable IDs suitable for save files, references, migrations and long-running cities.

Never use display names as foreign keys.

Renaming a display name must not change entity identity.

Breaking deletion or replacement of a stable persistent ID requires an explicit migration decision.

## Canonical vs mutable game state

Keep these categories separate:

1. **Universe canon** — imported from MineIT-Universe.
2. **Game-authored definitions** — City Builder mechanics, templates, scenarios, policies, building grammars and balancing data.
3. **Deterministic procedural content** — generated from explicit seeds and generator versions.
4. **Mutable save state** — the player's evolving city and simulation history.

Do not write mutable gameplay results back into Universe canon.

Examples of mutable City Builder state include population, household composition, jobs, property values, traffic, budgets, construction, business inventories, service queues, incidents, policy state, district history and player-created development.

## Simulation principles

The authoritative simulation must be separate from its visual representation.

A change in camera position, graphics quality, rendering distance or animation detail must not materially change macro simulation outcomes.

Visible citizens, vehicles and crowds may be representations of a larger underlying simulation. Never solve performance problems by silently deleting authoritative trips, citizens, traffic, demand or economic activity.

Simulation level-of-detail must preserve aggregate outcomes and conservation rules.

## Determinism

Systems identified by the specification as deterministic must remain reproducible from the same inputs, seed and game version.

Use named random streams or equivalent controlled randomness for independent simulation domains.

Avoid simulation behaviour that depends on unordered iteration, frame rate or rendering state.

Save/reload of the same authoritative state must not change outcomes merely because the game was reloaded.

## Explainability

Major simulation outcomes and failures must be inspectable by the player.

Do not implement opaque shortage, service, economy, transport, happiness, land-value or utility modifiers that cannot expose their meaningful causes.

Where the specification requires causal explanation, the UI should derive explanations from the same authoritative factors used by the simulation rather than maintaining a separate approximation.

## Population and species

Koplin's peoples must not be implemented as racial or species-based productivity classes.

Trondonian, Zoran and Blaxmar history, culture, architecture and institutions may influence authored identity and presentation, but species ancestry must not become a simplistic worker bonus/penalty system.

Mixed ancestry is normal in the modern Commonwealth.

## Artificial intelligence in-world

The setting permits powerful bounded and auditable AI.

Do not model AI as sovereign authority in conflict with established Universe lore.

Critical strategic or civic decisions must retain accountable biological/civic authority where required by canon.

This rule concerns **in-universe AI**. It does not prohibit development-time use of AI tooling.

## Economy and infrastructure

Physical access, logistics, workforce, utilities, capacity, maintenance, policy and externalities should matter where required by the specification.

Avoid decorative service systems where a building merely emits an unexplained circular bonus.

Nominal capacity and effective delivered service must remain conceptually distinct.

## City scale and performance

Design systems for the population and city scales defined by the specification and future implementation plan.

High-count entities should use compact/data-oriented representations rather than one heavyweight object hierarchy per citizen, trip or building when that would prevent the target scale.

Pathfinding, simulation, rendering, saves and UI must be profiled against repeatable benchmark cities.

Performance optimisation must not invalidate core simulation rules.

## Spatial model

Preserve the documented spatial hierarchy:

- planet
- metropolitan region
- canonical 1 km atlas tile
- simulation chunk
- block
- parcel
- building footprint
- abstract interior/occupancy state where applicable

The Koplin 3 atlas from MineIT-Universe is canonical world-space input.

Do not casually change atlas coordinates, orientation or stable tile identity inside the game to make implementation easier. Use an explicit transformation/import layer where required.

## Procedural content

Procedural systems must be deterministic where save stability requires it.

Generated buildings, parcels, citizens, businesses, names and variation should retain sufficient provenance to reproduce or migrate them, including seeds and generator versions where appropriate.

Procedural generation must remain constrained by Koplin art direction and city rules rather than becoming unconstrained random variation.

## Art direction

The canonical Concordia visual language is defined in the specification and informed by the Koplin 3 atlas.

Preserve the established direction: prosperous, mature, advanced Commonwealth city; clean light structures; strong orange accents; dark mechanical detailing; blue glass; integrated vegetation; appropriate biodomes and civic landscaping.

Avoid drifting into generic neon cyberpunk, dystopian grime, ruin aesthetics or present-day Earth styling unless an explicitly authored scenario calls for it.

Art should communicate simulation state where practical.

## Save compatibility

Treat save format as a versioned public contract.

Persistent state must use stable IDs and explicit schema/game/content versions.

Derived caches should be regenerable and should not become the sole copy of authoritative state.

Save migrations require automated fixture coverage.

Do not knowingly break existing saves without an explicit migration/versioning decision.

## Data and import pipeline

Universe integration must occur through an explicit import/adaptation layer.

Do not scatter direct assumptions about Universe JSON structure throughout simulation code.

Validate imported IDs, references, source versions and required fields.

Broken canonical references must fail visibly in development/build validation rather than being silently ignored.

## Architecture

Prefer clear domain boundaries and explicit contracts between systems.

Simulation, presentation/rendering, persistence, content import and UI should not become tightly coupled.

High-count simulation data should favour data-oriented storage and predictable update cadence.

Avoid global mutable state where domain-owned state or explicit services can be used.

Do not create duplicate production implementations such as `v2`, `new`, `alternate` or parallel systems to avoid refactoring. Prefer migrating the authoritative implementation.

## Implementation planning

Before major implementation begins, the implementation plan must resolve or explicitly defer the decisions identified by:

- `docs/specification/33_IMPLEMENTATION_CONTRACTS_AND_HANDOFF.md`

This includes target platforms, engine/framework, rendering pipeline, population/city scale, simulation cadence, entity strategy, pathfinding, procedural building technology, serialization, modding boundaries, canon packaging and performance budgets.

Major implementation epics should include a requirement traceability matrix back to `KCB-*` requirements.

## Testing and validation

Automated validation is mandatory for systems where the specification defines invariants.

At minimum, preserve or extend tests for:

- deterministic simulation;
- save/load equivalence;
- stable IDs and references;
- population/economic conservation;
- network connectivity and pathfinding;
- utility and service capacity;
- game-data validation;
- Universe import validation;
- save migrations;
- simulation LOD equivalence;
- benchmark city performance;
- accessibility-critical UI behaviour where automatable.

Use the benchmark and fixture scenarios defined by the specification rather than relying only on hand-played happy paths.

## Rendered UI acceptance

Every implementation phase must have an explicit UI test plan, including phases whose intended work is simulation, persistence, data import or other non-presentation infrastructure.

Use `docs/qa/14_RENDERED_UI_PHASE_ACCEPTANCE.md` as the mandatory rendered-UI acceptance contract.

Before claiming a phase complete:

1. declare its UI impact level (`UI-0` through `UI-4`);
2. identify affected screens, panels, overlays or state that should remain unchanged;
3. execute the required automated UI/resource/layout checks;
4. execute physical Android rendered-UI acceptance for `UI-1+` phases;
5. execute physical touch/interaction acceptance for `UI-2+` phases;
6. retain screenshot/video evidence where required;
7. record the UI result in the phase evidence/handover.

`UI-0` means “no intended UI change,” not “skip UI QA.” It still requires regression proof that the existing shell renders correctly.

Do not equate any of the following with rendered UI acceptance:

- Unity compilation;
- an APK being produced;
- a `UIDocument`, `Label` or `Button` existing in the hierarchy;
- text strings being non-empty in memory;
- the application process starting successfully.

A phase with a known blank, missing, clipped, unthemed, unreadable or unusable critical UI surface is not complete.

When physical-device testing exposes a UI defect missed by automation, add a regression test where practical and update the QA process if the gap is systemic.

## Accessibility

Accessibility is a core requirement, not a late polish phase.

New UI systems must support the specification's requirements for scalable text, contrast, non-colour signalling, input remapping, reduced motion, captions, narration metadata and localization readiness where applicable.

Do not build core interactions that depend only on hover, precise mouse control, colour distinction or audio cues.

## Documentation

Keep the specification and architectural documentation updated when implementation decisions materially change behaviour, architecture, persistence, data ownership, performance assumptions, platform constraints or Universe integration.

Architecture decisions that narrow or reinterpret the specification should be recorded explicitly rather than existing only in code or commit messages.

## Changes

Prefer one authoritative implementation over parallel production alternatives.

Refactor the current path instead of leaving old and new systems active indefinitely.

Make changes in coherent, reviewable commits.

Before claiming a feature or phase complete, verify the relevant tests, validation and requirement acceptance criteria.
