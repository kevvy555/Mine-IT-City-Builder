# 07 — Canon and Content Validation

## Purpose

Verify the game consumes MineIT Universe canon correctly and does not introduce contradictory local canon.

## Source precedence

QA uses the canon precedence defined by repository rules and MineIT-Universe:

1. Expanded Backstory/Lore Bible;
2. Materials and Substances;
3. World Surface;
4. Scenario extension where applicable;
5. structured Universe data.

A structured record that conflicts with higher-precedence lore is a source issue to resolve, not permission for City Builder to invent a third answer.

## Build-time canon checks

QA-CAN-001 — `universe.lock.json` contains exact accessible commit.

QA-CAN-002 — build metadata reports exact Universe commit.

QA-CAN-003 — required canonical IDs exist.

QA-CAN-004 — imported IDs preserved unchanged.

QA-CAN-005 — duplicate/missing references fail CI.

QA-CAN-006 — canonical atlas tile coordinates unique/stable.

QA-CAN-007 — source atlas changes produce impact report rather than silently rewriting saves.

## Concordia identity review

For canonical tile content verify:

- district identity;
- major axes;
- green/blue infrastructure;
- landmark intent;
- mature/prosperous city character;
- Commonwealth visual language.

Visual QA compares game capture to canonical atlas semantically, not pixel-for-pixel.

QA-CAN-020.

## Species/culture safeguards

QA-CAN-030 — No profession/intelligence/productivity class by species.

QA-CAN-031 — Mixed ancestry representation permitted/normal.

QA-CAN-032 — culture/history influences identity/content without mechanical stereotyping.

Search/content validators flag suspicious direct mappings such as:

```text
speciesId -> productivityMultiplier
speciesId -> allowedJob
speciesId -> intelligence
```

Human review covers nuanced content.

## AI safeguards

QA-CAN-040 — In-world AI systems remain bounded/auditable.

QA-CAN-041 — No generic policy/event grants AI sovereign civic authority.

QA-CAN-042 — critical decisions retain accountable civic/biological authority where canon requires.

## Technology

QA-CAN-050 — Year 5300 starts technologically mature.

QA-CAN-051 — progression is modernisation/specialisation, not arbitrary primitive unlock ladder.

QA-CAN-052 — Veyrite is not treated as generic magical fuel/material outside canon.

## Canon vs gameplay text

QA-CAN-060 — encyclopedia distinguishes Lore from Mechanics.

QA-CAN-061 — tuning values are never presented as Universe historical fact unless canonical.

QA-CAN-062 — game-facing title “Concordia Metropolitan Steward” is clearly a gameplay abstraction.

## Content integrity

Validate every game content record:

- stable ID;
- localisation key;
- referenced content IDs;
- visual archetype;
- allowed ranges;
- asset availability/fallback;
- schema version.

QA-CAN-070.

## Art replacement

QA-CAN-080 — PVG and HQ art for same archetype preserve identity/state.

QA-CAN-081 — HQ asset cannot encode gameplay state unavailable to PVG representation when that state is required for readability.

## Manual canon sign-off

Before release:

- canonical tile captures reviewed;
- major lore/mechanics encyclopedia entries reviewed;
- scenario premise reviewed;
- technology/policy names reviewed;
- content changes against Universe lock diff reviewed.

A canon failure can block release even if code tests are green.
