using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController Instance;

    [SerializeField] Slider bossHealthSlider;
    public int currentHealth;

    private BossBattle theBoss;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        theBoss = GetComponent<BossBattle>();

        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            AudioManager.Instance.PlaySFX(0);

            theBoss.EndBattle();
        }else
        {
            AudioManager.Instance.PlaySFX(1);
        }

        bossHealthSlider.value = currentHealth;
    }


}
