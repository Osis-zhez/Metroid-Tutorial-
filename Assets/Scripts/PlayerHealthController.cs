using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [Header("Обратная связь получения урона игроком")]
    [SerializeField] private float invicibilityLength;
    private float invicCounter;

    [SerializeField] private float flashLength;
    private float flashCounter;

    [SerializeField] private SpriteRenderer playerSprites;

    public bool isExitDoor, isEntranceDoor = false;


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

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
        {
            currentHealth -= 1;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    void Update() 
    {
        if (invicCounter > 0)
        {
            invicCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerSprites.enabled = !playerSprites.enabled;

                flashCounter = flashLength;
            }

            if (invicCounter <= 0)
            {
                playerSprites.enabled = true;
            }
            
        }
    }


    public void DamagePlayer(int damageAmount)
    {
        if (invicCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false);

                RespawnController.instance.Respawn();

                AudioManager.Instance.PlaySFX(8);
            }
            else
            {
                AudioManager.Instance.PlaySFXAdjucted(11);

                invicCounter = invicibilityLength;
            }
            
            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth = currentHealth + healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
