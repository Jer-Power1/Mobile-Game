# Arcane Survivor — Development Journal

## Project Overview

Arcane Survivor is my arena-survivor roguelite project for Mobile Game Development. My goal for the early development period was to establish a reliable core gameplay loop and Android build pipeline before spending significant time on art and polish.

I chose to use simple geometric placeholder graphics during this stage. This allowed me to concentrate on movement, combat, levelling, UI, Android controls and device deployment. Visual polish is part of my cuts list, so I considered a functioning mobile vertical slice more important for CA1.

---

## 1 October 2026 — Core Gameplay

I set up and maintained the Unity project using Git and GitHub for version control.

I began the gameplay implementation with player movement, enemy movement and enemy spawning. I then added automatic attacks, projectiles and enemy health so that there was a basic playable combat loop.

Defeated enemies were then made to drop XP. The player can collect this XP and level up, which provided the foundation for the upgrade system.

### Input System

The project uses Unity's new Input System rather than the legacy input API.

During development I encountered an issue when attempting to use the older `Input.GetAxisRaw` approach. This was incompatible with the project's selected input configuration. I changed the player movement implementation to use the new Input System for keyboard controls during Editor testing.

This taught me that the input implementation needs to match the input system configured in the Unity project. It also encouraged me to separate the player's movement behaviour from the particular input source, which later made it easier to add the mobile virtual joystick.

### Scope Decision

I deliberately retained placeholder geometric sprites rather than spending time producing final art.

At this stage, the important requirement was to prove that the gameplay loop and Android pipeline worked. Additional visual effects and cosmetic polish are already identified as possible cuts in my MDA, so this was consistent with my scope lock rather than an unplanned reduction in scope.

---

## 2 October 2026 — Weapons, XP and Upgrades

I expanded the combat system to include three weapon systems:

- Magic Bolt
- Arcane Orbit
- Magic Nova

I also implemented passive upgrades affecting damage, attack speed and movement speed.

When the player gains enough XP, a level-up interface presents three upgrade choices. This creates progression during a run and allows the player's build to become stronger as enemy pressure increases.

I added player health, enemy contact damage, a game-over state and restart/main-menu flow. I also created HUD elements displaying health, XP, player level and elapsed run time.

Enemy spawning increases during gameplay to create progressively greater pressure on the player.

### Upgrade-System Reflection

The current upgrade system successfully provides randomised upgrade choices, but it does not yet implement the true weighted selection described in my MDA.

I decided not to rewrite this immediately before completing the CA1 evidence because the existing system already demonstrates the core level-up and upgrade loop. A weighted system remains an identified improvement for the vertical slice.

This reinforced the importance of distinguishing between features necessary to prove the current assessment requirements and features that can safely be developed afterwards without risking a working build.

---

## 2 October 2026 — Mobile Controls

After the keyboard-controlled gameplay was working, I implemented a virtual joystick for Android.

The joystick provides one-thumb movement and the weapons attack automatically, reducing the number of touch controls required during gameplay.

I retained keyboard controls for testing inside the Unity Editor while allowing joystick input to take priority when it is being used.

### Reflection

Separating movement from the input source made supporting both Editor and mobile testing much easier. I could test gameplay quickly using a keyboard while still providing the intended mobile control scheme on the physical device.

Testing on the phone was also important because a control layout that appears acceptable in the Unity Game view does not necessarily feel or appear the same on a real mobile display.

---

## 2 October 2026 — Safe Area and Mobile UI

I added responsive Canvas scaling and an Android safe-area handler.

The purpose of the safe-area system is to keep important HUD and control elements away from display cut-outs and unsafe screen regions.

I tested the landscape layout on a Samsung Galaxy S23 Ultra. The health and XP bars, level indicator, timer, pause button and virtual joystick remained visible and usable on the physical device.

### Reflection

This showed me why mobile UI should not be designed only for a fixed resolution. Android devices can have different aspect ratios, resolutions and display cut-outs, so UI placement needs to respond to the usable screen area rather than assuming a single screen shape.

---

## 2 October 2026 — Pause, Lifecycle and Scene Flow

I implemented pause and resume functionality and added application pause/focus handling.

The game now contains a main menu and gameplay scene. The player can pause a run, resume it, reach a game-over state, restart the game or return to the main menu.

Handling application focus is particularly important on mobile because gameplay can be interrupted when the user changes applications or the operating system takes focus away from the game.

### Current Limitation

