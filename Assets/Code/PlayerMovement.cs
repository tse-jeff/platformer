using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI StarText;
    public Transform feetTrans;
    public LayerMask groundLayer;

    public int speed = 5;
    public int jumpForce = 300;
    public int airjumps = 1;

    Rigidbody2D _rigidbody;

    public bool grounded = false;
    public bool facingRight = true;
    float xSpeed = 0;

    public GameObject Stars;
    int starForce = 900;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if (xSpeed < 0 && facingRight == true)
        {
            Flip();
        }
        if (xSpeed > 0 && facingRight == false)
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

        if (Input.GetMouseButtonDown(1))
        {
            if (PublicVars.stars != 0)
            {
                PublicVars.stars -= 1;
                StarText.text = "STARS: " + PublicVars.stars;
                GameObject collectedStar = Instantiate(Stars, transform.position, transform.rotation);
                if (facingRight)
                {
                    collectedStar.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * starForce);
                }
                else
                {
                    collectedStar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * starForce);
                }
            }
        }
    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
        facingRight = !facingRight;
    }
}
