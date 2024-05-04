using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealtController : MonoBehaviour
{
    [SerializeField] int totalHealth = 3;
    [SerializeField] GameObject deathVFX;

    public void damageEnemy(int damageAmount)
    {
        totalHealth = totalHealth - damageAmount;

        if (totalHealth <= 0)
        {
            if (deathVFX != null)
            {
                Instantiate(deathVFX, transform.position, transform.rotation);
            }

            AudioManager.Instance.PlaySFX(4);

            Destroy(gameObject);
        }
    }

 
}
