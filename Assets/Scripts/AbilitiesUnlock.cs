using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilitiesUnlock : MonoBehaviour
{
    [SerializeField] private bool unlockDash, unlockDoubleJump;
    [SerializeField] GameObject abilitiesVFX;

    [SerializeField] string unlockMessage;
    [SerializeField] TMP_Text unlockText;
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerAbilityTracker abilitiesTracker = other.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDash == true)
            {
                abilitiesTracker.canDash = true;
                PlayerPrefs.SetInt("DashUnlocked", 1);
            }

            if (unlockDoubleJump == true)
            {
                abilitiesTracker.canDoubleJump = true;
                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
            }

            Instantiate(abilitiesVFX, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.position = transform.position;
            unlockText.transform.Translate(0, 7, 0);

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            AudioManager.Instance.PlaySFX(5);

            Destroy(unlockText.transform.parent.gameObject, 2.5f);

            Destroy(gameObject);
        }
        
    }

}
