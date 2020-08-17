using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGreenController : MonoBehaviour
{
    public static SlimeGreenController instance;
    private Rigidbody2D rigidBody;
    EnemyDamageController damageController;
    Animator thisAnimator;
    bool facingRight = false;

    public float runSpeed = 1.5f;
    private Vector3 velocity = Vector3.zero;
    Vector2 front = Vector2.zero;
    private float movementSmoothing = 0.005f;
    public GameObject target;

    private void Awake()
    {
        instance = this;
        damageController = this.GetComponent<EnemyDamageController>();
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
        if (damageController.goingDeath == true)
        {
            this.GetComponent<Collider2D>().enabled = false;
            rigidBody.bodyType = RigidbodyType2D.Static;
            runSpeed = 0;
            thisAnimator.SetBool("IsDead", true);
        }
    }

    private void FixedUpdate()
    {
        if (facingRight == false)
        {
            front = Vector2.left;
            rigidBody.velocity = new Vector2(-1 * runSpeed, rigidBody.velocity.y);
        }
        else
        {
            front = Vector2.right;
            rigidBody.velocity = new Vector2(1 * runSpeed, rigidBody.velocity.y);
        }

        if (FacingWall() == true)
        {
            Flip();
        }
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
        if (collision.gameObject.tag == "Player")
        {
            //rigidBody.AddForce(Vector2.right * 10000, ForceMode2D.Impulse);
            runSpeed = 0;
            //rigidBody.mass = 100;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //runSpeed = tempRunSpeed;
        rigidBody.mass = 10;
        runSpeed = 1.5f;
    }

    public LayerMask wallLayer;
    bool FacingWall()
    {
        if (Physics2D.Raycast(this.transform.position, front, 0.5f, wallLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
