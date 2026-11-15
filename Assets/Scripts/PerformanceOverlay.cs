
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class PerformanceOverlay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text performanceText;

    [Header("Settings")]
    [SerializeField] private float updateInterval = 0.5f;

    private float elapsed;
    private int frameCount;
    private float worstFrameMs;
    private bool isVisible;

    private const float BytesToMB = 1048576f;

    private void Start()
    {
        // Load the saved FPS display preference.
        SetVisible(GameSettings.ShowFPS);
    }

    private void Update()
    {
        if (!isVisible || performanceText == null)
            return;

        float delta = Time.unscaledDeltaTime;

        if (delta <= 0f)
            return;

        elapsed += delta;
        frameCount++;

        float frameMs = delta * 1000f;

        if (frameMs > worstFrameMs)
        {
            worstFrameMs = frameMs;
        }

        if (elapsed < Mathf.Max(0.1f, updateInterval))
            return;

        float fps = frameCount / elapsed;

        float averageFrameMs =
            elapsed * 1000f / frameCount;

        long memoryBytes =
            Profiler.GetTotalAllocatedMemoryLong();

        string memoryText = memoryBytes > 0
            ? (memoryBytes / BytesToMB).ToString("F0") + " MB"
            : "N/A";

        performanceText.text =
            $"FPS: {fps:F0}\n" +
            $"Avg Frame: {averageFrameMs:F1} ms\n" +
            $"Worst Frame: {worstFrameMs:F1} ms\n" +
            $"Unity Memory: {memoryText}";

        ResetCounters();
    }

    private void ResetCounters()
    {
        elapsed = 0f;
        frameCount = 0;
        worstFrameMs = 0f;
    }

    public void ToggleOverlay()
    {
        SetVisible(!isVisible);
    }

    public void SetVisible(bool visible)
    {
        isVisible = visible;

        if (performanceText != null)
        {
            performanceText.gameObject.SetActive(visible);
        }

        ResetCounters();
    }
}
