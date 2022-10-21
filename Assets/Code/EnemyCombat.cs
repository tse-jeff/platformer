using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int health = 5;
    public Animator animator;
    public GameObject player;
    public bool enemyFacingRight = false;
    public Vector2 playerDirection;

    void Start()
    {
        animator.SetBool("alive", true);
    }

    void Update()
    {
        playerDirection = (player.transform.position - transform.position).normalized;

        if(playerDirection.x < 0 && enemyFacingRight == true)
        {
            Flip();
            enemyFacingRight = false;
        }

        else if(playerDirection.x > 0 && enemyFacingRight == false)
        {
            Flip();
            enemyFacingRight = true;
        }


        if(health < 1)
        {
            gameObject.tag = "Dead";
            animator.SetBool("alive", false);
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(DestroyEnemy());
        }
        
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        
    }


    public void TakeMeleeDamage()
    {
        animator.SetTrigger("hurt");
        health -= 10;
    }

    public void TakeRangedDamage()
    {
        animator.SetTrigger("hurt");
        health -= 5;
    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
    }
}
