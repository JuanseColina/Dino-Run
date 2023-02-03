using System;
using TMPro;
using UnityEngine;

public class IndividualMultiplier : MonoBehaviour
{
    private TextMeshProUGUI txt;
    [SerializeField] private float multiplier;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<CashCtrl>()) return;
        GameManager.Instance.Multiplier = multiplier;
        Debug.Log(multiplier);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // throw new NotImplementedException();
        }
    }
}
