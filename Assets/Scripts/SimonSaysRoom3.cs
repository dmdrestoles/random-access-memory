using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimonSaysRoom3 : MonoBehaviour
{
    public GameObject simonSaysObject;
    [SerializeField] private GameObject lightBulb;
    [SerializeField] private Material[] redMaterials;
    [SerializeField] private Material[] greenMaterials;
    [SerializeField] private Material[] blueMaterials;
    [SerializeField] private Material[] yellowMaterials;

    public static SimonSaysRoom3 simonSays { get; private set; }
    public int levelsFinished { get; private set; }
    public bool isComplete { get; private set; }
    public bool isRunning { get; private set; }
    public bool isStarted { get; private set; }
    private static Coroutine coroutineShowPattern = null;
    private static int level = 0;
    private static string patternString = "";
    private static int inputIndex = 0;
    private static Light redLight;
    private static Light greenLight;
    private static Light blueLight;
    private static Light yellowLight;
    private static MeshRenderer redRenderer;
    private static MeshRenderer greenRenderer;
    private static MeshRenderer blueRenderer;
    private static MeshRenderer yellowRenderer;

    public AudioManager audioManager;

    private void Awake()
    {
        if (simonSays != null)
        {
            Debug.LogWarning("More than one instance of Simon Says!");
        }
        simonSays = this;

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            FindObjectOfType<CoworkerAudioManager>().Play("coworker05");
            return;
        }

        redLight = simonSaysObject.transform.GetChild(1).GetComponentInChildren<Light>();
        greenLight = simonSaysObject.transform.GetChild(4).GetComponentInChildren<Light>();
        blueLight = simonSaysObject.transform.GetChild(0).GetComponentInChildren<Light>();
        yellowLight = simonSaysObject.transform.GetChild(3).GetComponentInChildren<Light>();
        redRenderer = simonSaysObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        greenRenderer = simonSaysObject.transform.GetChild(4).GetComponent<MeshRenderer>();
        blueRenderer = simonSaysObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        yellowRenderer = simonSaysObject.transform.GetChild(3).GetComponent<MeshRenderer>();
    }

    public void AttachSimonSaysObject()
    {
        redLight = simonSaysObject.transform.GetChild(1).GetComponentInChildren<Light>();
        greenLight = simonSaysObject.transform.GetChild(4).GetComponentInChildren<Light>();
        blueLight = simonSaysObject.transform.GetChild(0).GetComponentInChildren<Light>();
        yellowLight = simonSaysObject.transform.GetChild(3).GetComponentInChildren<Light>();
        redRenderer = simonSaysObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        greenRenderer = simonSaysObject.transform.GetChild(4).GetComponent<MeshRenderer>();
        blueRenderer = simonSaysObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        yellowRenderer = simonSaysObject.transform.GetChild(3).GetComponent<MeshRenderer>();

        AudioSource[] audioSources = simonSaysObject.GetComponents<AudioSource>();
        audioManager.sounds[2] = audioSources[0];
        audioManager.sounds[3] = audioSources[1];
        audioManager.sounds[4] = audioSources[2];
        audioManager.sounds[5] = audioSources[3];
    }

    private void Start()
    {
        isComplete = false;
        isRunning = false;
        isStarted = false;

        if (SceneManager.GetActiveScene().buildIndex != 3)
            FindObjectOfType<CoworkerAudioManager>().Play("coworker02");
    }

    public void StartGame()
    {
        lightBulb.GetComponent<Light>().enabled = false;
        isStarted = true;

        if (simonSaysObject != null)
        {
            AttachSimonSaysObject();
            if (!isRunning)
            {
                isRunning = true;
                StartLevel();
            }
        }
        else
        {
            StartCoroutine(WaitForSimonSaysObject());
        }
    }

    IEnumerator WaitForSimonSaysObject()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (simonSaysObject != null)
            {
                AttachSimonSaysObject();
                break;
            }
        }
        StartGame();
    }

    public void FinishGame()
    {
        lightBulb.GetComponent<Light>().enabled = true;
        isRunning = false;
        isComplete = true;
        isStarted = false;
        level = 0;
        levelsFinished = 0;
        inputIndex = 0;
    }

    public void StartLevel()
    {
        Debug.Log("Starting Level!");
        patternString = GeneratePattern();
        coroutineShowPattern = StartCoroutine(ShowPattern(patternString));
    }

    private void FinishLevel()
    {
        Debug.Log("Level Finished!");
        StopCoroutine(coroutineShowPattern);
        coroutineShowPattern = null;
        ResetSimonSaysObject();
        level++;
        inputIndex = 0;
        levelsFinished++;
    }

    private void ResetGame()
    {
        Debug.Log("Resetting Game!");
        StopCoroutine(coroutineShowPattern);
        coroutineShowPattern = null;
        ResetSimonSaysObject();
        level = 0;
        inputIndex = 0;
        levelsFinished = 0;
        StartCoroutine(LevelCooldown());
    }

    private void ResetSimonSaysObject()
    {
        redRenderer.material = redMaterials[0];
        redLight.enabled = false;
        greenRenderer.material = greenMaterials[0];
        greenLight.enabled = false;
        blueRenderer.material = blueMaterials[0];
        blueLight.enabled = false;
        yellowRenderer.material = yellowMaterials[0];
        yellowLight.enabled = false;
    }

    private string GeneratePattern()
    {
        string pattern = "";
        int numColors = 1;
        switch (level)
        {
            case 0:
                numColors = 3;
                break;
            case 1:
                numColors = 5;
                break;
            case 2:
                numColors = 7;
                break;
            case 3:
                numColors = 8;
                break;
            default:
                Debug.LogWarning("Invalid level number!");
                break;
        }

        for (int i = 0; i < numColors; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    pattern += "r";
                    break;
                case 1:
                    pattern += "g";
                    break;
                case 2:
                    pattern += "b";
                    break;
                case 3:
                    pattern += "y";
                    break;
                default:
                    Debug.LogWarning("Random number in pattern generation out of bounds!");
                    break;
            }
        }
        return pattern;
    }

    IEnumerator ShowPattern(string pattern)
    {
        while (true)
        {
            Debug.Log("Showing Pattern!");
            for (int i = 0; i < pattern.Length; i++)
            {
                switch (pattern[i])
                {
                    case 'r':
                        redRenderer.material = redMaterials[1];
                        redLight.enabled = true;
                        audioManager.Play("toy00");
                        yield return new WaitForSeconds(1f);
                        redRenderer.material = redMaterials[0];
                        redLight.enabled = false;
                        break;
                    case 'g':
                        greenRenderer.material = greenMaterials[1];
                        greenLight.enabled = true;
                        audioManager.Play("toy01");
                        yield return new WaitForSeconds(1f);
                        greenRenderer.material = greenMaterials[0];
                        greenLight.enabled = false;
                        break;
                    case 'b':
                        blueRenderer.material = blueMaterials[1];
                        blueLight.enabled = true;
                        audioManager.Play("toy02");
                        yield return new WaitForSeconds(1f);
                        blueRenderer.material = blueMaterials[0];
                        blueLight.enabled = false;
                        break;
                    case 'y':
                        yellowRenderer.material = yellowMaterials[1];
                        yellowLight.enabled = true;
                        audioManager.Play("toy03");
                        yield return new WaitForSeconds(1f);
                        yellowRenderer.material = yellowMaterials[0];
                        yellowLight.enabled = false;
                        break;
                    default:
                        Debug.LogWarning("Pattern contains unknown character!");
                        break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator LevelCooldown()
    {
        yield return new WaitForSeconds(3f);
        StartLevel();
    }

    public char InputPress(char c)
    {
        if (patternString[inputIndex] == c)
        {
            inputIndex++;
            if (inputIndex == patternString.Length)
            {
                FinishLevel();
                if (levelsFinished != 4)
                    StartCoroutine(LevelCooldown());
                return 'f';
            }
            return 'c';
        }
        else
        {
            ResetGame();
            return 'w';
        }
    }
}
