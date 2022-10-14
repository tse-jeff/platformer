using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    float lifeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyCombat>().TakeRangedDamage();
        }
        if(other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}