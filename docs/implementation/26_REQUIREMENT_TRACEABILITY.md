# 26 — Requirement Traceability

## Strategy

The specification remains the single manually authored source of KCB requirement IDs.

Implementation ownership is defined by requirement **prefix** and validated by tooling. A derived row-per-requirement registry will be generated from Markdown; it must never become a second manually maintained specification.

This satisfies the requirement that every normative KCB item has an owner while avoiding duplicated requirement prose.

## Ownership table

| Requirement prefix | Primary implementation owner | Primary verification |
|---|---|---|
| KCB-CROSS | architecture + relevant domain | integration/invariant |
| KCB-VIS | roadmap/product acceptance | milestone review |
| KCB-CAN | Universe import/content/governance | canon validator |
| KCB-ROLE | governance/UI | scenario/UI test |
| KCB-MODE | app/scenario/save | integration |
| KCB-MAP | world/chunks/development | spatial tests |
| KCB-TIME | simulation kernel | determinism |
| KCB-GROW | development/economy | integration |
| KCB-ROAD | roads/construction | geometry/network |
| KCB-TRANS | mobility/transit | route/capacity |
| KCB-TRAF | mobility | routing/LOD |
| KCB-POP | population | conservation |
| KCB-WORK | population/workforce | matching |
| KCB-HOUSE | economy/housing | housing fixture |
| KCB-ECO | economy | ledger/invariant |
| KCB-IND | economy/freight | inventory/conservation |
| KCB-SERV | civic services | capacity/catchment |
| KCB-UTIL | utilities | network/cause trace |
| KCB-RES | resilience/environment | incident fixture |
| KCB-GOV | governance | policy/invariant |
| KCB-ENV | environment | field/exposure fixture |
| KCB-CULT | governance/content/history | canon/content tests |
| KCB-TECH | governance/technology | content/policy tests |
| KCB-BLD | development/PVG | building/geometry |
| KCB-ART | PVG/rendering | Android visual/perf |
| KCB-INP | camera/input/UI shell | touch/device |
| KCB-UI | UX/explainability | UI/integration |
| KCB-AUD | audio/accessibility | device/accessibility |
| KCB-EVT | governance/history | deterministic event tests |
| KCB-PROG | scenario/progression | scenario tests |
| KCB-DATA | persistence/content | save/migration |
| KCB-ARCH | architecture/performance | dependency/benchmark |
| KCB-ACC | accessibility/UI | accessibility QA |
| KCB-LOC | localisation | localisation validator |
| KCB-QA | QA/tooling | CI/benchmark |
| KCB-CONT | content/PVG/import | content validator |
| KCB-HAND | implementation plan/gates | plan/gate evidence |
| KCB-IDX | requirement tooling | requirement-index CI |

Every prefix currently present in `docs/specification/` appears above. Any new prefix must fail requirement-index CI until ownership is added.

## Derived registry format

`tools/RequirementIndex` will parse Markdown lines containing a requirement ID and output:

```csv
requirement_id,source_document,source_line,owner_document,owner_epic,verification,status
KCB-MAP-001,...,07_WORLD_ATLAS_CHUNKS_AND_STREAMING.md,IMP-WLD,spatial-test,Planned
...
```

A JSON equivalent is also generated for tooling.

Generated registry is CI artifact or generated documentation; it is not edited by hand.

## Ownership mapping to IMP epics

| Prefix | IMP epic |
|---|---|
| CROSS | IMP-ARC + domain |
| VIS | IMP-VS / IMP-REL |
| CAN | IMP-CAN |
| ROLE/MODE | IMP-GOV / IMP-INP |
| MAP | IMP-WLD |
| TIME | IMP-SIM |
| GROW | IMP-DEV |
| ROAD | IMP-RD |
| TRANS/TRAF | IMP-MOB |
| POP/WORK | IMP-POP |
| HOUSE/ECO/IND | IMP-ECO |
| SERV/UTIL/RES/ENV | IMP-CIV |
| GOV/TECH/EVT/PROG/CULT | IMP-GOV |
| BLD | IMP-DEV / IMP-REN |
| ART/CONT | IMP-REN |
| INP | IMP-INP |
| UI | IMP-UX |
| AUD/ACC/LOC | IMP-ACC |
| DATA | IMP-SAV / IMP-CAN |
| ARCH | IMP-ARC / IMP-PERF |
| QA | IMP-QA |
| HAND | IMP-FND / milestone gates |
| IDX | IMP-QA requirement tooling |

## Explicit global non-negotiables

| Requirement | Owner | Proof |
|---|---|---|
| KCB-CROSS-001 | IMP-UX + all domains | cause-factor fixtures |
| KCB-CROSS-005 | IMP-SIM/IMP-PERF | LOD equivalence |
| KCB-CAN-001 | IMP-CAN | canon import validator |
| KCB-CAN-070..075 | IMP-GOV | AI/content validation |
| KCB-MAP-001 | IMP-WLD | atlas ID roundtrip |
| KCB-TIME-001 | IMP-SIM | FPS independence test |
| KCB-TIME-040 | IMP-SIM | seed replay |
| KCB-TRAF-100 | IMP-MOB | trip/flow conservation |
| KCB-POP-090 | IMP-POP | population ledger |
| KCB-ECO-001 | IMP-ECO | double-entry/source-sink report |
| KCB-UTIL-003 | IMP-CIV/IMP-UX | utility cause trace |
| KCB-GOV-060 | IMP-GOV | content/policy validation |
| KCB-ART-220 | IMP-REN/IMP-PERF | graphics-tier checksum |
| KCB-DATA-030 | IMP-SAV | save fixture v1 |
| KCB-ARCH-001 | IMP-ARC | headless sim test |
| KCB-QA-050 | IMP-QA | 100-year soak |
| KCB-HAND-030 | IMP-QA | requirement-index CI |

## Requirement-index CI

IMP-QA-016 — Implement Markdown requirement parser.

IMP-QA-017 — Fail duplicate IDs.

IMP-QA-018 — Fail unknown prefix.

IMP-QA-019 — Fail requirement without ownership.

IMP-QA-020 — Fail superseded ID without deprecation metadata where required.

IMP-QA-021 — Export exact registry as CI artifact.

IMP-QA-022 — Optionally annotate PR with new/changed requirements and their owners.

## Status rule

At implementation start all derived rows are `Planned`.

A requirement becomes:

- `InProgress` when an active task references it;
- `Implemented` when code/content is merged;
- `Verified` only when required verification passes;
- `Deferred` only with explicit spec/plan decision.

A phase cannot claim complete with owned MUST requirements still only `Implemented` if their verification is required by that phase.
