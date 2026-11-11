
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 3f;

    [Header("Visual Rotation")]
    [SerializeField] private float rotationOffset = 0f;

    private Vector2 direction = Vector2.right;
    private int damage;
    private bool isSetup;

    public void Setup(Vector2 newDirection, int newDamage)
    {
        if (newDirection.sqrMagnitude < 0.0001f)
        {
            newDirection = Vector2.right;
        }

        direction = newDirection.normalized;
        damage = newDamage;
        isSetup = true;

        // The blue lightning sprite points right by default.
        // Rotate its tip towards the firing direction.
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

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (!isSetup)
            return;

        // Move in WORLD space, so rotating the sprite
        // does not change the movement direction.
        transform.Translate(
            (Vector3)(direction * speed * Time.deltaTime),
            Space.World
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy =
            other.GetComponent<EnemyHealth>();

        if (enemy == null)
            return;

        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
