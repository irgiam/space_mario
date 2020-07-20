using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rigidBody;
    Animator thisAnimator;
    public PlayerDamageController damageController;


    bool facingRight = true;
    public float jumpForce = 16f;
    public float runSpeed = 10f;
    float horizontalMove;
    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = 0.005f;

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.05f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.0f;
    public bool startBlinking = false;

    private void Awake()
    {
        instance = this;
        rigidBody = this.GetComponent<Rigidbody2D>();
        thisAnimator = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        thisAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove * runSpeed));
        thisAnimator.SetBool("IsJumping", !IsGrounded());
        if (horizontalMove < 0 && facingRight == true)
        {
            Flip();
        }
        else if (horizontalMove > 0 && facingRight == false)
        {
            Flip();
        }

        if (startBlinking == true)
        {
            SpriteBlinkingEffect();
        }
    }

    private void FixedUpdate()
    {
        HanddleMovement(horizontalMove);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //AudioManager.instance.PlayJump();
        }
    }

    public LayerMask groundLayer;

    bool IsGrounded()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1f, groundLayer.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HanddleMovement(float horizontal)
    {
        //normal movement
        //rigidBody.velocity = new Vector2(horizontal * runSpeed, rigidBody.velocity.y);

        //smooth movement
        Vector3 targetVelocity = new Vector2(horizontalMove * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing, Mathf.Infinity, Time.deltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            KnockBack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            startBlinking = true;
            TakenDamage();
        }
    }

    void TakenDamage()
    {
        LoseLive();
        Debug.Log("damage taken");
    }

    public void KnockBack()
    {
        //rigidBody.AddForce((Vector2.up * jumpForce) / 2, ForceMode2D.Impulse);
        Vector2 upRight = new Vector2(1, 1);
        Vector2 upLeft = new Vector2(-1, 1);
        if (facingRight == true)
        {
            rigidBody.AddForce(upRight * 5f, ForceMode2D.Impulse);
            //rigidBody.AddForce(new Vector2(1*jumpForce, 1*jumpForce));
        }
        else
        {
            rigidBody.AddForce(upLeft * 5f, ForceMode2D.Impulse);
            //rigidBody.AddForce(new Vector2(-1 * jumpForce, 1 * jumpForce));
        }
    }

    public void Attack()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 
                                                                             //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }

    void LoseLive()
    {
        if (InGameView.instance.playerLives.Count != 0)
        {
            Destroy(InGameView.instance.playerLives[InGameView.instance.playerLives.Count - 1]);
            GameObject losenLive = InGameView.instance.playerLives[InGameView.instance.playerLives.Count - 1];
            InGameView.instance.playerLives.Remove(losenLive);
        }
    }
}
