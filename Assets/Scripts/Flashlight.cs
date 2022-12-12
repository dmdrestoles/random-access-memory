using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public void ToggleFlashlight()
    {
        gameObject.transform.GetComponentInChildren<Light>().enabled = !gameObject.transform.GetComponentInChildren<Light>().enabled;
    }
}
