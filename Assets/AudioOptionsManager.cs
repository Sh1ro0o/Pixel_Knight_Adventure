using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptionsManager : MonoBehaviour
{
    //we make set private so we can't change their values outside of this class because they would be able to otherwise because they are static
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }


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
