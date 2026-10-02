using UnityEngine;

public class NovaEffect : MonoBehaviour
{
    [SerializeField] private float duration = 0.4f;

    private float timer;
    private Vector3 targetScale;

    private SpriteRenderer spriteRenderer;
    private Color startColor;

    public void Setup(float radius)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;

        // Circle diameter = radius * 2
        targetScale = Vector3.one * radius * 2f;

        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float progress = timer / duration;

        // Expand outward
        transform.localScale =
            Vector3.Lerp(
                Vector3.zero,
                targetScale,
                progress
            );

        // Fade away
        Color colour = startColor;
        colour.a = Mathf.Lerp(
            startColor.a,
            0f,
            progress
        );

        spriteRenderer.color = colour;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}