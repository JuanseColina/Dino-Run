using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSeller : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int Mode = Animator.StringToHash("Mode");
    private static readonly int Dance = Animator.StringToHash("dance");

    private void Awake()
    {
        GetComponent<Animator>();
    }

    public void Celebrate()
    {
        _animator.SetBool(Dance,true);
        _animator.SetInteger(Mode,99);
    }
}
