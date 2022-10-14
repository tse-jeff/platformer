using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int health = 5;
    public bool canAttack = true;

    void Start()
    {
        
    }

    void Update()
    {
        if(health < 1)
        {
            gameObject.tag = "Dead";
            //Add death Animation
            StartCoroutine(DestroyEnemy());
        }
        
    }


    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player" && canAttack == true)
        {
            StartCoroutine(AttackPlayer());
        }
    }
    
    IEnumerator AttackPlayer()
    {
        canAttack = false;
        PublicVars.playerHealth -= 1;
        yield return new WaitForSeconds(2f);
        canAttack = true;
        
        
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        
    }


    public void TakeMeleeDamage()
    {
        health -= 1;
    }

    public void TakeRangedDamage()
    {
        health -= 5;
    }
}
