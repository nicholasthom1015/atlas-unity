using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //public void LevelSelect(int level);
    //public void ButtonClicked(int buttonNo);

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
    }
   // SceneManager.LoadScene(SceneManager.LevelSelect(1));
}
