using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private Transform camPosition;
    [SerializeField] private float camSpeed;
    [SerializeField] private int threshold1;
    [SerializeField] private int threshold2;
    [SerializeField] private float activeTime;
    [SerializeField] private float fadeoutTime;
    [SerializeField] private float inactiveTime;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform bossTransform;
    [SerializeField] private float timeBetweenShots1;
    [SerializeField] private float timeBetweenShots2;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject winObjects;

    private CameraController mainCamera;
    private float activeCounter;
    private float fadeCounter;
    private float inactiveCounter;
    private Transform targetPoint;
    private float shotCounter;
    private bool battleEnded;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<CameraController>();
        mainCamera.enabled = false;

        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;

        //bossTransform.position = spawnPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, camPosition.position, camSpeed * Time.deltaTime);

        if (!battleEnded)
        {

            if (BossHealthController.instance.GetCurrentHealth() > threshold1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeoutTime;
                        anim.SetTrigger("vanish");
                    }

                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots1;
                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        bossTransform.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        bossTransform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        bossTransform.gameObject.SetActive(true);
                        activeCounter = activeTime;

                        shotCounter = timeBetweenShots1;
                    }
                }
            }
            else
            {
                if (targetPoint == null)
                {
                    targetPoint = bossTransform;
                    fadeCounter = fadeoutTime;
                    anim.SetTrigger("vanish");
                }
                else
                {
                    if (Vector3.Distance(bossTransform.position, targetPoint.position) > 0.02f)
                    {
                        bossTransform.position = Vector3.MoveTowards(bossTransform.position, targetPoint.position, moveSpeed * Time.deltaTime);

                        if (Vector3.Distance(bossTransform.position, targetPoint.position) <= 0.02f)
                        {
                            fadeCounter = fadeoutTime;
                            anim.SetTrigger("vanish");
                        }

                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (BossHealthController.instance.GetCurrentHealth() > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }

                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            bossTransform.gameObject.SetActive(false);
                            inactiveCounter = inactiveTime;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            bossTransform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            while (targetPoint.position == bossTransform.position)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            }
                            bossTransform.gameObject.SetActive(true);

                            if (BossHealthController.instance.GetCurrentHealth() > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            fadeCounter -= Time.deltaTime;
            if (fadeCounter < 0)
            {
                if (winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);

                }
                mainCamera.enabled = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void EndBattle()
    {
        battleEnded = true;

        fadeCounter = fadeoutTime;
        anim.SetTrigger("vanish");
        bossTransform.GetComponent<Collider2D>().enabled = false;
        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        foreach (BossBullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}
