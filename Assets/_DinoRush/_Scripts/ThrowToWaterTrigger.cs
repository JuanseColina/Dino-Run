using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowToWaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NpcController>())
        {
            StartCoroutine(other.GetComponent<NpcController>().JumpInToThePool());
        }
    }
}
