using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class LocomotionController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;

    public float teleportActivationThresh = 0.1f;

    private InputAction _thumbstick;

    private bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {

        var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Activate");
        activate.Enable(); 
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        activate.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        _thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;
        
        if (_thumbstick.triggered)
            return;

        // if raycast does not return valid
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        provider.QueueTeleportRequest(request);
        rayInteractor.enabled = false;
        _isActive = false;

    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        Debug.Log("Teleport Activated!");
        rayInteractor.enabled = true;
        _isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;
    }
}
