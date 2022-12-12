using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spoon : MonoBehaviour
{
    [SerializeField] private Animator spoonAnimator;
    [SerializeField] private Animator bowlAnimator;
    public bool isFull { get; set; }
    public bool isHeldByPlayer { get; private set; }

    private void Start()
    {
        isFull = false;
        isHeldByPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHeldByPlayer || isFull)
            return;

        if (other.CompareTag("Bowl") && other.gameObject.GetComponent<Bowl>().isEmpty)
            return;

        if (other.CompareTag("Bowl") && other.gameObject.GetComponent<Bowl>().isHeldByPlayer && other.gameObject.GetComponent<Bowl>().isMixed)
        {
            isFull = true;
            spoonAnimator.SetBool("isFilled", true);
            other.gameObject.GetComponent<Bowl>().spoonTouches++;

            if (other.gameObject.GetComponent<Bowl>().spoonTouches == 12)
            {
                other.gameObject.GetComponent<Bowl>().isEmpty = true;
                bowlAnimator.SetBool("Empty", true);
                Debug.Log("Bowl empty!");
            }
        }
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
