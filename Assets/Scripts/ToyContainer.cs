using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToyContainer : MonoBehaviour
{
    public string containerType;
    public bool isHeldByPlayer { get; private set; }

    private void Start()
    {
        ToyCounter.toyCounter.UpdateNumToyContainers();
    }

    public void HoldObject(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            isHeldByPlayer = true;
        }
    }

    public void LetGoOfObject(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            isHeldByPlayer = false;
        }
    }
}
