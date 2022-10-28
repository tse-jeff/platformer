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
    public int jumpForce = 900;
    public int airjumps = 1;
    

    private Rigidbody2D _rigidbody;

    public bool grounded = false;
    public bool paused = false;


    AudioSource _audioSource;
    public AudioClip jumpSound;
    public float volume = 0.5f;



    float xSpeed = 0;
    

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //gravityStore = theRB.gravityScale;
        animator.SetBool("alive", true);

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

        // Determines if player character is facing the right direction
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


        // Checks to see if ninja is sliding on wall, if it is performs sliding animation
        if (isWallSliding)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, wallSlideSpeed, float.MaxValue));
            animator.SetBool("isOnWall", true);
            Flip();
        }
        else
        {
            animator.SetBool("isOnWall", false);
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
                _audioSource.PlayOneShot(jumpSound, volume+0.3f);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(new Vector2(0, jumpForce));
            }
        }
        else if (airjumps > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJumping", true);
                _audioSource.PlayOneShot(jumpSound, volume+0.3f);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
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

    // Flips character model, helps with fixing animation issues
    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
        PublicVars.facingRight = !PublicVars.facingRight;
    }

    // Plays hurt animation 
    public void HurtAnimation(GameObject enemy) {
        animator.SetTrigger("hurt");
    }


    // Pauses game 
    public void PauseGame()
    {     
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    // Resumes game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        paused = false;
    }

    // Returns player to main menu
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
