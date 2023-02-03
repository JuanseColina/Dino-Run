using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class EndingStand : MonoBehaviour
{
    [SerializeField] private MoneyHolder moneyHolder;
    [SerializeField] private NpcManager npcManager;

    [SerializeField] private GameObject npcSeller, moneyFbx;
    [SerializeField] private float moneySize = .22f;
    
    [SerializeField] private Transform point;
    public float animationTime; // tiempo de la animación

    [SerializeField] private List<Transform> moneyStack;

    public List<Transform> moneyStack1 => moneyStack;
    // lista de objetos apilados
    private Vector3 nextPosition; // posición del proximo objeto en el stack

    
    private float previousPos;
    private float previousScaleToAdd;
    private float previousItem;


    private Transform lastMoney;
        // función para crear un stack
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator CreateStack()
        {
            for (var i = 0; i < moneyHolder.itemInBag1.Count; i++)
            {
                //Objects
                var newObject = moneyHolder.itemInBag1[i];
                moneyHolder.itemInBag1[i] = null;
                newObject.localScale = Vector3.one;
                newObject.SetParent(npcSeller.transform, true);
                previousPos = nextPosition.y;
                if(moneyStack.Count > 1)previousItem = moneySize + previousPos;
                nextPosition = Vector3.up * previousItem;
                LeanTween.move(newObject.gameObject, npcSeller.transform.position, animationTime / 3)
                    .setOnComplete(() => newObject.gameObject.SetActive(false));
                
                    
                //Money
                var newMoney = Instantiate(moneyFbx, point.position, Quaternion.identity);
                newMoney.transform.SetParent(point,true);
                newMoney.transform.rotation = Quaternion.identity;
                moneyStack.Add(newMoney.transform);
                newMoney.transform.localPosition = nextPosition;
                lastMoney = newMoney.transform;
                LeanTween.rotateY(newMoney.gameObject, 360f, animationTime + .1f);
                //LeanTween.moveLocal(newMoney.gameObject, nextPosition, animationTime).setEaseOutBack()
                //.setOnComplete((() => lastMoney = newMoney.transform));


                
                npcManager.LessToBagValue(); //bag size less
                
                yield return new WaitForSeconds(.1f);
            }
            StartCoroutine(GameManager.Instance.WinAction());
        }

        public Transform LastMoney()
        {
            return lastMoney;
        }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CamController.instance.ChangeCam(1);
            StartCoroutine(CreateStack());
            GameManager.Instance.StopPlayer();
            npcManager.npcSeller.Celebrate();
        }
    }
}
