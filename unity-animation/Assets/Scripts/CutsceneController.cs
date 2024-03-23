using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject mainCamera;
    public GameObject playerController;
    public GameObject timerCanvas;

    private bool cutsceneFinished = false;

    void Start()
    {
        // Disable Main Camera, PlayerController, and TimerCanvas at the beginning
        mainCamera.SetActive(false);
        playerController.SetActive(false);
        timerCanvas.SetActive(false);
    }

    void Update()
    {
        // Check if the cutscene has finished playing
        if (cutsceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !cutsceneAnimator.IsInTransition(0))
        {
            if (!cutsceneFinished)
            {
                // Cutscene finished, perform necessary actions
                EnableMainCameraPlayerControllerAndTimerCanvas();
                DisableCutsceneController();
                cutsceneFinished = true;
            }
        }
    }

    void EnableMainCameraPlayerControllerAndTimerCanvas()
    {
        // Enable Main Camera, PlayerController, and TimerCanvas
        mainCamera.SetActive(true);
        playerController.SetActive(true);
        timerCanvas.SetActive(true);
    }

    void DisableCutsceneController()
    {
        // Disable this script
        gameObject.SetActive(false);
    }
}