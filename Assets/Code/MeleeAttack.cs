using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public Animator animator;

    public bool canAttack = true;

    AudioSource _audioSource;
    public AudioClip attackSound;
    public float volume = 0.5f;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        animator.SetBool("alive", true);
    }

    private void FixedUpdate() {
        if(gameObject.tag == "Dead")
        {
            Destroy(this);
        }
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
        _audioSource.PlayOneShot(attackSound, volume);
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(2f);
        canAttack = true;
        
        
    }
}
