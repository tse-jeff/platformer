using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAttack : MonoBehaviour
{
 public Animator animator;
    public LayerMask playerLayer; 
    
    public bool canAttack = true;
    public int walkForce = 50;

    Rigidbody2D walker;
    RaycastHit2D ninjaLOS;

    // Start
    void Start()
    {
        animator.SetBool("alive", true);
        Physics2D.queriesStartInColliders = false;
        walker = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(gameObject.tag == "Dead")
        {
            Destroy(this);
        }
    }


    //Walk
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Vector2 ninjaPosition = other.transform.position - transform.position;
            RaycastHit2D ninjaLOS = Physics2D.Raycast(transform.position, ninjaPosition, playerLayer);
            Debug.DrawLine (transform.position, ninjaLOS.point, Color.red);

            //Dasher sees the ninja
            ninjaPosition.Normalize();
            
            print(walker.velocity.x);
            if(walker.velocity.x < 7 && walker.velocity.x > -7)
            {
                walker.AddForce(ninjaPosition * walkForce);
            }
            
        }
    }

    //Stop moving when dont see enemy
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            walker.velocity = new Vector2(0,0);
        }
    }
        

    //Melee attacks
    private void OnCollisionStay2D(Collision2D other) {
        {
            if(other.gameObject.tag == "Player" && canAttack == true)
            {
                other.gameObject.GetComponent<PlayerCombat>().TakeDamage();
                StartCoroutine(AttackPlayer());
            }
        }
    }


    IEnumerator AttackPlayer()
    {
        canAttack = false;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(2f);
        canAttack = true;
        
        
    }

}
