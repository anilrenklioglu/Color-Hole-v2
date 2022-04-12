using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace Hole
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton class: UIManager

        public static UIManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(this.gameObject);
            }
        }

        #endregion

        [Header("Level Progress")] 
        
        [SerializeField]private int offset;
        [SerializeField] private TMP_Text nextLevel;
        [SerializeField] private TMP_Text currentLevel;
        [SerializeField] private TMP_Text levelWinText;
        [SerializeField] private Image progressBar;

        [Space] 
        [SerializeField] private Image fade;
        
        public Score scoreController;
        public CubeCollector cubeCollector;
        private void Start()
        {
            Fade();
            progressBar.fillAmount = 0f;
            LevelProgressText();
        }

        private void LevelProgressText()
        {
            int level = SceneManager.GetActiveScene().buildIndex + offset;
            currentLevel.text = level.ToString();
            nextLevel.text = (level + 1).ToString();
        }

        public void LevelProgress()
        {
            float value = 1f - ((float) LevelManager.Instance.objectsInScene / LevelManager.Instance.totalObjectsInScene);

            //progressBar.fillAmount = value;

            progressBar.DOFillAmount(value, .5f);
        }

        public void ShowLevelWinText()
        {
            levelWinText.DOFade(1, .8f).From(0f);
        }

        public void Fade()
        {
            fade.DOFade(0f, 1f).From(1f);
        }
    }
  
}
