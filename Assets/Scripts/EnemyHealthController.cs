using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    private int totalHealth = 3;

    [SerializeField]
    private GameObject deathEffect;

    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;
        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            AudioManager.instance.PlaySFXAdjusted(AudioSfx.Explode);

            Destroy(gameObject);
        }
    }
}