The MDA specifies short haptic feedback for player damage and level-up. Haptic feedback has not yet been completed and remains future vertical-slice work.

---

## 2 October 2026 — Android Release Configuration

For CA1 I configured the project for an Android release build.

The build configuration used:

- Unity 6.6 / 6000.6.0f1
- IL2CPP scripting backend
- ARM64 architecture
- Release C++ configuration
- APK output rather than Android App Bundle
- Package name `com.jer.rogue`
- Version `0.1.0`
- Bundle Version Code `1`
- Minimum Android API Level 26
- Target API Level set to Automatic (highest installed)

The release build was signed using my custom release keystore.

The keystore is stored outside the Git repository so that the signing file and passwords are not accidentally committed.

### Signing Details

- Keystore: `User.keystore`
- Alias: `mygame`
- Created: 2 October 2026
- Valid until: 19 September 2076

I used `keytool` to inspect the existing certificate and verify its validity rather than assuming that the default validity shown by Unity's new-key interface represented the existing key.

### Reflection

The signing process helped me understand that an Android release keystore is not just another project file. The same signing identity needs to be protected and retained for future updates.

It also demonstrated why passwords and the keystore itself should not be stored in a public source-control repository.

---

## 2 October 2026 — Physical Device and ADB Testing

I connected a Samsung Galaxy S23 Ultra to the development PC with USB debugging enabled.

I first confirmed that ADB could detect the device and then installed the release APK using `adb install -r`.

The installation completed successfully and I saved the console output as:

`docs/CA1/install-proof.txt`

I then launched the installed build on the physical device and tested the main menu and gameplay.

I verified that:

- The application launched successfully.
- The game displayed in landscape.
- The virtual joystick controlled the player.
- Automatic combat operated on the device.
- The HUD remained visible.
- Pause and resume worked.
- The main menu worked.
- The game-over/restart flow worked.

I captured screenshots of the main menu, gameplay and pause screen, as well as a screen recording as additional testing evidence.

The primary CA1 gameplay screenshot is stored as:

`docs/CA1/device-screenshot.png`

### Reflection

Running the project on a physical Android device was an important step because successful Editor testing does not prove that the Android build, signing configuration, touch input and mobile UI all work together.

ADB also gave me a repeatable way to install a particular APK and record evidence of the installation rather than relying only on manually transferring the application to the phone.

---

## CA1 Publication Awareness

For CA1 I also prepared publication-awareness documentation without uploading the game to a store.

This includes:

- A store-asset checklist.
- A screenshot plan.
- Short and long store-description drafts.
- A Data Use and Privacy Statement.
- Android signing and build documentation.

The privacy statement is based on the current student-authored gameplay behaviour. The game does not intentionally implement accounts, advertising, in-app purchases, multiplayer, location-based gameplay or network-dependent gameplay features.

Before a real store publication, I would still inspect the final Android package and enabled Unity services to ensure that any generated permissions, analytics, diagnostics or network behaviour were correctly represented in the Google Play Data safety form.

### Reflection

This made it clear that publication readiness involves more than producing an APK. Store metadata, screenshots, signing, versioning, privacy information and accurate descriptions of runtime behaviour are all part of preparing a mobile application for distribution.

---

## Current Limitations and Next Steps

The CA1 build demonstrates the Android pipeline and the core gameplay loop, but several items from the wider MDA remain future work:

- Convert random upgrade selection to true weighted upgrade selection.
- Implement the intended 10-minute run completion condition.
- Add optional haptic feedback for player damage and level-up.
- Introduce object pooling for enemies, projectiles and XP drops.
- Continue balancing enemy progression and upgrades.
- Improve visual and audio presentation.
- Verify final Android permissions and Unity services before any real store Data safety declaration.

These items have not been added immediately because maintaining a stable, signed and tested CA1 build takes priority over expanding the project immediately before submission.

## Overall Reflection

The main lesson from this stage of the project was the importance of developing and testing the complete mobile pipeline early.

Initially, it would have been easy to concentrate mainly on adding more weapons, enemies and visual effects. However, CA1 required the project to work as an actual Android application rather than only inside the Unity Editor.

Implementing the virtual joystick, safe-area handling, lifecycle behaviour, release signing and ADB deployment exposed mobile-specific requirements that would not have been as obvious from Editor-only development.

The project now has a working foundation that I can continue developing after the CA1 submission. My next priority is to improve the systems already identified in the MDA while keeping the scope controlled rather than adding unnecessary features.