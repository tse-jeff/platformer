using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpdateHealth : MonoBehaviour
{

    public Slider healthBar;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = 3;
        healthBar.value = 3;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
