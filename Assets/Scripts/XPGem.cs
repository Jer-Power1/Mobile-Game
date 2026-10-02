using UnityEngine;

public class XPGem : MonoBehaviour
{
    [SerializeField] private int xpValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerXP playerXP = other.GetComponent<PlayerXP>();

        if (playerXP == null)
            return;

        playerXP.AddXP(xpValue);
        Destroy(gameObject);
    }
}