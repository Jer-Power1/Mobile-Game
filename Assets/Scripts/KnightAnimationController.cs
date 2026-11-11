
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class KnightAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Animation")]
    [SerializeField] private float movingThreshold = 0.05f;

    [Header("Facing Direction")]
    [SerializeField] private bool facesRightByDefault = true;
    [SerializeField] private float horizontalThreshold = 0.05f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (playerRigidbody == null)
        {
            playerRigidbody =
                GetComponentInParent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (playerRigidbody == null || animator == null)
            return;

        Vector2 velocity = playerRigidbody.linearVelocity;

        float speed = velocity.magnitude;

        animator.SetFloat(
            "Speed",
            speed > movingThreshold ? 1f : 0f
        );

        if (spriteRenderer == null)
            return;

        if (velocity.x > horizontalThreshold)
        {
            spriteRenderer.flipX = !facesRightByDefault;
        }
        else if (velocity.x < -horizontalThreshold)
        {
            spriteRenderer.flipX = facesRightByDefault;
        }
    }
}
