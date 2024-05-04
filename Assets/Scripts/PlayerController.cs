using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    bool canDoubleJump;

    [SerializeField] Transform groundPoint;
    [SerializeField] LayerMask whatIsGround;
    bool isOnGround;

    [SerializeField] Animator animationPlayer;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotPoint;

    [SerializeField] float dashTimer = -1f;
    [SerializeField] float dashCooldown = 3f;
    [SerializeField] float dashSpeed, dashTime;
    float dashCounter;
    bool isDash = false;
    

    [Header("Dash After Image info")]
    [SerializeField] SpriteRenderer theSR, afterImage;
    [SerializeField] float afterImageLifeTime, timeBeetweenAfterImages;
    float afterImageCounter;
    [SerializeField] Color afterImageColor;

    PlayerAbilityTracker abilities;

    
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
    }


    void Update()
    {
        if (Time.timeScale == 0) { return; }
        Move();
        Dash();
        Jump();
        Fire();
        Flipping();
        Animations();
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    void Move()
    {
        if (isDash) { return; }

        myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal")  * moveSpeed, myRigidbody.velocity.y);
        
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);
        isOnGround = Physics2D.OverlapBox(groundPoint.position, new Vector2(0.5f, 0), 0, whatIsGround);
    }

    void Dash()
    {
        if (Input.GetButtonDown("Fire2") && dashTimer < 0 && abilities.canDash == true) 
        {
            dashCounter = dashTime;
            dashTimer = dashCooldown;

            AudioManager.Instance.PlaySFX(7);

            ShowAfterImage();
        }

        dashTimer -= Time.deltaTime;  

        if (dashCounter > 0)
        {
            dashCounter = dashCounter - Time.deltaTime;
            isDash = true;

            myRigidbody.velocity = new Vector2(dashSpeed * transform.localScale.x, myRigidbody.velocity.y);

            afterImageCounter = afterImageCounter - Time.deltaTime;
            if (afterImageCounter <= 0)
            {
                ShowAfterImage();
            }
        }
        else
        {
            isDash = false;
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump == true))) 
        {
            if (isOnGround) // первый прыжок
            {
                AudioManager.Instance.PlaySFXAdjucted(12);

                canDoubleJump = true;
            }
            else if (isOnGround == false) // второй прыжок
            {
                canDoubleJump = false;

                AudioManager.Instance.PlaySFXAdjucted(9);

                animationPlayer.SetTrigger("doubleJump");
            }
            
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);

            animationPlayer.SetTrigger("shotFired");
        }
    }

    void Flipping()
    {
        if(myRigidbody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(myRigidbody.velocity.x > 0)
        {
            transform.localScale = Vector3.one; 
        }
    }

    void Animations()
    {
        animationPlayer.SetBool("isOnGround", isOnGround);
        animationPlayer.SetFloat("speed",Mathf.Abs(myRigidbody.velocity.x));
    }

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.sortingLayerName = "Player";
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifeTime);

        afterImageCounter = timeBeetweenAfterImages;
    }

    public void SetJumpForce(float setJumpForce)
    {
        jumpForce = jumpForce + setJumpForce;
    }
    
}
