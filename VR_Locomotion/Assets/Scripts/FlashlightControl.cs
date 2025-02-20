using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FlashlightControl : MonoBehaviour
{
    public InputAction FlashlightButton;
    public InputActionMap inputActions;
    // Start is called before the first frame update

    void Awake()
    {
        // FlashlightButton.performed += TurnOnFlashlight;
        inputActions["fire"].performed += TurnOnFlashlight;   
    }

    private void TurnOnFlashlight(InputAction.CallbackContext context)
    {
        Debug.Log("Flashllight On");
    }

    void OnEnable()
    {
        FlashlightButton.Enable();
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
}
