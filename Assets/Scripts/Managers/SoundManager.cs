using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSourceControl[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (AudioSourceControl s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.is_loop;
        }
    }

    public void Stop(string name)
    {
        AudioSourceControl s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            //Debug.LogWarning("Sound" + name + "not found");
            return;
        }

        s.source.Stop();
    }

    public void Play(string name)
    {
        AudioSourceControl s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found");
            return;
        }
        s.source.Play();
    }
}
