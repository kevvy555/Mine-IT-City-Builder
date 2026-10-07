# 01 — Technology and Android Platform

## Engine decision

Use **Unity 6.3 LTS**.

Reasons:

- mature Android export/toolchain;
- C# is suitable for both domain modelling and performance-oriented code;
- Entities/DOTS supports compact high-count simulation;
- Burst and Jobs provide controlled parallel execution;
- Entities Graphics supports instanced rendering and LOD;
- URP gives a scalable mobile render pipeline;
- Unity Input System and UI Toolkit cover touch-first interaction;
- GameCI/GitHub Actions can build and test without a permanently running local Unity editor.

This is a locked v1 decision. Changing engine requires a specification and implementation-plan revision.

## Package policy

The Unity project manifest must pin exact package versions. The initial bootstrap should use the latest **released, non-experimental** versions compatible with Unity 6.3 LTS for:

- Entities/DOTS;
- Entities Graphics;
- Burst;
- Collections;
- Mathematics;
- Input System;
- Addressables;
- URP.

At planning time, Unity documents released Entities 1.4.x and Entities Graphics 1.4.x for Unity 6. Exact patch versions must be resolved and pinned by the CI bootstrap PR rather than floating to “latest.”

Rules:

- no package wildcard versions;
- no preview/experimental package in the production dependency set without an ADR and proof;
- package upgrades happen in dedicated branches;
- upgrades run deterministic, save, benchmark and Android build suites;
- a package upgrade cannot silently change simulation behaviour.

## Android baseline

### OS and ABI

- minimum OS: Android 10 / API 29;
- target API: 36;
- primary ABI: ARM64-v8a;
- build backend: IL2CPP;
- landscape orientation locked for gameplay.

x86/x86_64 is allowed only for editor/emulator tests when useful, not required for production packages.

### Graphics APIs

Order:

1. Vulkan;
2. OpenGL ES fallback if device compatibility requires it.

Graphics API selection must be tested on the reference device set. Any renderer feature that fails on the fallback path must have an intentional reduced representation.

### Memory classes

Plan against:

- minimum supported: 6 GB system RAM;
- recommended: 8 GB+;
- low-memory mode lowers render/content residency, not authoritative simulation rules.

The game must monitor Unity/Android memory pressure and unload regenerable visual resources aggressively.

## Rendering stack

- URP;
- forward or forward+ only if profiling proves appropriate;
- one primary directional sun;
- limited additional realtime lights;
- shared palette materials;
- GPU instancing / Entities Graphics;
- camera-relative LOD;
- chunk-level HLOD/proxy strategy;
- restrained post effects;
- no HDRP dependencies.

## Simulation stack

High-count authoritative simulation uses Entities/DOTS and Burst-compatible systems.

Allowed conventional Unity objects:

- bootstrap scene;
- camera rig;
- UI Toolkit host;
- audio listeners;
- platform/lifecycle services;
- editor tooling;
- presentation bridge where ECS rendering is not appropriate.

Prohibited as the primary representation for high-count simulation:

- one MonoBehaviour per citizen;
- one GameObject per offscreen trip;
- Rigidbody traffic;
- NavMeshAgent population;
- per-building Update loops;
- per-entity managed allocations in hot paths.

## Physics

Unity Physics may be used selectively for:

- touch selection ray tests;
- construction placement queries;
- occasional local interaction;
- editor/debug geometry checks.

It must not be the authoritative movement engine for city traffic or population flow.

## UI technology

Use UI Toolkit runtime UI.

Architecture:

- simulation publishes read-only view models/snapshots;
- UI sends commands/intents;
- UI never directly mutates ECS components;
- long lists use virtualization;
- charts use sampled/aggregated history buffers;
- overlays use render data generated from simulation state.

## Input

Use the Unity Input System.

V1 input profiles:

- Android touch: primary;
- mouse: editor/tablet/accessory support;
- keyboard: shortcuts/search/camera where available;
- controller: architecture-ready and implemented before release if acceptance tests require it.

Core touch gestures:

- one-finger select/pan contextually;
- two-finger pan;
- pinch zoom;
- two-finger rotate;
- optional tilt gesture;
- long press for contextual information;
- construction handles sized for touch.

Every drag operation requires a non-drag alternative where accessibility requires it.

## Asset/content loading

Use Addressables for game-owned content and optional high-fidelity visual packs.

Do not use Addressables as the canonical identity database. Simulation references stable content IDs; the presentation/content registry resolves IDs to Addressable keys.

Primitive fallback assets are built into the base package so a missing optional art pack cannot make a save unloadable.

## Development tooling

Required:

- Unity editor project;
- .NET-compatible generator/validator tooling where useful;
- GitHub Actions;
- GameCI for Unity test/build execution;
- deterministic headless simulation harness;
- Android APK/AAB signing via GitHub Secrets;
- static Markdown/JSON validation for specification/requirements.

## Technology proof tasks

- IMP-FND-001 — Create Unity 6.3 LTS project.
- IMP-FND-002 — Pin package manifest.
- IMP-FND-003 — Configure Android API/IL2CPP/ARM64.
- IMP-FND-004 — Configure URP and Vulkan/OpenGL ES order.
- IMP-FND-005 — Add Entities/Burst/Jobs smoke system.
- IMP-FND-006 — Render 10,000 primitive entities in editor benchmark scene.
- IMP-FND-007 — Render 10,000 primitive entities on Android build.
- IMP-FND-008 — Verify UI Toolkit touch shell.
- IMP-FND-009 — Verify app pause/resume.
- IMP-FND-010 — Record package/version lock in build metadata.

## Exit criteria

Technology foundation is accepted only when GitHub Actions can produce an installable APK and the APK demonstrates:

- ECS system executing;
- Burst active in non-development build;
- primitive instanced geometry;
- touch camera;
- version/build metadata;
- pause/resume;
- no missing shader/material errors on Vulkan reference device.
