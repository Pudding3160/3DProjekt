using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Start()
    {
        Play("BGM");
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else { 
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop= s.loop;  
            s.source.playOnAwake=s.playOnAwake;

        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (s != null && !s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (s != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
    }



}