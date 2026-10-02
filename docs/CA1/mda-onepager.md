# MDA One-Pager — Arcane Survivor

**Module:** Mobile Game Development (A12581)  
**Student:** Jer Power 20106903 
**Project option:** 1 – Arena-Survivor Roguelite  
**Scope locked:** Wed 16 Sep 2026

## One-line Pitch

A fast-paced mobile roguelite where the player survives increasingly difficult enemy waves, automatically attacks nearby enemies, collects XP and chooses upgrades to create powerful builds during each run.

## Aesthetics

- **Power progression:** feeling increasingly powerful as the run continues.
- **Tension:** avoiding large groups of enemies and escaping when surrounded.
- **Satisfaction:** destroying groups of enemies and collecting their XP.
- **Experimentation:** trying different weapons and upgrade combinations between runs.

## Core Mechanics

1. Move using a virtual joystick.
2. Auto-attack enemies with equipped weapons.
3. Collect XP and loot from defeated enemies.
4. Upgrade weapons and abilities through weighted upgrade choices.
5. Survive an increasingly difficult spawn curve.

## Dynamics

- Players kite groups of enemies and search for openings to avoid being surrounded.
- Collecting XP creates a risk/reward decision because players must move towards drops while avoiding enemies.
- Randomised upgrade choices create different character builds each run.
- Increasing enemy density makes positioning more important as the run progresses.
- Stronger weapon combinations gradually change the player from vulnerable to powerful.

## Progression & Content

- **Session length:** 10-minute runs.
- **Vertical slice:** 1 arena, 3 weapons, 3 upgrades, XP/levelling, weighted upgrade choices, enemy spawn director and pause/resume.
- **Later scope:** meta-progression/save-load, additional enemies and weapons, playable characters, bosses, balancing and polish where development time allows.

## Platform Features — Android

- One-thumb virtual joystick; weapons attack automatically.
- Landscape orientation.
- Gameplay UI remains inside Android safe areas/notches.
- Short haptic feedback on player damage and level-up, with an optional toggle.
- Pause/resume and focus-loss lifecycle handling.
- Store/testing-track awareness only; no upload required at this stage.

## Performance Budget

- **Reference device:** Samsung S23 Ultra.
- **Target frame time:** 16.7 ms at 60 fps.
- **High FPS toggle:** No.
- **Memory ceiling:** under 600 MB.
- **Cold start:** under 4 seconds to interactive.
- **APK size:** under 100 MB.
- Enemies, projectiles and XP drops will use object pooling to avoid unnecessary allocations and frame-time spikes.

## Monetisation & Ethics

If published, the game would use a one-time purchase or optional cosmetic content. No loot boxes, pay-to-win upgrades, forced advertisements, energy systems or timers designed to pressure spending.

## Risks & Cuts List

1. Additional weapons, upgrades and enemy types beyond the required vertical slice.
2. Complex meta-progression between runs.
3. Additional visual effects and cosmetic polish.

## Scope Lock

Changes after the scope lock require a note in the development journal explaining what changed and why.

## Reference

Hunicke, LeBlanc and Zubek (2004), *MDA: A Formal Approach to Game Design and Game Research*.
