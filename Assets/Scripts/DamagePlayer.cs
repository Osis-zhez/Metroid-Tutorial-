using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;

    [SerializeField] bool destroyOnDamage;

    [SerializeField] GameObject deathVFX;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmount);

        if (destroyOnDamage)
        {   
            Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
