using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource current;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        current = sounds[0];
        current.Play();
    }

    public void SwapMusic(string name)
    {
        StopAllCoroutines();

        AudioSource s = Array.Find(sounds, audio => audio.clip.name.ToString() == name);

        StartCoroutine(FadeMusic(s));

        current = Array.Find(sounds, audio => audio.clip.name.ToString() == name);
    }

    IEnumerator FadeMusic(AudioSource s)
    {
        float timeToFade = 0.25f;
        float timeElapsed = 0;

        while(timeElapsed < timeToFade)
        {
            s.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            current.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        current.Stop();
    }
}