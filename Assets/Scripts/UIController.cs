using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Slider healthSlider;

    [SerializeField] string mainMenuScene;

    [SerializeField] GameObject pauseScreen;



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
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            PauseUnpause();
        }
        
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.value = currentHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }

}
