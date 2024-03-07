using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinReveal : MonoBehaviour
{
     [SerializeField]
     private Win win;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           win.gameobject.SetActive(true);
        }
    }
}
