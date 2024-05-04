using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthAmount;

    [SerializeField] GameObject pickupEffect;

    private PlayerController playerController;

    private void Awake() 
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.HealPlayer(healthAmount);
    


            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }

            AudioManager.Instance.PlaySFX(5);

            Destroy(gameObject);
        }
    }
}
