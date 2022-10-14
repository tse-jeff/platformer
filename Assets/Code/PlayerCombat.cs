using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class PlayerCombat : MonoBehaviour
{

    public TextMeshProUGUI StarText;

    public Transform attackPoint;
    public float meleeRange = 0.52f;
    public LayerMask enemyLayers;

    public GameObject Stars;
    int starForce = 900;

    void Update()
    {
        if (PublicVars.playerHealth < 1)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            PublicVars.playerHealth = 3;

        }

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

        if (PublicVars.stars != 0)
        {
            PublicVars.stars -= 1;
            StarText.text = "STARS: " + PublicVars.stars;
            GameObject collectedStar = Instantiate(Stars, transform.position, transform.rotation);

            if (PublicVars.facingRight)
            {
                collectedStar.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * starForce);
            }
            else
            {
                collectedStar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * starForce);
            }

            
        }

    }
}
