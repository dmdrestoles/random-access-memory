using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private XRRayInteractor grabRayInteractor;
    [SerializeField] private TeleportationProvider teleporationProvider;
    private InputAction cancel;
    private bool _isActive;
    private bool _isTeleporting;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        InputAction activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        InputAction teleport = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport");
        teleport.Enable();
        teleport.performed += OnTeleport;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
        {
            Debug.Log(grabRayInteractor);
            grabRayInteractor.enabled = true;
            return;
        }
        grabRayInteractor.enabled = false;

        if (cancel.ReadValue<Vector2>().x <= 0.1 && cancel.ReadValue<Vector2>().y <= 0.1)
        {
            Debug.Log(rayInteractor);
            rayInteractor.enabled = false;
            _isActive = false;
            _isTeleporting = false;
            return;
        }

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            _isTeleporting = false;
            return;
        }

        if (!_isTeleporting)
            return;

        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = hit.point
        };
        teleporationProvider.QueueTeleportRequest(teleportRequest);

        rayInteractor.enabled = false;
        _isActive = false;
        _isTeleporting = false;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
        _isTeleporting = false;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;
        _isTeleporting = false;
    }

    private void OnTeleport(InputAction.CallbackContext context)
    {
        _isTeleporting = true;
    }
}
