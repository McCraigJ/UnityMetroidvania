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
