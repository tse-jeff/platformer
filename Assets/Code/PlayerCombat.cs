using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public TextMeshProUGUI StarText;

    public Transform attackPoint;
    public Vector2 meleeRange = new Vector2(2.12f, 2.86f);
    public LayerMask enemyLayers;
    public Animator animator;
    public GameObject Stars;
    int starForce = 900;

    public Slider healthBar;

    AudioSource _audioSource;
    public AudioClip swordSound;
    public AudioClip hurtSound;
    public AudioClip deflectSound;
    public AudioClip deathSound;
    public float volume = 0.5f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        healthBar.value = 1;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = (float)((double)hp / 3);
    }

    void Update()
    {
        // death
        if (PublicVars.playerHealth < 1)
        {
            if (gameObject.tag != "Dead")
            {
                PublicVars.lives--;
                _audioSource.PlayOneShot(deathSound, volume);
                gameObject.tag = "Dead";
                StartCoroutine(Death());
            }
        }

        //Left click
        if (Input.GetButtonDown("Fire1"))
        {
            _audioSource.PlayOneShot(swordSound, volume);
            animator.SetTrigger("melee");
            MeleeAttack();
        }

        //Right click
        else if (Input.GetButtonDown("Fire2"))
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
            print(enemy.gameObject);
            if (enemy.gameObject.tag == "Arrow")
            {
                _audioSource.PlayOneShot(deflectSound, volume);
                Destroy(enemy.gameObject);
            }
            else if (enemy.gameObject.tag == "Enemy")
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

    public void TakeDamage()
    {
        _audioSource.PlayOneShot(hurtSound, volume);
        animator.SetTrigger("hurt");
        PublicVars.playerHealth -= 1;
        SetHealth(PublicVars.playerHealth);
    }

    IEnumerator Death()
    {
        Destroy(GetComponent<PlayerMovement>());
        animator.SetBool("isJumping", false);
        animator.SetBool("alive", false);
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = new Vector2(0, -5);
        yield return new WaitForSeconds(2f);

        if (PublicVars.lives >= 0)
        {
            //Reload Scene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            PublicVars.playerHealth = 3;
            PublicVars.facingRight = true;
            PublicVars.stars = PublicVars.prevStars;
            PublicVars.score = PublicVars.prevScore;
        }
        else
        {
            SceneManager.LoadScene("Game Over");
        }

    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
        PublicVars.facingRight = !PublicVars.facingRight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, meleeRange);
    }
}
