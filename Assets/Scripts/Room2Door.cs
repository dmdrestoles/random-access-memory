using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room2Door : MonoBehaviour
{
    [SerializeField] private GameObject exitSpawn;
    [SerializeField] private GameObject exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && other.gameObject.GetComponent<Key>().isHeldByPlayer)
        {
            Instantiate(exit, exitSpawn.transform.position, exitSpawn.transform.rotation);
            Destroy(gameObject);
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                FindObjectOfType<CoworkerAudioManager>().Play("coworker07");
            }
        }
    }
}
