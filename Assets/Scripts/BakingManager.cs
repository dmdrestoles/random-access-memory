using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingManager : MonoBehaviour
{
    public static BakingManager bakingManager { get; private set; }

    private void Awake()
    {
        if (bakingManager != null)
            Debug.LogWarning("More than one instance of BakingManager!");
        bakingManager = this;
    }


}
