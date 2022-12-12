using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestContainerTracker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Toy Container"))
        {
            ToyCounter.toyCounter.MarkContainerInChest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Toy Container"))
        {
            ToyCounter.toyCounter.UnmarkContainerInChest();
        }
    }
}
