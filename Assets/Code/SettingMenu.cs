using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{

    //public AudioMixer audio;

    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        //audio.SetFloat("volume", volume);
    }
}
