# Arcane Survivor — Economy and Telemetry Event Map (CA2)

## Economy loop

Defeat demons → collect XP gems → earn XP → reach the level-up threshold → select a free weapon or passive upgrade → gain a gameplay benefit. The first level requires **5 XP**; each subsequent threshold increases by **3 XP** (5, 8, 11, ...). XP spent on a level is deducted, and any remainder carries forward. This is an **in-game XP progression economy**, not an in-app purchase system; there are no real-money purchases.

## Local telemetry events

| Event | Parameters | When it fires | Purpose |
|---|---|---|---|
| `session_start` | `session_id`, `app_version`, `device_model`, `operating_system` | When the `Telemetry` component first enables in an application session | Identify the build and test environment |
| `level_start` | `level_id=arena_01` | At the start of the three-minute survival run (`SurvivalRunGoal.Start`) | Count attempted runs |
| `level_up` | `level`, `xp_spent`, `xp_remaining`, `next_xp_cost`, `total_xp_collected` | When an XP threshold is reached (`PlayerXP.LevelUp`) | Verify progression pace and XP carryover |
| `upgrade_purchased` | `upgrade_id`, `level`, `type`, `source=level_up_choice` | After a player selects an available upgrade (`UpgradeManager.SelectUpgrade`) | Record which free upgrades players choose |
| `level_complete` | `level_id=arena_01`, `time_s` | When the player survives the target duration (`SurvivalRunGoal.WinGame`) | Record successful runs |
| `level_fail` | `level_id=arena_01`, `time_s`, `cause=game_over` | When the existing Game Over panel becomes active (`SurvivalRunGoal.Update`) | Record failed runs |

## Implementation and validation

Events are emitted with `Debug.Log` using the `[telemetry]` prefix. The `Telemetry` component listens for these messages and optionally appends them to `telemetry.log` in `Application.persistentDataPath`. No external analytics SDK or network connection is used. Check the Unity Console to verify that a run produces `session_start` and `level_start`, a collected XP threshold produces `level_up`, selecting a card produces `upgrade_purchased`, and the run ends with either `level_complete` or `level_fail`.

**Testing status:** Event wiring is based on the current scripts. Actual event output and on-device log persistence still need to be verified; do not report these as tested until they have been observed.
