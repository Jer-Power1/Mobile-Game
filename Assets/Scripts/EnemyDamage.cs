using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int contactDamage = 10;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        PlayerHealth playerHealth =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
    }
}