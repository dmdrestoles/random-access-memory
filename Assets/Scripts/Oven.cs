using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Oven : MonoBehaviour
{
    [SerializeField] private GameObject circuitBreaker;
    [SerializeField] private TextMeshProUGUI ovenText;
    [SerializeField] private GameObject traySpawns;
    [SerializeField] private GameObject cookiePrefab;
    [SerializeField] private GameObject cookeiKeyPrefab;

    private bool isTrayInOven = false;
    private bool audioPlayed = false;
    private Coroutine coroutineBake = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tray"))
        {
            isTrayInOven = true;

            if (SceneManager.GetActiveScene().buildIndex == 3)
                return;

            if (!SimonSays.simonSays.isRunning && !SimonSays.simonSays.isComplete)
            {
                circuitBreaker.GetComponentInChildren<CircuitBreakerButton>().StartGame();
                FindObjectOfType<GrandmaAudioManager>().Play("grandma03");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Tray"))
        {
            isTrayInOven = false;
        }
    }

    private void Update()
    {
        try
        {
            if (SimonSays.simonSays.isComplete && isTrayInOven && int.Parse(ovenText.text) == 200)
            {
                if (coroutineBake == null)
                {
                    coroutineBake = StartCoroutine(BakeCookies());
                }
            }
        }
        catch
        {
            if (SimonSaysRoom3.simonSays.isComplete && isTrayInOven && int.Parse(ovenText.text) == 200)
            {
                if (coroutineBake == null)
                {
                    coroutineBake = StartCoroutine(BakeCookies());
                }
            }
        }
    }

    public void PlayCoworkerRoom2End()
    {
        FindObjectOfType<CoworkerAudioManager>().Play("coworker04");
    }

    IEnumerator BakeCookies()
    {
        for (int i = 0; i < traySpawns.transform.childCount; i++)
        {
            if (i < (traySpawns.transform.childCount - 1))
            {
                Instantiate(cookiePrefab, traySpawns.transform.GetChild(i).position, traySpawns.transform.GetChild(i).rotation);
            }
            else
            {
                Instantiate(cookeiKeyPrefab, traySpawns.transform.GetChild(i).position, traySpawns.transform.GetChild(i).rotation);
                FindObjectOfType<Tray>().CookedAnimation();

                if (SceneManager.GetActiveScene().buildIndex != 3)
                {
                    FindObjectOfType<GrandmaAudioManager>().Play("grandma04");
                    Invoke("PlayCoworkerRoom2End", FindObjectOfType<GrandmaAudioManager>().ClipLength("grandma04"));
                }
            }
        }
        yield return null;
    }
}
