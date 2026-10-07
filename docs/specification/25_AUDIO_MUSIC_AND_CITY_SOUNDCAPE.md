# 25 — Audio, Music and City Soundscape

## Audio goals

Audio makes the city feel inhabited and provides information. It should change with location, time, weather, density and simulation state.

KCB-AUD-001 — Audio MUST have scalable source management; the engine must not create an expensive independent emitter for every visible object.

## Audio layers

1. global ambience;
2. district ambience;
3. local building/service loops;
4. traffic/transit;
5. crowds;
6. weather;
7. events/emergencies;
8. UI;
9. music/radio.

## Global ambience

Base layers depend on:
- time of day;
- weather;
- season;
- city density.

KCB-AUD-010 — Global layers crossfade smoothly.

## District ambience

Examples:
- Federal Forum: water, foot traffic, restrained civic activity;
- Exchange Spine: denser movement, transit, commercial hum;
- Innovation Gardens: quieter landscape, research campus ambience;
- university: pedestrian/student activity;
- transit district: arrivals, guideway motion, concourse activity;
- residential garden: local activity, water/vegetation;
- peri-urban: wind, vegetation, agriculture/water.

KCB-AUD-020 — District audio is driven by actual district/use mix, not only a hard-coded district name.

## Building audio

KCB-AUD-030 — Major service/industrial buildings may have state-aware loops/spots.
KCB-AUD-031 — Audio can reflect operating load, construction, maintenance or closure.
KCB-AUD-032 — Large building complexes may use camera-relative/area sources rather than one fixed point.

## Traffic/transit audio

KCB-AUD-040 — Traffic loudness responds to visible/nearby volume and vehicle mix.
KCB-AUD-041 — Transit modes have distinctive but non-intrusive signatures.
KCB-AUD-042 — Distant traffic becomes aggregate bed rather than thousands of emitters.

## Crowd audio

KCB-AUD-050 — Crowd ambience scales with simulated local activity.
KCB-AUD-051 — Individual voice lines are optional flavour and must not create repetitive chatter.

## Weather

KCB-AUD-060 — Rain/wind/storm layers correspond to actual weather.
KCB-AUD-061 — Indoor-like/covered camera contexts MAY filter weather where supported.

## Alerts and sonification

Audio cues can signal:
- construction completion;
- severe service degradation;
- emergency;
- budget threshold;
- transit disruption.

KCB-AUD-070 — Critical cues have visual equivalents.
KCB-AUD-071 — No essential information is audio-only.

## Music

Desired direction:
- optimistic advanced civilisation;
- contemplative city planning;
- subtle cultural texture;
- avoid generic cyberpunk synth clichés as sole identity.

KCB-AUD-080 — Music intensity MAY adapt to build/inspection/emergency state but should not constantly dramatise ordinary simulation.

## Radio / in-world media

Optional:
- civic/news channels;
- cultural programmes;
- music channels;
- transport/public-service bulletins.

KCB-AUD-090 — Any in-world news referencing simulation state must derive from real game events.

## Audio LOD

Distance bands:
- close: individual local source;
- medium: grouped source by building/block;
- far: district aggregate;
- offscreen: none unless globally relevant.

KCB-AUD-100 — Audio source aggregation must avoid obvious double-counting when crossing LOD bands.

## Settings

Separate controls:
- master;
- music;
- ambience;
- traffic;
- effects;
- UI;
- voice/narration.

KCB-AUD-110 — Dynamic range options SHOULD include at least normal/night or compressed mode.
KCB-AUD-111 — Subtitles/captions required for meaningful speech.

## Acceptance criteria

- Moving camera from park to transit hub changes soundscape smoothly.
- Heavy traffic is audible without one emitter per vehicle.
- Rain changes both ambience and local surface sound.
- Muting music does not remove gameplay cues.
- Critical audio cue has a visual counterpart.
