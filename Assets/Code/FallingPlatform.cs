using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeOut = .3f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("something");
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timeOut);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
