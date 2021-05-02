using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisplayTeleportOnSelect : MonoBehaviour
{
    private XRInteractorLineVisual lineVisual;
    private ActionBasedController controller;

    private GameObject recticleObj;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        // Adds "Action_performed" callback function to list of funcs to call
        // when this action is performed
        controller.selectAction.action.started += Select_Started;
        controller.selectAction.action.canceled += Select_Canceled;

        lineVisual = this.GetComponent<XRInteractorLineVisual>();
        recticleObj = lineVisual.reticle;

        lineVisual.enabled = false;
        recticleObj.SetActive(false);
    }

    private void Select_Started(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log("You started Select!");
        // rayInteractor.enabled = true;
        lineVisual.enabled = true;
        recticleObj.SetActive(true);
    }

    private void Select_Canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log("You pressed Select!");
        // rayInteractor.enabled = false;
        lineVisual.enabled = false;
        recticleObj.SetActive(false);
    }
}
