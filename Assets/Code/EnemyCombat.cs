using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int health = 15;
    public Animator animator;
    public GameObject player;
    public bool enemyFacingRight = false;
    public Vector2 playerDirection;

    AudioSource _audioSource;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public float volume = 0.5f;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        animator.SetBool("alive", true);
    }

    void Update()
    {
        if(player != null){
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
        
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        
    }


    public void TakeMeleeDamage()
    {
        animator.SetTrigger("hurt");
        health -= 15;
        if(health < 1){
            _audioSource.PlayOneShot(hurtSound, volume);
        }
        else{
            _audioSource.PlayOneShot(deathSound, volume);
        }
    }

    public void TakeRangedDamage()
    {
        animator.SetTrigger("hurt");
        health -= 5;
        if(health < 1){
            _audioSource.PlayOneShot(hurtSound, volume);
        }
        else{
            _audioSource.PlayOneShot(deathSound, volume);
        }
    }

    void Flip()
    {
        Vector3 currDirection = gameObject.transform.localScale;
        currDirection.x *= -1;
        gameObject.transform.localScale = currDirection;
    }
}
