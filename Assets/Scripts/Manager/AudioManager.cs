using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager = null;

    //SOUNDS
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
    [SerializeField] private Sound[] sounds;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private void Awake()
    {
        MakeSingleton();

        // set the volume and slider from PlyerPrefabs
        //changing these values will also causee the onValueChange functions to be triggered in AudioOptionsManager.cs
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.4f);
        Debug.Log("reading values from playerprefs about volume!");

        Debug.Log("SFX slider value: " + sfxSlider.value);

        foreach (Sound sound in sounds)
        { 
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.isLoop;
            sound.source.playOnAwake = sound.playOnAwake;
            //sound.source.volume = sound.volume;

            //we set the AudioSources into correct MixerGroups
            switch (sound.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    sound.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    sound.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (sound.playOnAwake)
            {
                sound.source.Play();
            }
        }
    }
    void MakeSingleton()
    {
        if (audioManager == null)
            audioManager = this;
        else if (audioManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(SoundSystem sound)
    {
        switch (sound)
        {
            case SoundSystem.Main_menu:
                PlayClipByIndex((int)SoundSystem.Main_menu);
                break;
            case SoundSystem.Jump:
                PlayClipByIndex((int)SoundSystem.Jump);
                break;
            case SoundSystem.Grunt:
                PlayClipByIndex((int)SoundSystem.Grunt);
                break;
            case SoundSystem.Die:
                PlayClipByIndex((int)SoundSystem.Die);
                break;
            case SoundSystem.Player_swing:
                PlayClipByIndex((int)SoundSystem.Player_swing);
                break;
            case SoundSystem.Player_hit:
                PlayClipByIndex((int)SoundSystem.Player_hit);
                break;
            case SoundSystem.Player_running:
                PlayClipByIndex((int)SoundSystem.Player_running);
                break;
            case SoundSystem.Coin_pick_up:
                PlayClipByIndex((int)SoundSystem.Coin_pick_up);
                break;
            default:
                Debug.Log("Sound to play not found!");
                break;
        }
    }

    public void StopSound(SoundSystem sound)
    {
        switch (sound)
        {
            case SoundSystem.Main_menu:
                StopClipByIndex((int)SoundSystem.Main_menu);
                break;
            case SoundSystem.Jump:
                StopClipByIndex((int)SoundSystem.Jump);
                break;
            case SoundSystem.Grunt:
                StopClipByIndex((int)SoundSystem.Grunt);
                break;
            case SoundSystem.Die:
                StopClipByIndex((int)SoundSystem.Die);
                break;
            case SoundSystem.Player_swing:
                StopClipByIndex((int)SoundSystem.Player_swing);
                break;
            case SoundSystem.Player_hit:
                StopClipByIndex((int)SoundSystem.Player_hit);
                break;
            case SoundSystem.Player_running:
                StopClipByIndex((int)SoundSystem.Player_running);
                break;
            case SoundSystem.Coin_pick_up:
                StopClipByIndex((int)SoundSystem.Coin_pick_up);
                break;
            default:
                break;
        }
    }

    void PlayClipByIndex(int index)
    {
        sounds[index].source.Play();
    }

    void StopClipByIndex(int index)
    {
        sounds[index].source.Stop();
    }

    public void UpdateMusicVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", AudioOptionsManager.musicVolume);
        PlayerPrefs.Save();
    }

    public void UpdateSfxVolume()
    {
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
        PlayerPrefs.SetFloat("SfxVolume", AudioOptionsManager.soundEffectsVolume);
        PlayerPrefs.Save();
    }

    public enum SoundSystem
    {
        Main_menu,
        Jump,
        Grunt,
        Die,
        Player_swing,
        Player_hit,
        Player_running,
        Coin_pick_up
    }
}
