using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private float timeToExplode = 0.5f;
    
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float blastRange;

    [SerializeField]
    private LayerMask destructibleLayer;
    [SerializeField] private int damageAmount;
    [SerializeField] private LayerMask damageLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if (timeToExplode <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);                
            }

            Destroy(gameObject);

            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, destructibleLayer);

            if (objectsToRemove.Length > 0)
            {
                foreach (Collider2D obj in objectsToRemove)
                {
                    Destroy(obj.gameObject);
                }
            }

            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, blastRange, damageLayer);

            foreach (Collider2D col in objectsToDamage)
            {
                EnemyHealthController enemyHealthController = col.GetComponent<EnemyHealthController>();
                if (enemyHealthController != null)
                {
                    enemyHealthController.DamageEnemy(damageAmount);
                }
            }
        }
    }
}
