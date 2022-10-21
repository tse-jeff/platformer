using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class PlayerCombat : MonoBehaviour
{

    public TextMeshProUGUI StarText;

    public Transform attackPoint;
    //public float meleeRange = 0.91f;
    public Vector2 meleeRange = new Vector2(30,30);
    public LayerMask enemyLayers;
    public Animator animator;
    public GameObject Stars;
    int starForce = 900;

    void Update()
    {
        if (PublicVars.playerHealth < 1)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            PublicVars.playerHealth = 3;
            PublicVars.facingRight = true;
            PublicVars.stars = 3;

        }

        //Left click
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("melee");
            MeleeAttack();
        }

        //Right click
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("throw");
            RangedAttack();
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, meleeRange, enemyLayers);

        foreach (Collider2D enemy in hits)
        {
            if(enemy.gameObject.tag == "Arrow")
            {
                Destroy(enemy.gameObject);
            }
            else if(enemy.gameObject.tag == "Enemy")
            {
                enemy.GetComponent<EnemyCombat>().TakeMeleeDamage();
            }
            
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

    public void TakeDamage(){
        animator.SetTrigger("hurt");
        PublicVars.playerHealth -= 1;;
    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
        PublicVars.facingRight = !PublicVars.facingRight;
    }
    
    private void OnDrawGizmosSelected() {
        //Gizmos.DrawWireSphere(attackPoint.position, meleeRange);\
        Gizmos.DrawWireCube(attackPoint.position, meleeRange);
    }
}
