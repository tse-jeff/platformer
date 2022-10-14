using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float meleeRange = 0.52f;
    public LayerMask enemyLayers;

    void Update()
    {
        //Left click
        if (Input.GetMouseButtonDown(0))
        {
            MeleeAttack();
        }

        //Right click
        else if (Input.GetMouseButtonDown(1))
        {
            RangedAttack();
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange, enemyLayers);

        foreach (Collider2D enemy in hits)
        {
            enemy.GetComponent<EnemyCombat>().TakeMeleeDamage();
        }
    }

    void RangedAttack()
    {

    }
}
