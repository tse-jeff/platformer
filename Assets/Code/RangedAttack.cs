using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public Animator animator;
    public LayerMask playerLayer; 
    public GameObject arrows;
    int arrowForce = 400;
    
    public bool canAttack = true;

    RaycastHit2D ninjaLOS;

    AudioSource _audioSource;
    public AudioClip attackSound;
    public float volume = 0.5f;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        animator.SetBool("alive", true);
        Physics2D.queriesStartInColliders = false;
    }

    private void FixedUpdate() {
        if(gameObject.tag == "Dead")
        {
            Destroy(this);
        }
    }

    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && canAttack == true)
        {
            Vector2 ninjaPosition = other.transform.position - transform.position;
            RaycastHit2D ninjaLOS = Physics2D.Raycast(transform.position, ninjaPosition, playerLayer);
            Debug.DrawLine (transform.position, ninjaLOS.point, Color.red);
            //Archer sees the ninja
            if(other.gameObject == ninjaLOS.transform.gameObject)
            {
                StartCoroutine(AttackPlayer(ninjaPosition));
            }
            
        }
    }
    

    IEnumerator AttackPlayer(Vector2 ninjaPosition)
    {
        canAttack = false;
        animator.SetTrigger("attack");
        _audioSource.PlayOneShot(attackSound, volume);
        sendArrow(ninjaPosition);
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
        
        
    }

    void sendArrow(Vector2 ninjaPosition){
        ninjaPosition.Normalize();
        GameObject arrow = Instantiate(arrows, transform.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().AddForce(ninjaPosition * arrowForce);
    }
}
