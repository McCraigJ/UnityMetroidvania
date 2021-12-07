using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private CameraController camera;

    [SerializeField]
    private Transform camPosition;

    [SerializeField]
    private float camSpeed;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraController>();
        camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, camPosition.position, camSpeed * Time.deltaTime);
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
