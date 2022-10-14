using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMetrics : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Stars;

    public void UpdateScore(int points)
    {
        PublicVars.score += points;
        Score.text = "SCORE: " + PublicVars.score;
    }

    public void UpdateStars(int count)
    {
        PublicVars.stars += count;
        Stars.text = "STARS: " + PublicVars.stars;
    }
}
