using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    float lifeTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerCombat>().TakeDamage();
        }
        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "Dead" && other.gameObject.tag != "Coin" && other.gameObject.tag != "Range")
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // if xspeed is negative, flip the sprite
        if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
