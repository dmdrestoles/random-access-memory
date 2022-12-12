using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCounter : MonoBehaviour
{
    public static ToyCounter toyCounter { get; private set; }
    private int numToys = 0;
    private int numToyContainers = 0;
    private int numContainersInChest = 0;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject exitSpawn;
    [SerializeField] private GameObject exit;

    private void Awake()
    {
        if (toyCounter != null)
        {
            Debug.LogWarning("More than one instance of Toy Counter!");
        }
        toyCounter = this;
    }

    public void AddToyNums()
    {
        numToys += 1;
    }

    public void DeductNumToys()
    {
        numToys -= 1;
        CheckPuzzle();
    }

    public void MarkContainerInChest()
    {
        numContainersInChest += 1;
        CheckPuzzle();
    }

    public void UnmarkContainerInChest()
    {
        numContainersInChest -= 1;
    }

    public void UpdateNumToyContainers()
    {
        numToyContainers += 1;
    }

    private void CheckPuzzle()
    {
        if (numToys != 0 || numContainersInChest != numToyContainers)
            return;

        FindObjectOfType<GrandmaAudioManager>().Play("grandma01");
        Invoke("PlayCoworkerNextRoom", FindObjectOfType<GrandmaAudioManager>().ClipLength("grandma01"));
        Instantiate(exit, exitSpawn.transform.position, exitSpawn.transform.rotation);
        Destroy(door);
    }

    private void PlayCoworkerNextRoom()
    {
        FindObjectOfType<CoworkerAudioManager>().Play("coworker01");
    }
}
