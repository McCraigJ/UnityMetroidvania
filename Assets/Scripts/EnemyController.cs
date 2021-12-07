using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float waitAtPoints;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Rigidbody2D enemyRigidbody;
    [SerializeField]
    private Animator anim;

    private int currentPatrolPointIndex;
    private float waitCounter;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoints;
        foreach (Transform point in patrolPoints)
        {
            point.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = CalculateDistanceToPatrolPoint();
        if (Mathf.Abs(distance) > 0.2f)
        {
            if (distance < 0)
            {
                enemyRigidbody.velocity = new Vector2(moveSpeed, enemyRigidbody.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                enemyRigidbody.velocity = new Vector2(-moveSpeed, enemyRigidbody.velocity.y);
            }

            if (transform.position.y < patrolPoints[currentPatrolPointIndex].position.y - 0.5f && enemyRigidbody.velocity.y < 0.1f)
            {
                enemyRigidbody.velocity = new Vector2(enemyRigidbody.velocity.x, jumpForce);
            }
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(0f, enemyRigidbody.velocity.y);
            waitCounter -= Time.deltaTime;
        }

        if (waitCounter <= 0)
        {
            waitCounter = waitAtPoints;

            currentPatrolPointIndex++;
            
            if (currentPatrolPointIndex >= patrolPoints.Length)
            {
                currentPatrolPointIndex = 0;
            }
        }

        anim.SetFloat("speed", Mathf.Abs(enemyRigidbody.velocity.x));
    }

    private float CalculateDistanceToPatrolPoint()
    {
        return transform.position.x - patrolPoints[currentPatrolPointIndex].position.x;
    }

}
