using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField]
    private bool unlockDoubleJump;

    [SerializeField]
    private bool unlockDash;

    [SerializeField]
    private bool unlockBecomeBall;

    [SerializeField]
    private bool unlockDropBomb;

    [SerializeField]
    private GameObject pickupEffect;

    [SerializeField]
    private string unlockMessage;

    [SerializeField]
    private TextMeshProUGUI unlockMessageText;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Player")
        {
            PlayerAbilityTracker playerAbilityTracker = collision.GetComponentInParent<PlayerAbilityTracker>();

            

            if (unlockDoubleJump)
            {
                playerAbilityTracker.CanDoubleJump = true;
            }

            if (unlockDash)
            {
                playerAbilityTracker.CanDash = true;

            }

            if (unlockBecomeBall)
            {
                playerAbilityTracker.CanBecomBall = true;
            }

            if (unlockDropBomb)
            {
                playerAbilityTracker.CanDropBomb = true;
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);


            unlockMessageText.transform.parent.SetParent(null);
            unlockMessageText.transform.parent.position = transform.position;
            unlockMessageText.text = unlockMessage;
            unlockMessageText.gameObject.SetActive(true);

            Destroy(unlockMessageText.transform.parent.gameObject, 5f);

            Destroy(gameObject);

        }
    }
}
