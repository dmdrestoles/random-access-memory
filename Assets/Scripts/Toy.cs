using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Toy : MonoBehaviour
{
    [SerializeField] private string toyType;
    private bool isHeldByPlayer = false;
    private Coroutine coroutineCombine = null;

    private void Start()
    {
        ToyCounter.toyCounter.AddToyNums();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Toy Container"))
        {
            if (!isHeldByPlayer)
                return;

            ToyContainer toyContainer = other.gameObject.GetComponent<ToyContainer>();
            if (!toyContainer.isHeldByPlayer)
                return;

            if (toyType == toyContainer.containerType)
            {
                Destroy(gameObject);
                ToyCounter.toyCounter.DeductNumToys();

                // To be used once we have some sort of player feedback/visual indicator
                /*
                if (coroutineCombine == null)
                    coroutineCombine = StartCoroutine(Combine());
                */
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Add a visual indicator
        if (coroutineCombine != null)
        {
            StopCoroutine(coroutineCombine);
            coroutineCombine = null;
        }
    }

    IEnumerator Combine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        ToyCounter.toyCounter.DeductNumToys();
        coroutineCombine = null;
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
                gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
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
                gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            }
            isHeldByPlayer = false;
        }
    }
}
