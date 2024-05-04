using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    int currentPoint;

    [SerializeField] float moveSpeed, waitAtPoints;
    float waitCounter;

    [SerializeField] float jumpForce;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Animator anim;


    void Start()
    {
        waitCounter = waitAtPoints;

        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f) //если текущая точка пути находится дальше чем 0.2f, тогда нужно идти к этой точке. 
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // если точка справа, тогда идти вправо
                transform.localScale = new Vector3 (-1f, 1f, 1f); //нужно повернуться вправо
                
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // если точка слева, тогда идти влево
                transform.localScale = Vector3.one;                     //нужно повернуться влево

            }

            if (transform.position.y < patrolPoints[currentPoint].position.y - 0.5f && rb.velocity.y < 0.1f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else // если краб достиг точки
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoints;

                currentPoint = currentPoint +1;
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }

            }
        }

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

}
