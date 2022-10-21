using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public Animator animator;

    public bool canAttack = true;

    void Start()
    {
        animator.SetBool("alive", true);
    }


    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && canAttack == true)
        {
            other.gameObject.GetComponent<PlayerCombat>().TakeDamage();
            StartCoroutine(AttackPlayer());
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
