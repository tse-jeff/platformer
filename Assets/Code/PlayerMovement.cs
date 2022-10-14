using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    
    //public Rigidbody2D theRB;
    //private float gravityStore;
    //public float wallJumpTime = .2f;
    //private float wallJumpCounter;
    //public Transform wallGrabPoint;
    //private bool canGrab, isGrabbing;


    public Transform feetTrans;
    public LayerMask groundLayer;


    public int speed = 5;
    public int jumpForce = 300;
    public int airjumps = 1;
    

    Rigidbody2D _rigidbody;

    public bool grounded = false;



    float xSpeed = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //gravityStore = theRB.gravityScale;  
    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if (xSpeed < 0 && PublicVars.facingRight == true)
        {
            Flip();
        }
        if (xSpeed > 0 && PublicVars.facingRight == false)
        {
            Flip();
        }
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feetTrans.position, .1f, groundLayer);
        if (grounded)
        {
            airjumps = 1;
            if (Input.GetButtonDown("Jump"))
            {
                _rigidbody.AddForce(new Vector2(0, jumpForce));
            }
        }
        else if (airjumps > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rigidbody.AddForce(new Vector2(0, jumpForce));
                airjumps--;
            }
        }

        //Incomplete Wall Jump Codes
        /*
        if(wallJumpCounter <= 0)
        {
            //Handle Wall Jumping
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, 0.2f, groundLayer);

            isGrabbing = false;
            if (canGrab && !grounded)
            {
                if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
                {
                    isGrabbing = true;
                }

            }


            if (isGrabbing)
            {
                theRB.gravityScale = 0f;
                theRB.velocity = Vector2.zero;

                if (Input.GetButtonDown("Jump")) {
                    wallJumpCounter = wallJumpTime;
                    theRB.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * speed, jumpForce);
                    theRB.gravityScale = gravityStore;
                    isGrabbing = false;
                }
            }
            else
            {
                theRB.gravityScale = gravityStore;
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }
        */
    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
        PublicVars.facingRight = !PublicVars.facingRight;
    }
}
