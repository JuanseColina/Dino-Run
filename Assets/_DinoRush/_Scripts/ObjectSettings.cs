using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ObjectSettings : MonoBehaviour
{
    [FormerlySerializedAs("valueToAdd")] [SerializeField] private float lastValueAdd;
    public float ValueToAdd => lastValueAdd;

    private bool follow;
    

    public bool follow1
    {
        set => follow = value;
    }

    private NpcManager npcManager;
    private GameObject robberyBag;

    private NavMeshAgent navMeshAgent;
    private MoneyHolder moneyHolder;

    private float speed;
    private void Start()
    {
        moneyHolder = FindObjectOfType<MoneyHolder>();
        robberyBag = GameObject.FindWithTag("Player").GetComponent<PlayerController>().RobberyBag;
        npcManager = FindObjectOfType<NpcManager>();
        speed = npcManager.PlayerController.MSpeed * 4 ;
    }

    private void Update()
    {
        if (follow)
        {
            var position = robberyBag.transform.position;
            var time = Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, position, speed * time);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag") && follow)
        {
            npcManager.AddItemToBagValue();
            moneyHolder.AddItemToBag(transform); 
        }
    }
}
