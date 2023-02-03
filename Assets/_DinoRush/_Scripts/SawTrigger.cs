using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PlayerDeadMode();
        }

        if (other.GetComponent<NpcController>())
        {
            other.GetComponent<NpcController>().NpcDeadMode(false);
        }
    }
}
