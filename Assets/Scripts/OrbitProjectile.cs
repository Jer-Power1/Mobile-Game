using UnityEngine;

public class OrbitProjectile : MonoBehaviour
{
    private int damage = 1;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}