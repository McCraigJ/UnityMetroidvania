using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Rigidbody2D bulletRigidbody;

    [SerializeField]
    private Vector2 moveDir;

    [SerializeField]
    private GameObject impactEffect;

    [SerializeField]
    private int damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDir = direction;
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = moveDir * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }

        if (collision.tag == "Boss")
        {
            
            BossHealthController.instance.TakeDamage(damageAmount);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
