using System;
using Dreamteck.Splines;
using UnityEngine;

public class RoadSelector : MonoBehaviour
    {
        [SerializeField] private SplineComputer _splineComputer;

        [SerializeField] private bool entry;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                if (entry) player.SelectSplineComputer(_splineComputer, true, 0);
                else player.SelectSplineComputer(null, false, transform.position.x);
            }
        }
    }