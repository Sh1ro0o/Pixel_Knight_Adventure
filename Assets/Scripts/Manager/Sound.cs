using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AudioTypes
    {
        soundEffect,
        music
    }

    public AudioTypes audioType;

    [HideInInspector] public AudioSource source;
    public AudioClip clip;
    public string clipName;

    public bool isLoop = false;
    public bool playOnAwake = false;
    //[Range(0, 1)] public float volume = 0.5f;
}

