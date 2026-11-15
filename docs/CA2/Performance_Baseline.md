# Arcane Survivor — CA2 Performance Baseline

**Capture date:** 15 November 2026  
**Source:** Unity Profiler connected to Samsung SM-S918B (Android Development Build).

## Device Information

| Metric | Result |
|---|---|
| Device | Samsung Galaxy S23 Ultra (SM-S918B) |
| Android version | 16 |
| Processor / SoC | Snapdragon 8 Gen 2 for Galaxy (model specification; not verified on device) |
| RAM | Not yet verified for this handset |
| Battery health estimate | Not yet measured |
| APK version | 0.5.0 (release APK filename; installed version to verify) |
| APK size | 66,360 KB (approximately 64.8 MiB) |
| Screen resolution / render scale | Landscape; exact values not yet recorded |

## Performance Measurements

| Metric | Result |
|---|---|
| Selected frame time | **33.73 ms (~29.6 FPS)** |
| Average frame time | Not yet calculated |
| 99th-percentile frame time | Not yet calculated |
| CPU frame time | 33.73 ms, selected frame including waits |
| GPU frame time | Not reported by Profiler |
| WaitForTargetFPS | 28.11 ms, selected frame |
| GC allocations/frame | Not yet measured |
| Unity total used memory | **389.7 MB**, selected frame |
| Texture memory | **132.0 MB**, selected frame |
| Peak memory | Not yet measured |
| Draw calls/batches | Not yet measured |
| Cold-start time | Not yet measured |

## Thermal Test

| Metric | Result |
|---|---|
| Starting battery temperature | Not yet measured |
| Temperature after 10 minutes | Not yet measured |
| Temperature increase | Not yet measured |
| Thermal throttling observed | Not yet evaluated |

## Performance Optimisation

**Bottleneck investigated:** The selected Android frame took 33.73 ms, including 28.11 ms in `WaitForTargetFPS`. This suggests that frame pacing or a frame-rate limit may be influencing performance, but the cause needs verification.

**Changes implemented:** Enemy population caps and separation behaviour were introduced to prevent excessive crowding. Per-gem XP debug logging was also removed to reduce unnecessary work during gameplay.

**Before metric:** No comparable pre-optimisation Android measurement is currently available.

**After metric:** Selected-frame time of 33.73 ms, with 389.7 MB of Unity memory reported. These figures do not establish a performance improvement by themselves.

**Side effects / verification:** Enemy damage, XP collection, upgrades, victory/defeat and the UI should be checked in the final APK. A repeatable before/after profiling comparison is still required.

**Measurement limitation:** Selected-frame values are not averages or peaks. The missing measurements must be collected separately before they can be reported as completed evidence.