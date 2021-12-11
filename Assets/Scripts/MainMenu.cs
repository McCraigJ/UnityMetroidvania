using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string newGameScene;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private PlayerAbilityTracker playerAbilityTracker;

    private void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();    
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        ResetPlayerPrefs();
        SceneManager.LoadScene(newGameScene);
    }

    public void ContinueGame()
    {
        playerAbilityTracker.gameObject.SetActive(true);
        playerAbilityTracker.gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        
        if (PlayerPrefs.HasKey("DoubleJumpUnlocked"))
        {
            playerAbilityTracker.CanDoubleJump = PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1;
        }
        
        if (PlayerPrefs.HasKey("DashUnlocked"))
        {
            playerAbilityTracker.CanDash = PlayerPrefs.GetInt("DashUnlocked") == 1;
        }

        if (PlayerPrefs.HasKey("BallModeUnlocked"))
        {
            playerAbilityTracker.CanBecomeBall = PlayerPrefs.GetInt("BallModeUnlocked") == 1;
        }

        if (PlayerPrefs.HasKey("DropBombUnlocked"))
        {
            playerAbilityTracker.CanDropBomb = PlayerPrefs.GetInt("DropBombUnlocked") == 1;
        }

        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));        
    }

    public void QuitGame()
    {
        Application.Quit();
        
        Debug.Log("Quit");
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
