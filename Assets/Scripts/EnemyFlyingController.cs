using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    [SerializeField]
    private float rangeToStartChase;
    [SerializeField]
    private float chaseSpeed = 8f;
    [SerializeField]
    private float returnSpeed = 4f;
    [SerializeField]
    private float waitBeforeReturn = 1f;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private Animator anim;

    private Vector3 startPoint;
    private Quaternion startRotation;

    private bool isChasing;
    private bool isReturning;
    private bool returnAfterChase = false;
    private float waitingBeforeReturnCounter;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
        startPoint = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.activeSelf)
        {
            Chase();
        }
    }

    private void Chase()
    {
        bool isChasing = Vector3.Distance(transform.position, player.position) < rangeToStartChase;
        if (isChasing)
        {
            returnAfterChase = true;
            isReturning = false;
            MoveToward(player.position, true);
        }
        else if (returnAfterChase)
        {
            returnAfterChase = false;
            waitingBeforeReturnCounter = waitBeforeReturn;
        }
        
        if (waitingBeforeReturnCounter > 0f)
        {
            waitingBeforeReturnCounter -= Time.deltaTime;
            if (waitingBeforeReturnCounter <= 0f)
            {
                isReturning = true;
            }
        }

            if (isReturning)
            {
                MoveToward(startPoint, false);
                if (Vector3.Distance(transform.position, startPoint) < 0.6f)
                {
                    transform.rotation = startRotation;
                    isReturning = false;
                }
            }
        

        anim.SetBool("isChasing", isChasing || isReturning || waitingBeforeReturnCounter > 0f);

    }

    private void MoveToward(Vector3 point, bool isChasing)
    {
        Vector3 direction = transform.position - point;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        float moveSpeed = isChasing ? chaseSpeed : returnSpeed;
        transform.position += transform.right * -moveSpeed * Time.deltaTime;
    }
}
