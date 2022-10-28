using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Stars;

    AudioSource _audioSource;
    public AudioClip collectStarSound;
    public AudioClip collectCoinSound;
    public float volume = 0.5f;

    // Start is called before the first frame update
    void Start() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        Stars.text = "STARS: " + PublicVars.stars;
        Score.text = "SCORE: " + PublicVars.score;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NinjaStarCollectable"))
        {
            _audioSource.PlayOneShot(collectStarSound, volume+0.3f);
            Destroy(other.gameObject);
            PublicVars.stars += 1;
            Stars.text = "STARS: " + PublicVars.stars;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            _audioSource.PlayOneShot(collectCoinSound, volume);
            Destroy(other.gameObject);
            PublicVars.score += 10;
            Score.text = "SCORE: " + PublicVars.score;
        }
    }
}
