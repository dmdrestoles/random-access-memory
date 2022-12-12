using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerAudioManager : MonoBehaviour
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
}
