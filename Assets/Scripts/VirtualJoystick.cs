
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VirtualJoystick :
    MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("References")]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private Canvas canvas;

    [Header("Gameplay UI")]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Settings")]
    [SerializeField] private float handleRange = 65f;
    [SerializeField] private float deadZone = 0.1f;

    private Image touchAreaImage;

    private Vector2 input;
    private int activePointerId = int.MinValue;

    public Vector2 InputDirection => input;

    private void Awake()
    {
        touchAreaImage = GetComponent<Image>();

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        ResetJoystick();
    }

    private void OnEnable()
    {
        if (touchAreaImage == null)
        {
            touchAreaImage = GetComponent<Image>();
        }

        UpdateRaycastState();
    }

    private void Update()
    {
        UpdateRaycastState();

        if (IsGameplayBlocked())
        {
            ResetJoystick();
        }
    }

    private void OnDisable()
    {
        ResetJoystick();
    }

    private bool IsGameplayBlocked()
    {
        if (Time.timeScale == 0f)
            return true;

        if (levelUpPanel != null &&
            levelUpPanel.activeInHierarchy)
            return true;

        if (pausePanel != null &&
            pausePanel.activeInHierarchy)
            return true;

        if (gameOverPanel != null &&
            gameOverPanel.activeInHierarchy)
            return true;

        return false;
    }

    private void UpdateRaycastState()
    {
        if (touchAreaImage == null)
            return;

        touchAreaImage.raycastTarget =
            !IsGameplayBlocked();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsGameplayBlocked())
            return;

        if (activePointerId != int.MinValue)
            return;

        if (background == null ||
            handle == null ||
            canvas == null)
            return;

        activePointerId = eventData.pointerId;

        RectTransform canvasRect =
            canvas.transform as RectTransform;

        Camera uiCamera =
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : eventData.pressEventCamera;

        if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRect,
            eventData.position,
            uiCamera,
            out Vector3 worldPoint))
        {
            activePointerId = int.MinValue;
            return;
        }

        background.position = worldPoint;
        background.gameObject.SetActive(true);

        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        UpdateInput(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        if (IsGameplayBlocked())
        {
            ResetJoystick();
            return;
        }

        UpdateInput(eventData);
    }

    private void UpdateInput(PointerEventData eventData)
    {
        if (background == null ||
            handle == null ||
            canvas == null)
            return;

        Camera uiCamera =
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : eventData.pressEventCamera;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            uiCamera,
            out Vector2 localPoint))
            return;

        float radius = Mathf.Max(1f, handleRange);

        Vector2 normalized =
            Vector2.ClampMagnitude(localPoint / radius, 1f);

        input = normalized.magnitude < deadZone
            ? Vector2.zero
            : normalized;

        handle.anchoredPosition =
            input * radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        ResetJoystick();
    }

    private void ResetJoystick()
    {
        activePointerId = int.MinValue;
        input = Vector2.zero;

        if (handle != null)
        {
            handle.anchoredPosition = Vector2.zero;
        }

        if (background != null)
        {
            background.gameObject.SetActive(false);
        }
    }
}
