using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarCtrl : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private BoxCollider myBox;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        myBox = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private bool touchIt;
    private float explosionForce = 15f;
    private float explosionRadius = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !touchIt)
        {
            touchIt = true;
            _particleSystem.Play();
            other.transform.GetComponent<PlayerController>().Attack();
            ExplosionSimulate(other);
        }
    }
    
    
    void ExplosionSimulate(Collider other)
    {
        // Convertir el resultado de OverlapSphere a Rigidbody[]
        Collider[] colliders = Physics.OverlapSphere(other.transform.position, explosionRadius);
        Rigidbody[] rigidbodies = new Rigidbody[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            rigidbodies[i] = colliders[i].GetComponentInParent<Rigidbody>();
        }

        // Aplicar la fuerza de explosión en la dirección opuesta
        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, other.transform.position, explosionRadius, .7f,
                    ForceMode.Impulse);
            }
        }

        _rigidbody.useGravity = true;
        myBox.isTrigger = false;
    }
    
}
