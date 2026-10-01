MDA One-Pager – Working Title: Arcane Survivor

Module: Mobile Game Development (A12581) · Student: <name, student number> · Project option: 1 – Arena-Survivor Roguelite · Due: Wed 16 Sep 2026 (Week 2 Lab B)

One-line pitch

A fast-paced mobile roguelite where the player survives increasingly difficult enemy waves, automatically attacks nearby enemies, collects XP and chooses upgrades to create powerful builds during each run.

Aesthetics

•	Power progression: feeling increasingly powerful as the run continues.

•	Tension: avoiding large groups of enemies and escaping when surrounded.

•	Satisfaction: destroying groups of enemies and collecting their XP.

•	Experimentation: trying different weapons and upgrade combinations between runs.

Core mechanics

1\.	Move using a virtual joystick.

2\.	Auto-attack enemies with equipped weapons.

3\.	Collect XP and loot from defeated enemies.

4\.	Upgrade weapons and abilities through weighted upgrade choices.

5\.	Survive an increasingly difficult spawn curve.

Dynamics

•	Players kite groups of enemies and search for openings to avoid being surrounded.

•	Collecting XP creates a risk/reward decision because players must move towards drops while avoiding enemies.

•	Randomised upgrade choices create different character builds each run.

•	Increasing enemy density makes positioning more important as the run progresses.

•	Stronger weapon combinations gradually change the player from vulnerable to powerful.

Progression \& content

•	Session length: 10-minute runs.

•	Content in the vertical slice (by Week 6): 1 arena, 3 weapons, 3 upgrades, XP/levelling, weighted upgrade choices, enemy spawn director and pause/resume.

•	Content by CA3: meta-progression and save/load, additional enemy variety, additional weapons, different playable characters, boss enemies and balancing/polish where development time allows.

Platform features (Android)

•	Touch model: one-thumb virtual joystick; weapons attack automatically.

•	Safe areas and orientation: landscape; gameplay UI remains inside Android safe areas/notches.

•	Haptics: short feedback on player damage and level-up; optional toggle.

•	Lifecycle: pause/resume and focus loss handled from Week 2.

•	Store / testing tracks: awareness only, no uploads.

Performance budget

•	Device: Samsung S23 Ultra

•	Target frame time: 16.7 ms at 60 fps; High FPS toggle: no.

•	Memory ceiling: under 600 MB.

•	Cold start: under 4 seconds to interactive.

•	APK size: under 100 MB.

•	Enemies, projectiles and XP drops will use object pooling to avoid unnecessary allocations and frame-time spikes.

Monetisation \& ethics

If published, the game would use a one-time purchase or optional cosmetic content. No loot boxes, pay-to-win upgrades, forced advertisements, energy systems or timers designed to pressure spending.

Risks \& cuts list

1\.	Additional weapons, upgrades and enemy types beyond the required vertical slice.

2\.	Complex meta-progression between runs.

3\.	Additional visual effects and cosmetic polish.

Scope lock

•	Locked on: Wed 16 Sep 2026.

•	Changes after lock require a note in the development journal explaining what changed and why.

Reference: Hunicke, LeBlanc and Zubek (2004), MDA: A Formal Approach to Game Design and Game Research.





