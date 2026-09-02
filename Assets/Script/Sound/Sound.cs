using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound 
{

    public string Name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool playOnAwake=false;
   

    [HideInInspector]
    public AudioSource source;
    
}
