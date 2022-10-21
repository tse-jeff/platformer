using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    public Animator animator;
    public LayerMask playerLayer; 
    
    public bool canAttack = true;
    public bool canLunge = true;
    public int dashForce = 600;

    Rigidbody2D dasher;
    RaycastHit2D ninjaLOS;


    void Start()
    {
        animator.SetBool("alive", true);
        Physics2D.queriesStartInColliders = false;
        dasher = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(gameObject.tag == "Dead")
        {
            Destroy(this);
        }
    }



    //Lunge
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && canLunge == true)
        {
            Vector2 ninjaPosition = other.transform.position - transform.position;
            RaycastHit2D ninjaLOS = Physics2D.Raycast(transform.position, ninjaPosition, playerLayer);
            Debug.DrawLine (transform.position, ninjaLOS.point, Color.red);

            //Dasher sees the ninja
            if(other.gameObject == ninjaLOS.transform.gameObject)
            {
                StartCoroutine(Lunge(ninjaPosition));
            }
            
        }
    }
    
    //Stop moving when dont see enemy
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            dasher.velocity = new Vector2(0,0);
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
    

    IEnumerator Lunge(Vector2 ninjaPosition)
    {
        canLunge = false;
        ninjaPosition.Normalize();
        dasher.AddForce(ninjaPosition * dashForce);
        yield return new WaitForSeconds(2f);
        canLunge = true;
    }

    IEnumerator AttackPlayer()
    {
        canAttack = false;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(2f);
        canAttack = true;
        
        
    }

}
