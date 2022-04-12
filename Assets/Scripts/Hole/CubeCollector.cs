using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Hole
{
    public class CubeCollector : MonoBehaviour
    {
        [Header("Cube Collector")] 
        
        [SerializeField] private TextMeshPro cubeCountText;
        [SerializeField] private int cubeCount;

        private void Start()
        {
            cubeCount = 0;
        }

        public void IncreaseCubeCount(int c)
        {
            cubeCount += c;
            SetCubeCountTextText();
        }
        public void ReduceCubeCount(int c)
        {
            cubeCount -= c;
            SetCubeCountTextText();
        }
        
        private void SetCubeCountTextText()
        {
            cubeCountText.text = cubeCount.ToString();
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}

