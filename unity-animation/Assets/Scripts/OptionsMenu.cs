using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance;
    public int previousScene = 0;

    void Start(){
        if(Instance != this || Instance == null){
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void Back()
    {
    SceneManager.LoadScene(previousScene);
    }
}
