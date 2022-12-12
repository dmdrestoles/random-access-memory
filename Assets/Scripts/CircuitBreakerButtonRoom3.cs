using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBreakerButtonRoom3 : MonoBehaviour
{
    [SerializeField] private char buttonColor;
    [SerializeField] private GameObject[] lights1;
    [SerializeField] private GameObject[] lights2;
    [SerializeField] private GameObject[] lights3;
    [SerializeField] private GameObject[] lights4;
    private static Coroutine coroutineButtonCooldown = null;
    private static bool isGameFinished = false;
    private static bool isAudioPlayed = false;

    public AudioManager audioManager;

    // Use this to start the simon says game
    public void StartGame()
    {
        lights1[1].GetComponent<Light>().enabled = true;
        lights1[0].GetComponent<Light>().enabled = false;
        lights2[1].GetComponent<Light>().enabled = true;
        lights2[0].GetComponent<Light>().enabled = false;
        lights3[1].GetComponent<Light>().enabled = true;
        lights3[0].GetComponent<Light>().enabled = false;
        lights4[1].GetComponent<Light>().enabled = true;
        lights4[0].GetComponent<Light>().enabled = false;
        coroutineButtonCooldown = null;

        if (!isAudioPlayed)
        {
            audioManager.Play("thunder");
            audioManager.Play("circuitoff");
            isAudioPlayed = true;
        }
        SimonSaysRoom3.simonSays.StartGame();
    }

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        Debug.Log(audioManager);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameFinished)
            return;

        Debug.Log("Button Pressed!");
        char reply;
        if (other.CompareTag("Player") && coroutineButtonCooldown == null)
        {
            switch (buttonColor)
            {
                case 'r':
                    FindObjectOfType<AudioManager>().Play("toy00");
                    break;
                case 'g':
                    FindObjectOfType<AudioManager>().Play("toy01");
                    break;
                case 'b':
                    FindObjectOfType<AudioManager>().Play("toy02");
                    break;
                case 'y':
                    FindObjectOfType<AudioManager>().Play("toy03");
                    break;
                default:
                    Debug.LogWarning("Unkown button color how did u get here");
                    break;
            }
            reply = SimonSaysRoom3.simonSays.InputPress(buttonColor);
        }
        else
        {
            return;
        }
        coroutineButtonCooldown = StartCoroutine(ButtonCooldown());

        switch (reply)
        {
            case 'c':
                break;
            case 'w':
                lights1[1].GetComponent<Light>().enabled = true;
                lights1[0].GetComponent<Light>().enabled = false;
                lights2[1].GetComponent<Light>().enabled = true;
                lights2[0].GetComponent<Light>().enabled = false;
                lights3[1].GetComponent<Light>().enabled = true;
                lights3[0].GetComponent<Light>().enabled = false;
                lights4[1].GetComponent<Light>().enabled = true;
                lights4[0].GetComponent<Light>().enabled = false;
                break;
            case 'f':
                switch (SimonSaysRoom3.simonSays.levelsFinished)
                {
                    case 1:
                        lights1[1].GetComponent<Light>().enabled = false;
                        lights1[0].GetComponent<Light>().enabled = true;
                        break;
                    case 2:
                        lights2[1].GetComponent<Light>().enabled = false;
                        lights2[0].GetComponent<Light>().enabled = true;
                        break;
                    case 3:
                        lights3[1].GetComponent<Light>().enabled = false;
                        lights3[0].GetComponent<Light>().enabled = true;
                        break;
                    case 4:
                        lights4[1].GetComponent<Light>().enabled = false;
                        lights4[0].GetComponent<Light>().enabled = true;
                        isGameFinished = true;
                        SimonSaysRoom3.simonSays.FinishGame();
                        break;
                    case 5:
                        // Ignore
                        break;
                    default:
                        Debug.LogWarning("Invalid levelsFinished integer in CircuitBreakerButton script!");
                        break;
                }
                break;
            default:
                Debug.LogWarning("Unknown simon says button response! " + reply);
                break;
        }
    }

    IEnumerator ButtonCooldown()
    {
        yield return new WaitForSeconds(1f);
        coroutineButtonCooldown = null;
    }
}
