using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;

    [SerializeField]
    private bool destroyOnDamage = false;

    [SerializeField]
    private GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmount);

        if (destroyOnDamage)
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
