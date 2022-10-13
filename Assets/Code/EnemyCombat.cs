using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int health = 5;


    void Start()
    {
        
    }

    void Update()
    {
        if(health < 1)
        {
            StartCoroutine(DestroyEnemy());
        }
        
    }

    public void TakeMeleeDamage()
    {
        health -= 1;
    }

    public void TakeRangedDamage()
    {
        health -= 5;
    }

    IEnumerator DestroyEnemy(){
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        
    }
}
