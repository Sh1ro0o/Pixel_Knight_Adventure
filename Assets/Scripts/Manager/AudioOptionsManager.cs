using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume;
    public static float soundEffectsVolume;

    //sliders have an on value change event which pass you a float value to your function so it's required to accept a float parameter
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        AudioManager.audioManager.UpdateMusicVolume();
    }
    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;
        AudioManager.audioManager.UpdateSfxVolume();
    }
}
