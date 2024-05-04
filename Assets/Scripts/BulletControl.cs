using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] Rigidbody2D rb;
    PlayerController player;

    float xSpeed;

    [SerializeField] GameObject impact;

    [SerializeField] int damageAmount = 1;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        xSpeed = player.transform.localScale.x * bulletSpeed;

        AudioManager.Instance.PlaySFXAdjucted(14);
    }

    
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealtController>().damageEnemy(damageAmount);
        }

        if (other.tag == "Boss")
        {
            BossHealthController.Instance.TakeDamage(damageAmount);
        }

        if (impact != null)
        {
            Instantiate(impact, transform.position, Quaternion.identity);
        }
        
        AudioManager.Instance.PlaySFXAdjucted(3);

        Destroy(gameObject);
    }

    void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
