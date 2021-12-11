using UnityEngine;

public enum AudioSfx
{
    BossDeath = 0,
    BossImpact = 1,
    BossShot = 2,
    BulletImpact = 3,
    Explode = 4,
    PickupGem = 5,
    PlayerBall = 6,
    PlayerDash = 7,
    PlayerDeath = 8,
    PlayerDoubleJump = 9,
    PlayerFromBall = 10,
    PlayerHurt = 11,
    PlayerJump = 12,
    PlayerMine = 13,
    PlayerShoot = 14
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private AudioSource levelMusic;
    [SerializeField] private AudioSource bossMusic;
    [SerializeField] private AudioSource[] sfx;

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

    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            bossMusic.Stop();
            mainMenuMusic.Stop();
            levelMusic.Play();
        }
    }

    public void PlayBossMusic()
    {
        levelMusic.Stop();
        mainMenuMusic.Stop();
        bossMusic.Play();
    }

    public void PlaySFX(AudioSfx sfxIndex)
    {
        sfx[(int)sfxIndex].Stop();
        sfx[(int)sfxIndex].Play();
    }

    public void PlaySFXAdjusted(AudioSfx sfxIndex)
    {
        sfx[(int)sfxIndex].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(sfxIndex);
    }
}
