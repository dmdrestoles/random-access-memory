using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sounds;

    public void Play(string name)
    {
        AudioSource s = Array.Find(sounds, audio => audio.clip.name.ToString() == name);

        try
        {
            s.Play();
        }
        catch
        {
            Debug.LogError("No such audio source with specified audio clip name!");
        }
    }

    public float ClipLength(string name)
    {
        AudioSource s = Array.Find(sounds, audio => audio.clip.name.ToString() == name);

        try
        {
            return s.clip.length;
        }
        catch
        {
            Debug.LogError("No such audio source with specified audio clip name!");
            return 0f;
        }

    }
}
