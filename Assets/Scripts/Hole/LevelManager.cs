using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hole
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton class: LevelManager

        public static LevelManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

        }

        #endregion

        [SerializeField] private Transform parent;

        [SerializeField] private ParticleSystem winFX;
        
        [Space]
        [HideInInspector] public int objectsInScene;
        [HideInInspector] public int totalObjectsInScene;
        
        [Space] 
        [Header("Level Obstacles & Objects")] 
    
        [SerializeField] Material groundMaterial;
        [SerializeField] Material objectMaterial;
        [SerializeField] Material obstacleMaterial;

        [SerializeField] SpriteRenderer groundBorderSprite;
        [SerializeField] SpriteRenderer fadeSprite;

        [SerializeField] Image progressFillImage;

        [Space] 
        [Header("Level Colors")] 
        [Header("Ground Colors")]
    
        [SerializeField] Color groundColor;
        [SerializeField] Color bordersColor;
        
    
        [Header("Cube Colors")]
    
        public Color objectColor;
        [SerializeField] Color obstacleColor;
    
        [Header("UI (Progress)")]
    
        [SerializeField] Color progressFillColor;
    
        [Header("Background")]
    
        [SerializeField] Color cameraColor;
        [SerializeField] Color fadeColor;

        public int levelID;
        
        
        private void Start()
        {
            Count();
            UpdateColors();
        }
        private void Count()
        {
            totalObjectsInScene = parent.childCount;
            objectsInScene = totalObjectsInScene;
        }

        public void WinFX()
        {
            winFX.Play();
        }
        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
        
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void UpdateColors()
        { groundMaterial.color = groundColor;
            groundBorderSprite.color = bordersColor;

            obstacleMaterial.color = obstacleColor;
            objectMaterial.color = objectColor;

            progressFillImage.color = progressFillColor;

            Camera.main.backgroundColor = cameraColor;
            fadeSprite.color = fadeColor;
        }

        private void OnValidate()
        {
            UpdateColors();
        }
    }

}
