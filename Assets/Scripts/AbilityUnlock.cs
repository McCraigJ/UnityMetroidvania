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

                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
            }

            if (unlockDash)
            {
                playerAbilityTracker.CanDash = true;
                PlayerPrefs.SetInt("DashUnlocked", 1);

            }

            if (unlockBecomeBall)
            {
                playerAbilityTracker.CanBecomeBall = true;
                PlayerPrefs.SetInt("BallModeUnlocked", 1);
            }

            if (unlockDropBomb)
            {
                playerAbilityTracker.CanDropBomb = true;
                PlayerPrefs.SetInt("DropBombUnlocked", 1);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);


            unlockMessageText.transform.parent.SetParent(null);
            unlockMessageText.transform.parent.position = transform.position;
            unlockMessageText.text = unlockMessage;
            unlockMessageText.gameObject.SetActive(true);

            Destroy(unlockMessageText.transform.parent.gameObject, 5f);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(AudioSfx.PickupGem);

        }
    }
}
