# 00 — Vision and Design Pillars

## Game promise

Koplin City Builder gives the player stewardship of a living Commonwealth metropolis. The player is not founding civilisation from nothing and is not an absolute ruler. They are directing planning, infrastructure and public investment within a mature legal, economic and social system whose citizens, institutions and companies have agency of their own.

The city should be enjoyable at three simultaneous distances:

1. **Street view:** people, vehicles, parks, deliveries, transit, building activity and small stories make the city feel inhabited.
2. **District view:** traffic, land use, services, employment, public space and local character become legible patterns.
3. **Metropolitan view:** networks, budgets, supply chains, growth, environmental systems and long-term planning become strategic decisions.

## Pillar 1 — A city that behaves, not a spreadsheet painted onto roads

KCB-VIS-001 — Physical access MUST matter. Commuters, service vehicles, cargo, pedestrians and transit cannot teleport between unrelated locations.

KCB-VIS-002 — Capacity MUST have location and time. A hospital across the city is not equivalent to one nearby if congestion or route failure prevents access.

KCB-VIS-003 — Economic actors MUST react to conditions instead of merely filling player-painted zones. Households and organisations evaluate affordability, accessibility, service quality and suitability.

KCB-VIS-004 — The simulation MUST use feedback loops with damping and hysteresis so cities evolve rather than flicker between states every tick.

## Pillar 2 — Deep but explainable

KCB-VIS-010 — The player MUST be able to answer “why?” for unemployment, vacancies, congestion, low service quality, business failure, unaffordable housing, low happiness, utility failure and budget stress.

KCB-VIS-011 — Each major metric MUST expose contributors, trend direction and at least one relevant action.

KCB-VIS-012 — The default UI MUST prioritize exceptions and bottlenecks over constant manual inspection of every entity.

KCB-VIS-013 — Advanced panels MAY expose equations and detailed ledgers, but basic play MUST remain comprehensible without them.

## Pillar 3 — Build Concordia, not “future Earth”

The city must visually and mechanically express Koplin history.

KCB-VIS-020 — Public institutions, rapid transit, gardens, water features and distributed civic power MUST be fundamental to Concordia’s layout.

KCB-VIS-021 — The three peoples MUST be present throughout the city as ordinary citizens with mixed households and careers.

KCB-VIS-022 — Historical memory of conquest, the AI Wars and the Commonwealth Compact SHOULD appear through monuments, civic institutions, policies, events and cultural life without reducing modern politics to species conflict.

KCB-VIS-023 — Advanced technology MUST make infrastructure cleaner, more capable and more integrated, but MUST NOT remove planning trade-offs, maintenance, capacity, logistics or governance.

KCB-VIS-024 — AI MUST operate as bounded civic assistance, scheduling, modelling and automation; no game system may imply an autonomous sovereign AI mayor.

## Pillar 4 — Organic growth with deliberate planning

A mature city should not look like a collection of identical plopped rectangles.

KCB-VIS-030 — Streets and parcels SHOULD support curved and irregular geometry.

KCB-VIS-031 — Development SHOULD form blocks, frontage, courtyards, setbacks, plazas and mixed-use edges based on local context.

KCB-VIS-032 — The player controls strategic land-use permissions and public works while private/cooperative development fills suitable parcels.

KCB-VIS-033 — Key civic, cultural, transport and infrastructure projects are directly placed or commissioned.

KCB-VIS-034 — Blueprint/planning mode MUST allow layouts to be designed before expenditure or construction begins.

## Pillar 5 — Watchability

KCB-VIS-040 — At normal play speed, the city MUST provide continuous visual activity without requiring UI interaction.

KCB-VIS-041 — Buildings SHOULD expose state through lights, occupancy patterns, rooftop equipment, delivery activity, maintenance crews, public-space use and subtle animation.

KCB-VIS-042 — Traffic and pedestrians MUST not exist purely as decoration: visible movement should correspond to simulated trips at the chosen representation level.

KCB-VIS-043 — Time of day, weather and seasons SHOULD alter city activity and sound as well as visuals.

KCB-VIS-044 — Photo/cinematic tools SHOULD be first-class because building an aesthetically satisfying city is a core player reward.

## Pillar 6 — Problems emerge from systems

The game should avoid arbitrary “disaster card” design as its primary challenge.

KCB-VIS-050 — Most ordinary problems MUST emerge from interacting systems: growth overloads a corridor; rents push workers outward; a power maintenance backlog reduces spare capacity; a freight bottleneck starves manufacturers.

KCB-VIS-051 — Scripted events SHOULD perturb existing systems rather than bypass them.

KCB-VIS-052 — The game SHOULD permit multiple valid solutions. Congestion can be addressed through capacity, mode shift, land-use change, pricing, scheduling, remote work or decentralisation.

KCB-VIS-053 — There MUST NOT be a single mandatory “correct” city shape.

## Pillar 7 — Long-lived cities

KCB-VIS-060 — A campaign SHOULD support centuries of simulated history if the player chooses to continue.

KCB-VIS-061 — Buildings and infrastructure MUST support ageing, maintenance, renovation, replacement and heritage designation.

KCB-VIS-062 — District identity SHOULD accumulate from history rather than reset when a policy changes.

KCB-VIS-063 — Save formats and simulation state MUST be versionable from the start.

## Player emotional arc

Early play: understand an inherited district, make targeted improvements and establish trust in the simulation.

Middle play: coordinate interconnected districts, transit, housing, services and economic specialisation while preserving quality of life.

Late play: reshape metropolitan-scale systems, deliver major infrastructure, manage regional flows, preserve heritage and ecology, and decide what Concordia becomes over generations.

Post-goal play: continue indefinitely, focus on beauty, optimisation, historical storytelling, scenario challenges or experimental policy.

## Anti-pillars

The game MUST NOT become:

- a pure traffic junction puzzle;
- a survival colony where basic food and oxygen dominate normal homeworld play;
- a species-class system;
- a cyberpunk dystopia;
- a click-heavy service-building spam game;
- a deterministic production-chain puzzle with no households or urban geography;
- a decorative city painter whose numbers ignore physical networks;
- an omniscient AI-managed city where player governance is irrelevant.

## Success criteria

A successful prototype should make these statements true:

- “I can see why this neighbourhood works.”
- “I can watch people use what I built.”
- “The city looks recognisably Koplin.”
- “I solved the same problem differently in two saves.”
- “The simulation is deep, but I can trace the cause.”
- “Zooming out does not make the city feel dead.”
