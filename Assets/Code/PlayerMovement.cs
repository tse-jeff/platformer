using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    }

    void FixedUpdate() {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feetTrans.position, .1f, groundLayer);

        if(grounded){
            airjumps = 1;
            if(Input.GetButtonDown("Jump"))
            {
                _rigidbody.AddForce(new Vector2(0, jumpForce));
            }
        }
        else if(airjumps > 0)
        {
            if(Input.GetButtonDown("Jump"))
            {
                _rigidbody.AddForce(new Vector2(0, jumpForce));
                airjumps--;
            }
        }
    }

}
