using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCam : MonoBehaviour {
    private static Camera _mainCam;

    void Awake(){
        if(_mainCam == null){
            _mainCam = Camera.main;
        }
    }
    // Use this for initialization
    void Start ()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.5f, .4f).setEasePunch();
    }
	
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward,
            _mainCam.transform.rotation * Vector3.up);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward,
            _mainCam.transform.rotation * Vector3.up);
    }
}