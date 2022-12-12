using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mixer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (SimonSays.simonSays.isStarted == true)
                return;
        }
        catch
        {
            if (SimonSaysRoom3.simonSays.isStarted == true)
                return;
        }
        try
        {
            if (SimonSaysRoom3.simonSays.isStarted == true)
                return;
        }
        catch
        {
            if (SimonSays.simonSays.isStarted == true)
                return;
        }

        if (other.CompareTag("Bowl") && other.gameObject.GetComponent<Bowl>().currentIngredientOrder == 4)
        {
            other.gameObject.GetComponent<Bowl>().isMixed = true;
            other.gameObject.GetComponent<Bowl>().isEmpty = false;
            other.GetComponent<Bowl>().BowlUpdate();
            Debug.Log("Bowl mixed!");
        }
        else if ((other.CompareTag("Bowl") && SceneManager.GetActiveScene().buildIndex == 3 && other.gameObject.GetComponent<Bowl>().currentIngredientOrder == 3))
        {
            other.gameObject.GetComponent<Bowl>().isMixed = true;
            other.gameObject.GetComponent<Bowl>().isEmpty = false;
            other.GetComponent<Bowl>().BowlUpdate();
            Debug.Log("Bowl mixed!");
        }
    }
}
