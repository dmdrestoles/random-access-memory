using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredients : MonoBehaviour
{
    public bool isHeldByPlayer { get; private set; }
    [SerializeField] private int ingredientOrder;

    private void Start()
    {
        isHeldByPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bowl"))
            return;

        if (!isHeldByPlayer)
            return;

        if (!other.gameObject.GetComponent<Bowl>().isHeldByPlayer)
            return;

        if (other.gameObject.GetComponent<Bowl>().currentIngredientOrder == ingredientOrder)
        {
            other.GetComponent<Bowl>().currentIngredientOrder++;
            other.GetComponent<Bowl>().BowlUpdate();
            Destroy(gameObject);
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
