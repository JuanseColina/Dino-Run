using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.MultiplierMode();
            other.GetComponent<PlayerController>().winMode1 = true;
        }
    }
}
