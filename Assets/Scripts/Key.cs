using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Key : MonoBehaviour
{
    public bool isHeldByPlayer { get; private set; }

    private void Start()
    {
        isHeldByPlayer = false;
    }

    public void HoldObject(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.CompareTag("Player"))
        {
            try
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
            catch
            {
                gameObject.GetComponent<MeshCollider>().isTrigger = true;
            }
            isHeldByPlayer = true;
        }
    }

    public void LetGoOfObject(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.CompareTag("Player"))
        {
            try
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
            catch
            {
                gameObject.GetComponent<MeshCollider>().isTrigger = false;
            }
            isHeldByPlayer = false;
        }
    }
}
