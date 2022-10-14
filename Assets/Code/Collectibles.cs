using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NinjaStar"))
        {
            Destroy(other.gameObject);
            PublicVars.stars += 1;
        }
    }
}
