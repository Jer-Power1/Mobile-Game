using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick :
    MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("Joystick")]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;

    [Header("Settings")]
    [SerializeField] private float handleRange = 80f;

    private Vector2 input;

    public Vector2 InputDirection => input;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (background == null || handle == null)
            return;

        Vector2 localPoint;

        bool success =
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint
            );

        if (!success)
            return;

        Vector2 size =
            background.rect.size;

        Vector2 normalizedPosition =
            new Vector2(
                localPoint.x / (size.x * 0.5f),
                localPoint.y / (size.y * 0.5f)
            );

        input = Vector2.ClampMagnitude(
            normalizedPosition,
            1f
        );

        handle.anchoredPosition =
            input * handleRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;

        if (handle != null)
        {
            handle.anchoredPosition =
                Vector2.zero;
        }
    }
}