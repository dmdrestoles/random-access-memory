using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private string thisKey;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private GameObject handle1;
    [SerializeField] private GameObject handle2;
    [SerializeField] private GameObject chestCover;
    [SerializeField] private GameObject cabinetDoor;
    [SerializeField] private GameObject[] containers;
    [SerializeField] private GameObject simonSaysObject;
    [SerializeField] private GameObject spawnPoints;
    private static string correctCode;
    private static string currentCode = "";
    private static Coroutine coroutineKeyPressed = null;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            correctCode = "2035";
        }
        else
        {
            correctCode = "0725";
        }
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
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            handle1.layer = 6;
            handle2.layer = 6;
            chestCover.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else
        {
            GameObject go = Instantiate(simonSaysObject, spawnPoints.transform.position, spawnPoints.transform.rotation);
            SimonSays.simonSays.simonSaysObject = go;
            Destroy(cabinetDoor);
            Destroy(gameObject.transform.parent.gameObject);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
            return;

        int i = 0;
        foreach(GameObject obj in containers)
        {
            Instantiate(obj, spawnPoints.transform.GetChild(i).position, transform.rotation);
            i++;
        }

        FindObjectOfType<GrandmaAudioManager>().Play("grandma00");
        Destroy(gameObject.transform.parent.gameObject);
    }

   
}
