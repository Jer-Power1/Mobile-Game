# Arcane Survivor — Data Use and Privacy Statement

## Purpose

This statement describes the current runtime behaviour of the CA1 release of **Arcane Survivor** and how its data use would be considered for a future Google Play Data safety declaration.

This statement applies to the current CA1 release build, version **0.1.0**, package name `com.jer.rogue`.

## Current Game Behaviour

Arcane Survivor is currently a single-player Android game.

The player moves using a virtual joystick, fights enemies using automatic weapon attacks, collects XP and selects upgrades during gameplay.

The current student-authored gameplay systems do not intentionally require a user account or collect personal information from the player.

The game currently does not intentionally implement:

- User registration or login.
- Online multiplayer.
- Advertising.
- In-app purchases.
- Location-based gameplay.
- Contact or address-book access.
- Camera or microphone features.
- User-generated content.
- Cloud saves.
- Social-media integration.

The current gameplay does not depend on an Internet connection.

## Local Runtime Data

During a run, the game maintains gameplay information such as:

- Player health.
- Player XP and level.
- Weapon and passive upgrade levels.
- Enemy state.
- Current run time.
- Pause and game-over state.

This information is used to operate the current game session.

The current vertical slice does not intentionally transmit this gameplay information to an external server.

No account-based progression or cloud save system has been implemented in the current CA1 version.

## APK Permission Verification

The final CA1 release APK was inspected using Android's `aapt` tool to verify the permissions contained in the built application.

The APK declares the following permissions:

- `android.permission.INTERNET`
- `android.permission.ACCESS_NETWORK_STATE`
- `com.android.vending.BILLING`
- `com.jer.rogue.DYNAMIC_RECEIVER_NOT_EXPORTED_PERMISSION`

The current Arcane Survivor gameplay code does not intentionally implement online multiplayer, user accounts, advertising or in-app purchases. However, the final Unity-generated APK contains Internet, network-state and Google Play Billing permissions.

These permissions may originate from Unity packages or services included in the project rather than from the core gameplay systems.

The presence of a permission does not by itself demonstrate that personal data is collected, shared or transmitted. Therefore, I would not claim that the application collects data solely because these permissions are present, but I would also not assume that the final application has no network-capable components without further verification.

Before publishing the game, I would audit the Unity packages and services included in the final build, remove unnecessary services or permissions where possible, and inspect the final application's runtime network behaviour.

## Data Collection and Sharing

Based on the gameplay systems intentionally implemented for the current CA1 version, I have not intentionally implemented the collection or sharing of personal user data.

However, because the generated APK contains network-related and Google Play Billing permissions, a production privacy declaration would require verification of all included Unity packages, SDKs and services before stating definitively that no data is collected or shared.

Any third-party SDK or Unity service included in a future release would also need to be considered when determining the application's actual data practices.

## Google Play Data Safety

If Arcane Survivor were prepared for publication on Google Play, I would complete the Data safety declaration based on the behaviour of the **final release build**, including both my own game code and any third-party SDKs or Unity services included in that build.

The declaration would need to accurately identify:

- Whether any user data is collected.
- Whether any collected data is shared with third parties.
- The purpose for which any data is collected or processed.
- Whether data is encrypted in transit where applicable.
- Whether users can request deletion where applicable.
- The behaviour of any analytics, advertising, billing or other third-party services.

I would not base the declaration only on the intended behaviour of my own gameplay scripts. I would verify the final APK and all included services before completing the form.

If future versions introduce features such as analytics, advertising, online accounts, cloud saves, in-app purchases or other network services, this statement and the corresponding Data safety declaration would need to be updated.

## Privacy Policy for Future Publication

For a future public release, I would provide an appropriate privacy policy where required and ensure that it accurately reflects the final application's behaviour.

The policy and Google Play Data safety information would be reviewed whenever features, SDKs, Unity services or data-handling behaviour change.

## CA1 Summary

For the current Arcane Survivor CA1 release:

- The game is a single-player Android application.
- No user account system has been implemented.
- No advertising system has been intentionally implemented.
- No in-app purchase system has been intentionally implemented.
- No location, contacts, camera or microphone gameplay features have been implemented.
- Gameplay state is maintained for the current game session.
- The student-authored gameplay systems do not intentionally transmit personal user information.
- The final APK declares Internet, network-state and Google Play Billing permissions.
- The final APK and included Unity services would require further auditing before making a production Google Play Data safety declaration.

This statement reflects the current CA1 build and would be reviewed and updated before any future store publication.