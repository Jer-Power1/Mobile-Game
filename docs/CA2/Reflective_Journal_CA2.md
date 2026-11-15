# Arcane Survivor — CA2 Reflective Development Journal

**Module:** Mobile Game Development (A12581)  
**Project:** Arcane Survivor  
**Engine/platform:** Unity 6.6 / Android  
**Student:** [Insert student name or number]  
**Submission date:** 15 November 2026

## Weekly development record (Weeks 5–9)

- **Week 5 — Playable foundation:** I developed the basic arena-survival flow: the knight moves around a top-down arena, enemies chase it, automatic attacks damage enemies and a run can end in defeat. The main learning was that a collection of working scripts is not yet a complete playable experience. The weekly lab emphasised a start-to-finish vertical slice and responsive feedback (SETU, 2026a).
- **Week 6 — Progression and rewards:** I connected enemy defeats to XP gem drops and player experience. Collecting enough XP opens a choice of upgrades affecting weapons or passive abilities. I chose XP-based progression because it directly rewards the core combat loop without adding a separate shop or real-money economy. The Week 6 labs framed the importance of a visible earn-and-upgrade cycle and local telemetry (SETU, 2026b).
- **Week 7 — Controls and combat feel:** I worked on character and enemy animations, directional projectile visuals and mobile-friendly control. I also tested how UI panels interacted with the floating joystick. [Confirm which of these changes took place in Week 7; correct the week if needed.]
- **Week 8 — Device-oriented polish:** I revised the arena presentation and interface, including the grass background, HUD and menus. The priority shifted towards making the game readable and operable on a phone rather than merely playable in the Editor. [Add any genuine APK/device tests or peer feedback from this week, if available.]
- **Week 9 — Complete run and balancing:** I added a timed survival victory condition, a victory panel with replay/menu choices, progressively tighter enemy spawning and separation steering to reduce crowding. I also added local telemetry hooks and began implementing persistent run statistics. [Confirm the dates and final test status of these changes.]

## Description — What I built and why

Arcane Survivor is a top-down, three-minute arena-survival game inspired by the simple movement-and-combat loop of survivor-style games. The player controls a knight who automatically attacks demons, collects XP gems and chooses upgrades as the run progresses. I selected this design because it suits one-thumb Android play: movement is the main continuous input, while attacks happen automatically. The central experience is meant to be understandable quickly but increasingly demanding as enemies accumulate.

The initial version proved that individual systems worked, including movement, enemy pursuit, projectiles, contact damage, XP collection and upgrade cards. However, the complete game needed a defined success state. Previously, the player could lose, but there was not an equally clear way to win. I therefore added a survival countdown and a victory screen reached after three minutes. This gave the level a measurable objective, an ending and a reason to replay. I kept the existing death screen so that success and failure remain distinct outcomes.

## Evaluation — What worked and what did not

The most useful improvement was the floating joystick. A fixed control position made movement more restrictive; allowing a touch across the left gameplay area made the controls feel more suitable for mobile. Introducing it also caused a serious interface problem: the invisible joystick touch area captured input intended for the pause button and upgrade cards. My first attempt suppressed joystick movement while menus were open, but that did not resolve the actual UI raycast conflict. The successful fix involved correcting UI sibling order and preventing the joystick touch area from blocking raycasts while a menu was active. I retested joystick movement, pause/resume and upgrade selection together, and all three worked. This taught me to distinguish between a script ignoring an input and a UI element intercepting it.

Enemy crowding was a second important problem. Demons initially chased the player's exact position, so many opponents could pile into the same space. The result was hectic rather than strategically challenging. I introduced separation steering, reduced physical enemy-to-enemy collisions and imposed stage-based limits on how many enemies may be active. The cap increases through the run, preserving escalation while controlling density. During an Editor playtest, the revised behaviour was better. I still need phone playtesting to judge the final balance and frame-time effect.

Visual polish also improved readability. Animated knight and demon sprites made direction and movement easier to understand, while the blue Magic Bolt and XP gems became clearer combat rewards. However, polish exposed UI layout weaknesses: the first victory panel had overlapping text and buttons. Repositioning these elements and testing the replay/menu controls was more valuable than adding further decoration.

