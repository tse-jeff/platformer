using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int levelToLoad;
    public string sLevelToLoad;

    public bool useIntegerLoadLevel = false;
    public bool isTouching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTrigger2D(Collider2D collision)
    {
        isTouching = true;
       
        if (collision.tag == "Player")
        {

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
