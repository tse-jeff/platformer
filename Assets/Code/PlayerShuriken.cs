using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShuriken : MonoBehaviour
{
    float lifeTime = 2;

    AudioSource _audioSource;
    public AudioClip starSound;
    public float volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.PlayOneShot(starSound, volume);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyCombat>().TakeRangedDamage();
        }
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Dead" && other.gameObject.tag != "Coin" && other.gameObject.tag != "NinjaStarCollectable" && other.gameObject.tag != "Range")
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