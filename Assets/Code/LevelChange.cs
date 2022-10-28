using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int levelToLoad;
    public string sLevelToLoad;

    public bool useIntegerLoadLevel = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PublicVars.prevScore = PublicVars.score;
            PublicVars.prevStars = PublicVars.stars;
            PublicVars.playerHealth = 3;
            LoadScene();
        }
    }

    void LoadScene()
    {
        if(useIntegerLoadLevel)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
