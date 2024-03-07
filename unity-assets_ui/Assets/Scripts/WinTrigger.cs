using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public TMP_Text TimerText;
    [SerializeField]
    private Timer time;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            time.gameObject.SetActive(false);
            TimerText.color = Color.green;
            TimerText.fontSize = 60;
        }
    }
}
