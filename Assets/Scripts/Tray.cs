using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    private int numDough = 0;

    [SerializeField] private Animator animator;
    [SerializeField] private Animator spoonAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (numDough == 12)
            return;

        if (other.CompareTag("Spoon") && other.gameObject.GetComponent<Spoon>().isHeldByPlayer && other.gameObject.GetComponent<Spoon>().isFull)
        {
            numDough++;
            animator.SetInteger("cookies", numDough);
            other.gameObject.GetComponent<Spoon>().isFull = false;
            spoonAnimator.SetBool("isFilled", false);

            if (numDough == 12)
            {
                gameObject.GetComponent<MeshCollider>().isTrigger = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                Debug.Log("Tray full!");
            }
        }
    }

    public void CookedAnimation()
    {
        animator.SetTrigger("cooked");
    }
}
