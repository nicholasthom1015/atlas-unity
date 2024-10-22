using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongMenu : MonoBehaviour
{
    [SerializeField] private bool isOp1;
    [SerializeField] private bool isOp2;
    [SerializeField] private bool isOp3;
    [SerializeField] private bool isOp4;

    public void LoadScene(int sceneIndex)
    {
        if(isOp1)
            {
                SceneManager.LoadScene(1);
            }
        
        if(isOp2)
            {
                SceneManager.LoadScene(2);
            }

        if(isOp3)
            {
                SceneManager.LoadScene(3);
            }

        if(isOp4)
            {
                SceneManager.LoadScene(0);
            }  
    }
}
