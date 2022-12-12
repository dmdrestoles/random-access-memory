using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private GameObject flashlightPrefab;
    [SerializeField] private GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cabinet Key") && other.GetComponent<Key>().isHeldByPlayer)
        {
            Instantiate(flashlightPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Destroy(gameObject);
        }
    }
}
