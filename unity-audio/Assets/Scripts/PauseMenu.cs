using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private bool pressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pressed)
        {
            Pause();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pressed)
        {
            Resume();
        }
    }

    public void Pause()
    {
        pressed = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pressed = false;
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }
}
