using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager newAm = Instantiate(audioManager);
            AudioManager.instance = newAm;
            DontDestroyOnLoad(newAm.gameObject);
        }
    }
}
