using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    [RequireComponent(typeof(SphereCollider))]
    public class Magnet : MonoBehaviour
    {
        #region Singleton class: Magnet

        public static Magnet Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion
        [SerializeField] private ParticleSystem magnetFX;
        [SerializeField] private float magnetForce;

        private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();

        private Transform magnet;
        private AudioSource audioSource;

        private void Start()
        {
            magnet = transform;
            audioSource = GetComponent<AudioSource>();
            affectedRigidbodies.Clear();
        }

        private void FixedUpdate()
        {
            foreach (Rigidbody rb in affectedRigidbodies)
            {
                rb.AddForce((magnet.position - rb.position)* magnetForce * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!GameController.IsGameOver && other.CompareTag("Object") || other.CompareTag("Obstacle"))
            {
                AddMagnet(other.attachedRigidbody);
                magnetFX.Play();
                audioSource.Play();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!GameController.IsGameOver && other.CompareTag("Object") || other.CompareTag("Obstacle"))
            {
               RemoveMagnet(other.attachedRigidbody);
            }
        }

        public void AddMagnet(Rigidbody rb)
        {
            affectedRigidbodies.Add(rb);
        }

        public void RemoveMagnet(Rigidbody rb)
        {
            affectedRigidbodies.Remove(rb);
        }
    } 
}

