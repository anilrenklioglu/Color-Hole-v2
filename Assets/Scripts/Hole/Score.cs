using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Hole
{
    public class Score : MonoBehaviour
    {
        [Header("Score")] 
        
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private int score;

        private void Start()
        {
            score = 0;
        }

        public void AddScore(int s)
        {
            score += s;
            SetScoreText();
        }

        public void ReduceScore(int s)
        {
            if (score == 0) return;
            score -= s;
            SetScoreText();
        }

        private void SetScoreText()
        {
            scoreText.text = score.ToString();
        }
    } 
}

