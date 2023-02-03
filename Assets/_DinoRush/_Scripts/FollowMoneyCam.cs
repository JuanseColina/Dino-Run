using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowMoneyCam : MonoBehaviour
{
    [SerializeField] private EndingStand _endingStand;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

   

    private void Update()
    {
        if (_endingStand.LastMoney())
        {
            //_cinemachineVirtualCamera.LookAt = _endingStand.LastMoney();
            _cinemachineVirtualCamera.Follow = _endingStand.LastMoney();
        }
    }
}
