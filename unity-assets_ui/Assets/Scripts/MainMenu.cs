using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private bool isL1;
    [SerializeField] private bool isL2;
    [SerializeField] private bool isL3;
    [SerializeField] private bool isOp;
   
   public void LoadScene(int sceneIndex)
   {
        if(isOp)
            {
                SceneManager.LoadScene(1);
                OptionsMenu.Instance.previousScene = SceneManager.GetActiveScene().buildIndex;
            }

        if(isL1)
            {
                SceneManager.LoadScene(2);
            }

        if(isL2)
            {
                SceneManager.LoadScene(3);
            }

        if(isL3)
            {
                SceneManager.LoadScene(4);
            }

        
   }
}
