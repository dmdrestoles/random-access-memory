using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPadRoom3 : MonoBehaviour
{
    [SerializeField] private string thisKey;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private GameObject cabinetDoor;
    [SerializeField] private GameObject simonSaysObject;
    [SerializeField] private GameObject spawnPoints;
    private static string correctCode;
    private static string currentCode = "";
    private static Coroutine coroutineKeyPressed = null;

    private void Start()
    {
        correctCode = "0725";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coroutineKeyPressed != null)
                return;

            currentCode += thisKey;
            codeText.text = currentCode;
            coroutineKeyPressed = StartCoroutine(KeyPressed());

            if (currentCode.Length == 4)
            {
                if (currentCode == correctCode)
                {
                    UnlockChest();
                    codeText.text = "Unlocked";
                }
                else
                {
                    codeText.text = "Wrong";
                }
                currentCode = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentCode == "")
        {
            codeText.text = "Locked";
        }
    }

    private IEnumerator KeyPressed()
    {
        yield return new WaitForSeconds(1f);
        coroutineKeyPressed = null;
    }

    private void UnlockChest()
    {
        GameObject go = Instantiate(simonSaysObject, spawnPoints.transform.position, spawnPoints.transform.rotation);
        SimonSaysRoom3.simonSays.simonSaysObject = go;
        Destroy(cabinetDoor);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
