# Arcane Survivor

Arcane Survivor is a 2D mobile arena-survivor roguelite developed in Unity for the Mobile Game Development module at SETU.

The player moves using a virtual joystick while weapons attack automatically. Enemies become increasingly numerous during a run and drop XP when defeated. Levelling allows the player to select weapon and passive upgrades.

## Development Environment

- Unity: 6.6 / 6000.6.0f1
- Target platform: Android
- Orientation: Landscape
- Input: Unity Input System Package (New)
- Reference/test device: Samsung Galaxy S23 Ultra

## Android Build Configuration

The CA1 release APK uses the following Android configuration:

- Scripting Backend: IL2CPP
- Target Architecture: ARM64 only
- C++ Compiler Configuration: Release
- Build App Bundle: Off
- Minimum API Level: Android 8.0 / API Level 26
- Target API Level: Automatic (highest installed)
- Package Name: `com.jer.rogue`
- Version: `0.1.0`
- Bundle Version Code: `1`

The CA1 APK is located at:

`releases/ArcaneSurvivor-0.1.0-release-arm64.apk`

## Release Signing

The Android release build is signed using a custom release keystore.

- Keystore name: `User.keystore`
- Key alias: `mygame`
- Creation date: 2 October 2026
- Certificate valid from: 2 October 2026
- Certificate valid until: 19 September 2076
- Validity: approximately 50 years
- Key type: 2048-bit RSA

The keystore is stored securely outside the Git repository at:

`C:\Users\micha\OneDrive - South East Technological University (Waterford Campus)\website development 1\Not school\Documents\user.keystore`

Keystore and key passwords are not stored in this repository.

The same signing key should be retained for future updates to the application.

## Building the Android APK

1. Open the project using Unity 6.6.
2. Select the Android build platform.
3. Confirm the Scripting Backend is set to IL2CPP.
4. Confirm ARM64 is enabled and ARMv7 is disabled.
5. Confirm Build App Bundle is disabled so that Unity produces an APK.
6. Confirm the custom release keystore and `mygame` alias are selected under Android Publishing Settings.
7. Build the APK as a Release build.
8. Output the release APK to the `releases` directory.

Passwords for the signing key must be entered locally and must not be committed to Git.

## Installing on an Android Device

Connect an Android device with USB debugging enabled.

Check that ADB can see the device:

`adb devices`

Install or reinstall the release APK:

`adb install -r releases/ArcaneSurvivor-0.1.0-release-arm64.apk`

For the CA1 device test, the APK was successfully installed and run on a Samsung Galaxy S23 Ultra.

ADB installation evidence is stored at:

`docs/CA1/install-proof.txt`

The on-device gameplay screenshot is stored at:

`docs/CA1/device-screenshot.png`

## Versioning

The current CA1 release is:

- Application version: `0.1.0`
- Android Bundle Version Code: `1`

The package name `com.jer.rogue` should remain stable between releases.

For subsequent Android releases, the Bundle Version Code must be incremented before building the new APK/AAB. The semantic application version should also be updated when appropriate.

## Current Gameplay Features

The current vertical slice includes:

- One-thumb virtual joystick movement
- Automatic weapon attacks
- Three weapon systems
- XP collection and levelling
- Weapon and passive upgrades
- Increasing enemy pressure
- Player health and enemy contact damage
- Health, XP, level and run-time HUD
- Pause and resume
- Android safe-area handling
- Game-over and restart flow
- Main-menu scene

Placeholder geometric graphics are currently used so development can focus on the gameplay loop and mobile platform requirements.

## CA1 Documentation

CA1 supporting documentation is located under `docs/CA1/`:

- `install-proof.txt`
- `device-screenshot.png`
- `store-assets-checklist.md`
- `descriptions.md`
- `privacy-statement.md`
- `mda-onepager.md`

The development journal is located at:

`docs/dev-journal.md`

## Security

The release keystore itself and all signing passwords are excluded from the repository. No passwords or other signing secrets should be committed to source control.