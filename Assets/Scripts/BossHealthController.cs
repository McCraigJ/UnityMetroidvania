using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    [SerializeField]
    private Slider bossHealthSlider;

    [SerializeField]
    private int currentHealth = 30;

    [SerializeField]
    private BossBattle boss;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            boss.EndBattle();

            AudioManager.instance.PlaySFX(AudioSfx.BossDeath);
        } else
        {
            AudioManager.instance.PlaySFX(AudioSfx.BossImpact);
        }

        bossHealthSlider.value = currentHealth;
    }
}
