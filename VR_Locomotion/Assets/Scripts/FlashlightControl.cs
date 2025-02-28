using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class FlashlightControl : MonoBehaviour
{
    public InputAction FlashlightButton;
    public InputActionMap inputActions;
    public GameObject Spotlight;
    // Start is called before the first frame update

    void Awake()
    {
        // FlashlightButton.performed += TurnOnFlashlight;
        inputActions["fire"].performed += TurnOnFlashlight;   
    }

    [ContextMenu("ActivateFlashlight")]

    void TurnOnFlashlight()
    {
        Spotlight.SetActive(!Spotlight.activeSelf);

    }
    private void TurnOnFlashlight(InputAction.CallbackContext context)
    {
        TurnOnFlashlight();
        //Debug.Log("Flashllight On");
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
