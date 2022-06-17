using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume = 0f;
    public static float soundEffectsVolume = 0f;

    private void Start()
    {
        //default music value is 25%
        OnMusicSliderValueChange(0.25f);
    }


    //sliders have an on value change event which pass you a float value to your function so it's required to accept a float parameter
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        AudioManager.audioManager.UpdateMixerVolume();
    }
    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;
        AudioManager.audioManager.UpdateMixerVolume();
    }
}
