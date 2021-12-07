using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float distanceToOpen;

    [SerializeField]
    private float waitBeforeUseDoor = 1.5f;

    [SerializeField]
    private Transform exitPoint;

    [SerializeField]
    private float movePlayerSpeed;

    [SerializeField]
    private string levelToLoad;

    private PlayerController player;
    private bool playerExiting;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
        playerExiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        } else
        {
            anim.SetBool("doorOpen", false);
        }
        if (playerExiting)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !playerExiting)
        {
            player.SetCanMove(false);
            StartCoroutine(UseDoorCo());
            
        }
    }

    private IEnumerator UseDoorCo()
    {
        playerExiting = true;
        player.SetAnimEnabled(false);
        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitBeforeUseDoor);

        ResetAfterUseDoor();

    }

    private void ResetAfterUseDoor()
    {
        RespawnController.instance.SetSpawn(exitPoint.position);
        player.SetCanMove(true);
        player.SetAnimEnabled(true);

        UIController.instance.StartFadeFromBlack();
        SceneManager.LoadScene(levelToLoad);
    }
}
