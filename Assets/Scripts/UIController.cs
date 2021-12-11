using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Image fadeScreen;

    [SerializeField]
    private float fadeSpeed = 2f;

    [SerializeField] private string mainMenuScene;
    [SerializeField] private GameObject pauseScreen;

    private bool isFadingToBlack = false;
    private bool isFadingFromBlack = false;

    public static UIController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //UpdateHealth(PlayerHealthController.instance.GetCurrentHealth(), PlayerHealthController.instance.GetMaxHealth());
    }

    private void Update()
    {
        if (isFadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                isFadingFromBlack = false;
            }

        } else if (isFadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                isFadingFromBlack = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void StartFadeToBlack()
    {
        isFadingToBlack = true;
        isFadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        isFadingFromBlack = true;
        isFadingToBlack = false;
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        
    }

    public void GoToMainMenu()
    {
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;
        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;
        Destroy(gameObject);
        instance = null;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
