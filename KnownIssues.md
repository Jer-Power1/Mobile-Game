# Arcane Survivor — Known Issues and Verification Log (CA2)

**Version:** CA2 candidate (confirm the exact APK version/build before submission)  
**Engine:** Unity 6000.6.0f1  
**Platform:** Android  
**Updated:** 15 November 2026

This file distinguishes **known limitations** from **tests that still need verification**. A pending test is not a confirmed defect, and an Editor test does not by itself establish that Android behaviour has passed.

## Known limitations

| ID | Severity | Item | Reproduction / impact | Current action |
| --- | --- | --- | --- | --- |
| KI-01 | Medium | Old CA1 APK cannot be upgraded using the new signing certificate | Installing a differently signed APK over an existing app with the same package name can produce `INSTALL_FAILED_UPDATE_INCOMPATIBLE`. | Back up any needed local data, uninstall the older app, then install the newly signed CA2 APK. Retain the new keystore for subsequent builds. |
| KI-02 | Low | Arcane Orbit sawblade rotation may not visually animate as intended | The orbit weapon was usable, but a separate spinning sawblade visual had not been confirmed working during the earlier art pass. | Visual-polish task; verify current build before submission and remove this entry if fixed. |
| KI-03 | Medium | Enemy and XP gem spawning uses `Instantiate` / `Destroy` rather than pooling | During combat, spawned/destroyed GameObjects may contribute to allocation spikes. The active-enemy cap limits crowding but does not replace pooling. | Profile on device and prioritise measured improvements; note as technical debt until pooling is implemented. |
| KI-04 | Low | Unfinished run is not restored to its exact state | Saving retains summary statistics, not character position, enemy positions, active upgrades or remaining time. | Deliberate scope limit for a short three-minute run; confirm retained statistics survive relaunch. |

## Pending verification before CA2 submission

| ID | Priority | Verification required | Evidence / acceptance test |
| --- | --- | --- | --- |
| V-01 | Critical | Release APK installs and launches after the signing-key change | Install on Android, start from app icon, record device/version and screenshot |
| V-02 | Critical | A full run reaches Victory or Game Over, with working Replay/Menu controls | 3–5 minute gameplay video; no blocking UI failures |
| V-03 | High | Android Home/background, resume, focus changes and Back behaviour | Repeated on-device tests; document pass/fail, crashes and any required fix |
| V-04 | High | Persistent run statistics and local telemetry survive relaunch | Complete/lose run, restart app, inspect displayed stats and `[telemetry]` logs |
| V-05 | High | On-device profiling and thermal stability | Baseline sheet with mean/99th frame time, CPU/GPU, GC allocations, peak memory, batches, 10-minute temperature delta and bottleneck before/after |
| V-06 | Medium | HUD safe-area margins and touch targets on the physical device | Test edge areas, Pause, level-up cards and floating joystick at target display size |
| V-07 | Medium | Correct final scene inclusion, release build version and signing alias | Verify Unity Build Profiles, Player Settings and clean Console; confirm APK identity |

## Recently addressed issues

- **Touch area intercepting UI clicks:** The floating joystick previously prevented selecting upgrades and using Pause. The UI raycast ordering and touch-area behaviour were adjusted. The player confirmed that the joystick, Pause and upgrade selection all worked together in the Editor.
- **Enemy overcrowding:** Enemy separation steering, an active-population cap and staged spawn-pressure limits were added and the player reported the resulting behaviour was better. The longer Android playtest is still outstanding.
- **Missing victory condition:** A three-minute survival win condition, victory panel and Replay/Main Menu actions were added and confirmed to work in the Editor.

## Reporting new issues

Record the **build version**, **device**, **steps to reproduce**, **expected result**, **actual result** and any Console/logcat message. Keep the list current; remove resolved issues only after a retest.
