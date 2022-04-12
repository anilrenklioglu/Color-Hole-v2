using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    public class ExplosionFXController : MonoBehaviour
    {
        [SerializeField] private Material[] materials;
        
        private void Start()
        {
            GetComponent<ParticleSystemRenderer>().material.color = LevelManager.Instance.objectColor;
        }
    }

}
