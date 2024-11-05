using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenScene : MonoBehaviour
{
    public Animator Opening;
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public GameObject Ball; 

    private bool openingFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerOne.SetActive(false);
        PlayerTwo.SetActive(false);
        Ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Opening.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !Opening.IsInTransition(0))
        {
            if (!openingFinished)
            {
                EnableGame();
                DisableOpening();
                openingFinished = true;
            }
        }
    }

    void EnableGame()
    {
        PlayerOne.SetActive(true);
        PlayerTwo.SetActive(true);
        Ball.SetActive(true);
    }

    void DisableOpening()
    {
        gameObject.SetActive(false);
    }
}
