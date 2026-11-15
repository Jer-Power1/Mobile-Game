# Arcane Survivor

**Arcane Survivor** is a 2D, top-down arena-survival game made in **Unity 6.6** for SETU's Mobile Game Development module. The player controls a staff-wielding knight, survives waves of demons, collects XP gems, and chooses weapon or passive upgrades. The current playable goal is to **survive for three minutes**.

**Repository:** https://github.com/Jer-Power1/Mobile-Game  
**Platform:** Android (sideloaded APK; no store release)

## Gameplay and controls

1. Choose **Play** from the main menu.
2. Move the knight using the **floating virtual joystick**: touch and drag within the left portion of the screen. The joystick appears wherever the drag begins. Keyboard movement (WASD or arrow keys) is also available for Editor testing.
3. Weapons fire automatically. Defeat demons, collect the dropped blue XP gems, and choose one of the offered upgrades when levelling up.
4. Survive until the three-minute countdown ends to reach **Victory**, or lose all health to reach **Game Over**.
5. Use **Pause / Resume** during a run. After a run, use **Play Again** or **Main Menu**, as available.

The weapons and progression systems include **Magic Bolt**, **Arcane Orbit**, **Magic Nova**, and passive upgrades such as Might, Haste, and Swift Boots. The knight and demons use animated sprites. Enemy AI combines chasing and separation, while the spawner limits active enemies and increases pressure in stages to reduce overcrowding.

## Development environment and Android targets

| Item | Configuration |
| --- | --- |
| Unity Editor | **6000.6.0f1 (Unity 6.6)** |
| Build platform | Android |
| Orientation | Landscape |
| Input | Unity Input System (New) |
| Rendering | 2D URP |
| Scripting backend | IL2CPP |
| Target architecture | ARM64 |
| Distribution | APK, with **Build App Bundle** disabled |
| Package name | `com.jer.rogue` |
| Original test device | Samsung Galaxy S23 Ultra |

**Before the CA2 release:** verify the current Version, Bundle Version Code, Minimum API Level, target API and Android graphics settings in Unity Player Settings. The earlier CA1 build used version `0.1.0`, version code `1`, and minimum Android API 26; these historical values must not be mistaken for the final CA2 build settings.

## Build a release APK

1. Open the repository in **Unity Hub** using Unity **6000.6.0f1**, with Android Build Support, SDK/NDK tools and OpenJDK installed.
2. Open **File > Build Profiles** and select/switch to **Android**.
3. Check that the **MainMenu** and **Game** scenes are enabled in the Scene List, with the menu scene first. Check actual scene names before building.
4. In **Player Settings > Other Settings**, confirm **IL2CPP**, **ARM64**, package name `com.jer.rogue`, landscape orientation, and the intended version/version code.
5. In **Publishing Settings**, select the **current CA2 release keystore and its alias**, entering passwords locally. Keep these credentials outside Git.
6. Disable **Development Build**, **Autoconnect Profiler**, and **Build App Bundle (Google Play)** for the submitted release APK.
7. Click **Build** and place the resulting APK in a clearly named `Releases/` or `Builds/` folder. Record its exact filename, size, and SHA-256 hash for the submitted build.
8. Install and launch the APK on an Android phone. Verify it independently of the Unity Editor.

### Signing-key history and installation

The original **CA1 signing keystore** is no longer available for the CA2 workflow, and a **new keystore** has been created. Android generally refuses an in-place update when the installed APK and replacement APK have the same package name but are signed with different certificates. **Back up any data you want to keep before uninstalling the earlier CA1 app**, then install the new CA2 APK. Use the new keystore consistently for later updates and keep a secure backup of its file and passwords.

For USB installation, connect the Android device with USB debugging enabled, then run:

```bash
adb devices
adb install -r "PATH/TO/ACTUAL-CA2.apk"
```

Replace the path with the actual APK filename. If installation reports a signature mismatch, uninstall the older version **only after considering loss of local app data**, then reinstall. Alternatively, open the APK directly on the phone and allow installation from the chosen file source.

## Progress, economy and local telemetry

Defeated demons drop XP gems. XP is accumulated until a level-up threshold is met; the threshold's XP cost is deducted, excess XP is retained, and the player chooses a free weapon or passive upgrade. The initial level-up threshold is 5 XP, increasing by 3 XP for subsequent levels with the current default settings. This is **in-game progression**, not real-money purchasing.

The game has a local telemetry implementation using Unity Console messages prefixed `[telemetry]` and an optional `telemetry.log` stored in `Application.persistentDataPath`. Events include `session_start`, `level_start`, `level_up`, `upgrade_purchased` (the name refers to a **free upgrade selection**, not an IAP), `level_complete`, and `level_fail`. No third-party analytics SDK is required. See `docs/CA2/Economy_Telemetry_Map.md`.

A lightweight `PlayerPrefs` save system is intended to retain **best survival time, runs played, victories and last run result** between launches. It does **not** currently restore an unfinished combat encounter. Save/load behaviour on a physical Android device must be checked and documented before final submission.

## CA2 evidence and documentation

The CA2 rubric requires all of the following in the **Moodle submission ZIP**:

```text
APK/                     Release-signed IL2CPP + ARM64 APK
Video/                   3–5 minute gameplay recording (MP4 or link)
Docs/Performance_Baseline.md
Docs/Economy_Telemetry_Map.md
Docs/Reflective_Journal.md
README.md
KnownIssues.md
LINKS.txt                 GitHub repository URL and exact release tag
```

Additionally, supply an **on-device screenshot** identifying the device, and a **tagged GitHub release**. The gameplay recording should demonstrate boot, menu, a complete run, pause/resume, and quit/relaunch. The performance baseline must contain *measured* phone data (including average and 99th-percentile frame time, CPU/GPU, allocations, memory, draw calls, thermal soak, startup time and one before/after optimisation). Do not replace unmeasured fields with estimates.

CA1 background documents are kept in `docs/CA1/`, including the original installation proof, store-asset checklist, descriptions and privacy statement. The project's earlier development journal is under `docs/dev-journal.md`.

## Known issues and current verification status

See **[KnownIssues.md](KnownIssues.md)** for the full issue list, severity, workarounds and outstanding verification. In particular, enemy/projectile instantiation is not yet pooled, exact mid-run restoration is out of scope, and the Android lifecycle/performance evidence must be verified on device. A visually spinning Arcane Orbit sawblade may also need additional polish.

## AI assistance and attribution

ChatGPT was used as a **development assistant** to help draft, debug and revise Unity C# scripts, propose UI layouts and enemy balancing, and structure technical documentation and reflective writing. The project owner is responsible for reviewing the resulting code, integrating it into Unity, playtesting behaviour and measuring performance. AI assistance does **not** substitute for device test evidence. Any externally sourced art, packages or code should also be credited in the final project documentation as appropriate.

## Security

Never commit `.keystore`, `.jks`, signing passwords or Unity-generated `*_BackUpThisFolder_ButDontShipItWithYourGame/` directories. Store the **new** Android release key securely outside the repository with a backed-up copy.
