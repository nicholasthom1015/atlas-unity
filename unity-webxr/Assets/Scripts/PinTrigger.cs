using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PinTrigger : MonoBehaviour
{

    [SerializeField] private TMP_Text PinScore;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            PinScore.text = (int.Parse(PinScore.text) + 1).ToString();
            //Debug.Log("Pins");
        }
    }
}
