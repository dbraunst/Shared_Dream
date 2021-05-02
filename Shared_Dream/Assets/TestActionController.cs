using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestActionController : MonoBehaviour
{

    private ActionBasedController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        // Read whether of not the inputAction attached to select is true. 
        // Can also use float, for ex, to read press amount
        bool isPressed = controller.selectAction.action.ReadValue<bool>();

        // Adds "Action_performed" callback function to list of funcs to call
        // when this action is performed
        controller.selectAction.action.performed += Action_performed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log("You pressed Select!");
    }
}
