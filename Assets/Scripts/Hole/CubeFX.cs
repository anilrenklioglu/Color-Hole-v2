using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Hole
{
    public class CubeFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem cubeFX; 
        private MeshRenderer meshRenderer;
        

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Explosion()
        {
            meshRenderer.enabled = false;
            cubeFX.Play();
            UIManager.Instance.cubeCollector.ReduceCubeCount(1);
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.transform.tag == "Obstacle")
            {
                Explosion();
            }
        }
    }  
}

