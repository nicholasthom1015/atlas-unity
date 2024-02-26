using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    [SerializeField]
    private Timer time;
    // Start is called before the first frame update

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            time.gameObject.SetActive(true);
        }
    }
}
