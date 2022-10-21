using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{


    [Header("Wall Jump")]
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.7f;
    bool isWallSliding = false;
    public bool isFacingRight;
    RaycastHit2D WallCheckHit;
    float jumpTime;


    public GameObject pauseMenuScreen;

    public Transform feetTrans;
    public LayerMask groundLayer;
    public Animator animator;

    public int speed = 5;
    public int sprintMultiplier = 2;
    public int jumpForce = 300;
    public int airjumps = 1;
    

    public Rigidbody2D _rigidbody;

    public bool grounded = false;
    public bool paused = false;



    float xSpeed = 0;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //gravityStore = theRB.gravityScale;  
        HurtAnimation(gameObject);


    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;

        if (xSpeed < 0)
        {
            isFacingRight = false;
        }
        else
        {
            isFacingRight = true;
        }


        if (Input.GetButton("Dash"))
        {
            xSpeed *= sprintMultiplier;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        animator.SetFloat("speed", Mathf.Abs(xSpeed));
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if (xSpeed < 0 && PublicVars.facingRight == true)
        {
            Flip();
        }
        if (xSpeed > 0 && PublicVars.facingRight == false)
        {
            Flip();
        }

        // Wall Jumping
        if (isFacingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
           
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
      
        }

        if (WallCheckHit && !grounded && xSpeed != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time){
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, wallSlideSpeed, float.MaxValue));
        }
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feetTrans.position, .1f, groundLayer);
        if (grounded || isWallSliding)
        {
            animator.SetBool("isJumping", false);
            airjumps = 1;
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJumping", true);
                _rigidbody.AddForce(new Vector2(0, jumpForce));
            }
        }
        else if (airjumps > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJumping", true);
                _rigidbody.AddForce(new Vector2(0, jumpForce));
                airjumps--;
            }
        }

        if (Input.GetButtonDown("Cancel") && !paused)
        {
            PauseGame();
            paused = true;
        }
        else if (Input.GetButtonDown("Cancel") && paused)
        {
            ResumeGame();
            paused = false;
        }

        
    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
        PublicVars.facingRight = !PublicVars.facingRight;
    }

    public void HurtAnimation(GameObject enemy) {
        animator.SetTrigger("hurt");
    }

    public void PauseGame()
    { 
        
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        paused = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
