using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PublicVars.playerHealth = 0;
        }
        else if(other.gameObject.tag != "Dead")
        {
            Destroy(other.gameObject);
        }
    }
}
