
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DemonAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Movement Animation")]
    [SerializeField] private float movingThreshold = 0.05f;

    [Header("Facing Direction")]
    [SerializeField] private bool facesRightByDefault = true;
    [SerializeField] private float horizontalThreshold = 0.05f;

    private Animator animator;

    private Vector2 previousPosition;
    private Vector2 movementVelocity;

    private bool initialized;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (enemyRigidbody == null)
        {
            enemyRigidbody =
                GetComponentInParent<Rigidbody2D>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer =
                GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        if (enemyRigidbody != null)
        {
            previousPosition = enemyRigidbody.position;
            initialized = true;
        }
    }

    private void FixedUpdate()
    {
        if (enemyRigidbody == null)
            return;

        Vector2 currentPosition =
            enemyRigidbody.position;

        if (!initialized)
        {
            previousPosition = currentPosition;
            initialized = true;
            return;
        }

        movementVelocity =
            (currentPosition - previousPosition) /
            Time.fixedDeltaTime;

        previousPosition = currentPosition;
    }

    private void Update()
    {
        if (animator == null)
            return;

        float speed = movementVelocity.magnitude;

        animator.SetFloat(
            "Speed",
            speed > movingThreshold ? 1f : 0f
        );

        UpdateFacingDirection();
    }

    private void UpdateFacingDirection()
    {
        if (spriteRenderer == null)
            return;

        if (movementVelocity.x > horizontalThreshold)
        {
            spriteRenderer.flipX =
                !facesRightByDefault;
        }
        else if (movementVelocity.x < -horizontalThreshold)
        {
            spriteRenderer.flipX =
                facesRightByDefault;
        }
    }

    public void PlayAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}
