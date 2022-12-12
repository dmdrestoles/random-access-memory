using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private bool isPlayed = false;
    public void PlayAudio()
    {
        if (!isPlayed)
        {
            FindObjectOfType<CoworkerAudioManager>().Play("coworker06");
            isPlayed = true;
        }
    }
}
