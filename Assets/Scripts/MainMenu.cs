using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string newGameLevel;

    [SerializeField] GameObject continueButton;

    public PlayerAbilityTracker playerAbility;

    void Start()
    {
        
        

        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }

        AudioManager.Instance.PlayMainMenuMusic();
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        
        SceneManager.LoadScene(newGameLevel);
    }

    public void ContinueGame()
    {
        playerAbility.gameObject.SetActive(true);
        PlayerHealthController.instance.isExitDoor = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));

        if (PlayerPrefs.HasKey("DashUnlocked"))
        {
            if (PlayerPrefs.GetInt("DashUnlocked") == 1)
            {
                playerAbility.canDash = true;
            }
        }

        if (PlayerPrefs.HasKey("DoubleJumpUnlocked"))
        {
            if (PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1)
            {
                playerAbility.canDoubleJump = true;
            }
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }

    
}
