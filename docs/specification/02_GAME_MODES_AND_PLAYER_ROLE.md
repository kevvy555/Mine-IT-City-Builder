# 02 — Game Modes and Player Role

## Default player role

The player is the **Concordia Metropolitan Steward** working through a planning and executive administration. The title is a game-facing abstraction and is not yet Universe canon.

The player controls:
- strategic land use and development permissions;
- public rights of way;
- infrastructure investment;
- public-service budgets and major facilities;
- taxation/fees within delegated powers;
- district policy;
- capital projects;
- emergency priorities;
- public-space and heritage decisions;
- research/modernisation programmes;
- municipal procurement and contracts.

The player does not directly control:
- where every household lives;
- every person’s job;
- every company’s daily decisions;
- personal travel choices;
- every building design;
- Commonwealth-level law;
- private property without legal process;
- bounded AI objectives outside authorised civic domains.

KCB-ROLE-001 — Player powers MUST map to an in-world institutional authority or be marked as sandbox convenience.

KCB-ROLE-002 — The UI SHOULD distinguish “command,” “policy,” “permission,” “funding” and “information” actions.

## Mode A — Concordia Stewardship

This is the intended primary game.

Starting state:
- an already functioning canonical central city;
- Federal Forum and core atlas districts present;
- existing transport and utility networks;
- existing residents, jobs, institutions and budget;
- several visible pressures chosen by scenario seed, such as housing demand, corridor congestion, ageing utility assets or environmental restoration.

KCB-MODE-010 — The starting city MUST not be broken purely to manufacture tutorial tasks.

KCB-MODE-011 — Tutorial objectives SHOULD ask the player to improve a real system while teaching diagnosis.

## Mode B — Concordia Expansion

Starts at the metropolitan edge with the central canonical city simulated as an external regional system. The player develops new districts that must connect to the existing metropolis.

Purpose:
- lower complexity entry point;
- more empty land for creative building;
- retains canonical context.

KCB-MODE-020 — External Concordia demand, commuters, freight, service agreements and fiscal transfers MUST still affect play.

## Mode C — Historical / Scenario Challenges

Curated scenarios may use fixed goals and constraints:
- transit modernisation;
- post-disaster recovery;
- housing affordability;
- polluted industrial corridor regeneration;
- heritage preservation during rapid growth;
- major intercontinental event;
- Veyrite-era logistics boom;
- grid resilience test.

KCB-MODE-030 — Scenario scripts MUST perturb the same underlying systems rather than substituting bespoke fake mechanics wherever possible.

KCB-MODE-031 — Scenario scoring SHOULD be multi-dimensional rather than a single “population reached” target.

## Mode D — Creative Sandbox

Options:
- unlimited funds;
- unlocked technology;
- disable politics;
- disable disasters;
- unlock all land;
- ignore heritage constraints;
- freeze economy;
- fixed weather/time;
- free placement;
- demand override;
- instant build;
- traffic/agent density scaling.

KCB-MODE-040 — Sandbox switches MUST be persisted in the save and visibly mark altered simulation rules.

KCB-MODE-041 — Sandbox MUST not write altered outcomes back into canonical Universe files.

## Mode E — City Laboratory

An expert testing mode aimed at simulation enthusiasts and developers.

Capabilities:
- deterministic seed selection;
- pause and single-step;
- spawn controlled demand;
- freeze subsystems;
- view entity/path/network diagnostics;
- compare policy A/B runs;
- export time-series metrics;
- replay simulation events.

KCB-MODE-050 — Laboratory mode SHOULD share diagnostics with automated balancing/testing infrastructure.

## Start flow

1. Choose mode/scenario.
2. Review map/canon summary.
3. Choose difficulty profile.
4. Optional advanced rules.
5. Choose deterministic seed.
6. Review starting city conditions and objectives.
7. Begin at paused speed with a short metropolitan briefing.

KCB-ROLE-010 — A new game MUST explain what authority the player has and why.

KCB-ROLE-011 — The opening SHOULD highlight three city strengths and three current pressures rather than presenting only negative alerts.

## Difficulty

Difficulty is composed from independent dimensions:
- fiscal support;
- construction cost;
- maintenance cost;
- demand volatility;
- citizen tolerance;
- service resilience;
- incident frequency;
- political friction;
- environmental sensitivity;
- external market volatility;
- simulation assistance/explainability.

KCB-ROLE-020 — Difficulty MUST NOT secretly make traffic agents “stupider” or invalidate physical rules.

KCB-ROLE-021 — Difficulty presets are named collections of transparent modifiers.

## Win and loss philosophy

The city should rarely hard-fail.

Possible severe states:
- fiscal insolvency;
- institutional intervention;
- sustained infrastructure collapse;
- mass out-migration;
- public mandate loss.

KCB-ROLE-030 — Severe failure SHOULD create recovery conditions, emergency financing, elections/mandate reduction or scenario failure rather than deleting the city.

KCB-ROLE-031 — Endless continuation MUST be available after campaign goals unless the scenario explicitly forbids it.

## Session length

The design must support:
- 10-minute diagnostic/improvement sessions;
- 1–3 hour building sessions;
- long-running cities over many real-world months.

KCB-ROLE-040 — Autosave and simulation pausing MUST make short sessions safe.

KCB-ROLE-041 — On load, a concise “since last session” city briefing SHOULD identify current trends and unresolved alerts without implying background simulation occurred while the game was closed unless that feature is explicitly enabled.
