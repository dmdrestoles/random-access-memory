using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class Bowl : MonoBehaviour
{
    public bool isHeldByPlayer { get; private set; }
    public int currentIngredientOrder { get; set; }
    public bool isMixed { get; set; }
    public bool isEmpty { get; set; }
    public int spoonTouches { get; set; }

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject circuitBreaker;

    private void Start()
    {
        isHeldByPlayer = false;
        currentIngredientOrder = 0;
        isMixed = false;
        isEmpty = true;
        spoonTouches = 0;
    }

    public void HoldObject(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.CompareTag("Player"))
        {
            try
            {
                gameObject.GetComponent<MeshCollider>().isTrigger = true;
            }
            catch
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
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
                gameObject.GetComponent<MeshCollider>().isTrigger = false;
            }
            catch
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
            isHeldByPlayer = false;
        }
    }

    public void BowlUpdate()
    {
        if (currentIngredientOrder == 0)
        {
            return;
        }
        else if (currentIngredientOrder == 1)
        {
            animator.SetTrigger("State1");
        }
        else if (currentIngredientOrder == 2)
        {
            animator.SetTrigger("State2");
        }
        else if (SceneManager.GetActiveScene().buildIndex != 3 && currentIngredientOrder == 3)
        {
            animator.SetTrigger("State3");
            FindObjectOfType<CoworkerAudioManager>().Play("coworker03");
        }
        else if (SceneManager.GetActiveScene().buildIndex != 3 && currentIngredientOrder == 4)
        {
            if (isMixed == true)
            {
                animator.SetTrigger("tMixed");
            }
            else
            {
                animator.SetTrigger("Full");
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 && currentIngredientOrder == 3)
        {
            Debug.Log("Room 3");
            if (isMixed == true)
            {
                animator.SetTrigger("tMixed");
            }
            else
            {
                animator.SetTrigger("Full");
                circuitBreaker.GetComponentInChildren<CircuitBreakerButtonRoom3>().StartGame();
            }
        }
    }
}
