using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    
    public void LoadScene(string level)
    {
        PublicVars.lives = 5;
        PublicVars.score = 0;
        PublicVars.stars = 3;
        PublicVars.playerHealth = 3;

        PublicVars.prevScore = 0;
        PublicVars.prevStars = 3;

        PublicVars.facingRight = true;

        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    
}
