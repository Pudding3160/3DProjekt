
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        // Make sure there is only one AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create AudioSources for each sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        Play("BGM");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called automatically whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sounds that should stop when changing scenes
        Stop("Walk");
        Stop("Sneak");
        Stop("Sprint");
    }

    // Play a sound
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (s != null && s.source != null && !s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    // Stop a specific sound
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        if (s != null && s.source != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

    // Stop every sound
    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            if (s.source != null && s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
    }

    // Check if a sound is currently playing
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        return s != null &&
               s.source != null &&
               s.source.isPlaying;
    }
}

