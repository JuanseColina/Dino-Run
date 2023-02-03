using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyImageBehavoiur : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    private readonly float speed = 6;
    private bool flag;
    
    
    private void Start()
    {
        LeanTween.move(gameObject,  transform.position + Vector3.up * 3 + Vector3.forward * 3, 
            .4f).setEaseOutCubic().setOnComplete(() =>
        {
            flag = true;
        });
        Destroy(this, 2f);
    }

    private void Update()
    {
        float timeDelta = Time.deltaTime;
        if (flag)
        {
            var transparency = image.color;
            var transparency2 = text.color;
            transparency2.a -= timeDelta * speed;
            transparency.a -= timeDelta * speed;
            image.color = transparency;
            text.color = transparency2;
        }
        
    }
}
