using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] int damageAmount;
    [SerializeField] GameObject impactEffect;

    void Start()
    {
        Vector3 direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        AudioManager.Instance.PlaySFX(2);
    }

    
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
        }

        if(impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        AudioManager.Instance.PlaySFXAdjucted(3);
    }
}
