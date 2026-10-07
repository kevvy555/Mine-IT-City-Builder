# 34 — Glossary and Requirement Index

## Glossary

**Active set** — entities currently receiving high-frequency detailed simulation.

**Aggregate simulation** — lower-detail computation representing many entities/flows while conserving authoritative totals.

**Atlas tile** — canonical 1 km x 1 km Koplin 3 world-atlas unit.

**Authoritative state** — simulation data whose value determines game outcomes and must survive save/load exactly or within documented deterministic rules.

**Block** — polygonal urban area bounded by rights of way, water or other hard boundaries.

**Canon** — persistent MineIT Universe facts governed by repository canon precedence.

**Catchment** — area/population able to reach a service within a defined travel/access threshold.

**Chunk** — implementation spatial subdivision used for streaming/scheduling.

**Concordia** — canonical Commonwealth federal capital on the Zoran continent.

**Connector** — abstraction of an external regional/intercontinental network link.

**Demand** — expressed need/market pressure; not guaranteed construction.

**Effective capacity** — delivered operational capacity after staffing, utilities, access, condition and other limiting factors.

**Generalised travel cost** — route choice value combining time, money, transfers, reliability and comfort.

**HLOD** — hierarchical level-of-detail proxy replacing many distant rendered objects with a cheaper representation.

**Info view** — map overlay/panel revealing a simulation layer.

**Land value** — spatial willingness-to-pay/desirability signal; not synonymous with affordability.

**Mandate** — abstraction of civic trust, cooperation and authority needed for disruptive decisions.

**Nominal capacity** — theoretical design capacity before operational limiting factors.

**Parcel** — developable property/lot inside a block.

**Procedural grammar** — authored rules/modules used to generate varied buildings or urban form.

**Primitive Visual Grammar** — the initial intentional render system that assembles game visuals from a small instanced vocabulary of simple 3D shapes and shared palette materials.

**Visual archetype ID** — stable presentation-facing ID that maps one simulation/content identity to a primitive or higher-fidelity render representation without changing authoritative state.

**Representation LOD** — visual fidelity/representation of an entity at a given importance/distance.

**Right of way** — corridor carrying streets, paths, transit and possibly utilities.

**Simulation LOD** — level/frequency of authoritative simulation detail.

**Stable ID** — persistent identifier not dependent on runtime memory/array position.

**Universe import** — build/content pipeline bringing canonical records into the game while preserving IDs.

## Requirement prefixes

- KCB-CROSS — cross-system
- KCB-VIS — vision/pillars
- KCB-CAN — canon
- KCB-ROLE / KCB-MODE — player role/modes
- KCB-MAP — world/spatial
- KCB-TIME — simulation time
- KCB-GROW — development
- KCB-ROAD — roads
- KCB-TRANS — transit
- KCB-TRAF — traffic/routing
- KCB-POP — population
- KCB-WORK — jobs/education
- KCB-HOUSE — housing
- KCB-ECO — economy
- KCB-IND — industry
- KCB-SERV — services
- KCB-UTIL — utilities
- KCB-RES — resilience
- KCB-GOV — governance
- KCB-ENV — environment
- KCB-CULT — culture
- KCB-TECH — technology/AI
- KCB-BLD — buildings
- KCB-ART — art/rendering
- KCB-INP — input/camera
- KCB-UI — UX
- KCB-AUD — audio
- KCB-EVT — events
- KCB-PROG — progression
- KCB-DATA — save/data/modding
- KCB-ARCH — architecture/performance
- KCB-ACC / KCB-LOC — accessibility/localisation
- KCB-QA — testing/balance
- KCB-CONT — content/assets
- KCB-HAND — handoff/implementation planning
- KCB-RSCH — research provenance

## Traceability rule

KCB-IDX-001 — Requirement IDs are stable once implementation planning begins.

KCB-IDX-002 — If a requirement is superseded, preserve the old ID in a deprecation note and create a new requirement if semantics materially change.

KCB-IDX-003 — Implementation issues/commits/tests SHOULD cite relevant IDs.

## Global non-negotiable requirement set

The implementation plan must explicitly cover at minimum:

- KCB-CROSS-001 explainable failures
- KCB-CROSS-005 LOD consistency
- KCB-CAN-001 canon precedence
- KCB-CAN-070 through 075 AI constraints
- KCB-MAP-001 stable atlas coordinates
- KCB-TIME-001 rendering independent simulation
- KCB-TIME-040 deterministic seed
- KCB-TRAF-100 LOD conserved flows
- KCB-POP-090 population conservation
- KCB-ECO-001 traceable money/resources
- KCB-UTIL-003 traceable utility failures
- KCB-GOV-060 no species discrimination mechanics
- KCB-ART-220 quality tiers preserve simulation
- KCB-DATA-030 versioned saves
- KCB-ARCH-001 simulation/render separation
- KCB-QA-050 long-run soak
- KCB-HAND-030 requirement ownership

## Document ownership

README.md is the catalogue and cross-system entry point. Individual subsystem documents own their requirement prefixes. Cross-document changes should update links and acceptance criteria wherever necessary.

## Future machine-readable index

During implementation planning, generate a machine-readable requirement registry from Markdown or maintain a parallel derived index. That index must be generated or validated from these documents rather than becoming a second manually authored source of truth.
