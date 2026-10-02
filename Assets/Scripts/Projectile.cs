using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private int damage;

    public void Setup(Vector2 newDirection, int newDamage)
    {
        direction = newDirection.normalized;
        damage = newDamage;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy == null)
            return;

        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}