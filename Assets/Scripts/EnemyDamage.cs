
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int contactDamage = 10;

    [SerializeField] private float damageCooldown = 1f;

    [Header("Animation")]
    [SerializeField]
    private DemonAnimationController
        demonAnimation;

    private float nextDamageTime;

    private void Awake()
    {
        if (demonAnimation == null)
        {
            demonAnimation =
                GetComponentInChildren<DemonAnimationController>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamagePlayer(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryDamagePlayer(collision.gameObject);
    }

    private void TryDamagePlayer(GameObject other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Time.time < nextDamageTime)
            return;

        PlayerHealth playerHealth =
            other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        nextDamageTime =
            Time.time + Mathf.Max(0.01f, damageCooldown);

        if (demonAnimation != null)
        {
            demonAnimation.PlayAttack();
        }

        playerHealth.TakeDamage(contactDamage);
    }
}
