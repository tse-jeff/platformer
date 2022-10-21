using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Stars;
    // Start is called before the first frame update
    void Start() {
        Stars.text = "STARS: " + PublicVars.stars;
        Score.text = "SCORE: " + PublicVars.score;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NinjaStarCollectable"))
        {
            Destroy(other.gameObject);
            PublicVars.stars += 1;
            Stars.text = "STARS: " + PublicVars.stars;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            PublicVars.score += 10;
            Score.text = "SCORE: " + PublicVars.score;
        }
    }
}
