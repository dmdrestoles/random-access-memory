using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject slot0;

    private bool isEnabled;
    private XRSocketInteractor socket0;
    private IXRSelectInteractable object0;
    private Vector3 object0Transform;

    // Start is called before the first frame update
    void Start()
    {
        tablet.GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in tablet.transform)
        {
            child.GetComponent<MeshRenderer>().enabled = true;
        }
        InputAction toggle = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Inventory Toggle");
        toggle.Enable();
        toggle.performed += OnInventoryToggle;
        isEnabled = false;

        socket0 = slot0.GetComponent<XRSocketInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        tablet.GetComponent<MeshRenderer>().enabled = isEnabled;
        if (isEnabled == false)
        {
            tablet.transform.position = new Vector3(0, 100, 0);
        }
    }

    private void OnInventoryToggle(InputAction.CallbackContext context)
    {
        isEnabled = !isEnabled;
        tablet.transform.position = rightHand.transform.position;
        tablet.transform.rotation = rightHand.transform.rotation;
    }
}
