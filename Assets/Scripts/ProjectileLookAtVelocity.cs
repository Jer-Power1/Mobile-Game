
using UnityEngine;

public class ProjectileLookAtVelocity : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D projectileRigidbody;

    [Header("Rotation")]
    [SerializeField] private float rotationOffset = 0f;
    [SerializeField] private float minimumMovement = 0.001f;

    private Vector3 previousPosition;

    private void Awake()
    {
        if (projectileRigidbody == null)
        {
            projectileRigidbody =
                GetComponentInParent<Rigidbody2D>();
        }
    }

    private void OnEnable()
    {
        previousPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector2 direction = Vector2.zero;

        if (projectileRigidbody != null &&
            projectileRigidbody.linearVelocity.sqrMagnitude >
            minimumMovement * minimumMovement)
        {
            direction = projectileRigidbody.linearVelocity;
        }
        else
        {
            direction = transform.position - previousPosition;
        }

        if (direction.sqrMagnitude >
            minimumMovement * minimumMovement)
        {
            float angle = Mathf.Atan2(
                direction.y,
                direction.x
            ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(
                0f,
                0f,
                angle + rotationOffset
            );
        }

        previousPosition = transform.position;
    }
}
