using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager = null;

    //SOUNDS
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
    [SerializeField] private Sound[] sounds;


    private void Awake()
    {
        MakeSingleton();

        foreach (Sound sound in sounds)
        { 
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.isLoop;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.volume = sound.volume;

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
                sound.source.Play();
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
            case SoundSystem.Sword_hit:
                PlayClipByIndex((int)SoundSystem.Sword_hit);
                break;
            case SoundSystem.Sword_swing:
                PlayClipByIndex((int)SoundSystem.Sword_swing);
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
            case SoundSystem.Sword_hit:
                StopClipByIndex((int)SoundSystem.Sword_hit);
                break;
            case SoundSystem.Sword_swing:
                StopClipByIndex((int)SoundSystem.Sword_swing);
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

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
    }

    public enum SoundSystem
    {
        Main_menu,
        Jump,
        Grunt,
        Die,
        Sword_hit,
        Sword_swing
    }
}
