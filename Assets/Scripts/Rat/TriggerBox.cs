using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public Action OnTriggered;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Vegi"))
        {
            Debug.Log("vegi");
            OnTriggered?.Invoke();
        }
    }
}