## Analysis — Why the changes mattered

The biggest design lesson was that difficulty is not the same as enemy count. Increasing spawn frequency alone raises pressure, but it can also make attacks, enemies and pickups hard to distinguish. Capping the population, adding separation and increasing difficulty in stages are more controlled ways to sustain tension. They also create potential performance benefits by limiting the number of active agents, although I cannot claim a measured improvement until I compare actual profiler captures.

I also learned that mobile interaction must be tested as a whole system. The joystick, pause button and modal upgrade panel each behaved reasonably alone, but failed when combined because UI raycast priority was wrong. Fixing this required inspecting the Canvas hierarchy, pointer events and raycast targets, not simply changing the movement code. This is directly relevant to the rubric's emphasis on readable touch interfaces, pause/resume and robust lifecycle behaviour.

The progression loop uses XP rather than currency. Each gem adds experience; a level-up subtracts the required amount, carries any remainder forward and increases the next threshold. The upgrade choice then changes the player's weapons or statistics. To make this observable, I added local telemetry events for session start, run start, level-up, upgrade selection and win/loss. The `upgrade_purchased` name follows the lab vocabulary, but the event represents a free XP-based selection, not a monetary purchase. File logging and the full sequence of events still need explicit verification in a complete test run.

I began a lightweight save system with `PlayerPrefs` for best survival time, total attempts, victories and last result. I deliberately avoided saving every moving enemy and projectile because each run lasts only three minutes, and restoring an exact mid-combat state would add complexity and risks. The correct claim for submission is persistent run statistics, not mid-run resume. [Add the result of an actual Editor restart and Android relaunch test here.]

**Performance evidence to insert after device testing:** On [device / Android version], the busy-scene average frame time was **[___ ms]**, the 99th percentile was **[___ ms]**, CPU/GPU times were **[___ / ___ ms]**, GC allocations were **[___ B/frame]**, peak memory was **[___ MB]**, and draw calls were **[___]**. A ten-minute test changed battery temperature from **[___°C]** to **[___°C]**, with **[throttling observation]**. The bottleneck was **[specific observed cause]**; changing **[specific fix]** changed **[metric]** from **[before]** to **[after]**. I also checked for side effects: **[enemy behaviour / visuals / controls]**. Do not replace this paragraph with estimates: the Week 9 rubric explicitly asks for measured before-and-after evidence (SETU, 2026c).

## Action — What I would do next

Before CA2 submission, my priority is to finish a full Android run, verify that every menu and victory/defeat path works, confirm the local telemetry events, measure performance on-device and record the required gameplay video. I also need to ensure the release APK uses IL2CPP and ARM64, and that the README, Known Issues and tagged GitHub release match the submitted build. If profiling shows heavy spawning costs, the next technical improvement is object pooling for enemies and projectiles, followed by another measurement to establish whether the change helps. I should not claim pooling or `Awaitable` use until those systems are actually implemented and tested.

For CA3, I would prioritise an evidence-led optimisation pass, a stronger tutorial prompt and additional playtesting with people unfamiliar with the controls. I would gather feedback on whether the difficulty becomes unfair, whether upgrades are understandable and whether the game remains smooth over a longer session. This project has shown me that a polished vertical slice is not simply more content: it is a short, coherent experience with reliable controls, observable progression, meaningful feedback and verifiable performance.

## References

SETU (2026a) *Mobile Game Development: Week 5, Lab B — Greybox lane and async loading that never hangs*. Module lab sheet.

SETU (2026b) *Mobile Game Development: Week 6, Lab B — Telemetry hooks for balancing and Async Pack 1*. Module lab sheet.

SETU (2026c) *Mobile Game Development: Week 9, Lab B — CA2 showcase and the final sprint*. Module lab sheet.

## AI assistance declaration

ChatGPT was used to help draft and revise Unity C# scripts, troubleshoot joystick UI behaviour, and organise this journal. I reviewed the suggested changes and tested relevant gameplay in Unity. Any features, measurements or verification not personally completed are identified above as pending rather than presented as proven results.
