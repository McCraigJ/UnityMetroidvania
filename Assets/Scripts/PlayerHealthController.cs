using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private float invincibilityLength;
    [SerializeField]
    private float flashLength;

    [SerializeField]
    private SpriteRenderer[] playerSprites;

    private float invincibilityCounter;
    private float flashCounter;

    private int currentHealth;

    private void Awake()
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

    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if (invincibilityCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincibilityCounter > 0)
        {
            return;
        }

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {

            currentHealth = 0;

            RespawnController.instance.Respawn();
        }
        else
        {
            invincibilityCounter = invincibilityLength;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        Mathf.Clamp(currentHealth, 0, maxHealth);
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public int GetCurrentHealth() => currentHealth;
}
