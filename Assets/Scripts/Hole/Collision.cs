using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Hole
{
    public class Collision : MonoBehaviour
    {
        public int cubeCollectorScore;
        public int score;
        private int obstaclesInGround;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Object") || other.tag.Equals("Obstacle"))
            {
                Magnet.Instance.RemoveMagnet(other.attachedRigidbody);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!GameController.IsGameOver)
            {

                if (other.CompareTag("Object"))
                { 
                    UIManager.Instance.scoreController.AddScore(score);
                    UIManager.Instance.cubeCollector.IncreaseCubeCount(cubeCollectorScore);
                    
                    LevelManager.Instance.objectsInScene--;

                    UIManager.Instance.LevelProgress();

                    if (LevelManager.Instance.objectsInScene == 0)
                    {
                        UIManager.Instance.ShowLevelWinText();
                        
                        LevelManager.Instance.WinFX();
                        
                        Invoke("Next", 1f);
                    }
                }

                if (other.CompareTag("Obstacle"))
                {
                    UIManager.Instance.scoreController.ReduceScore(score);
                    
                    obstaclesInGround++;

                    if (obstaclesInGround == 2)
                    {
                        GameController.IsGameOver = true;
                        
                        Camera.main.transform.DOShakePosition(.6f, .7f, 20, 90f).OnComplete(() =>
                        {
                            LevelManager.Instance.ReloadLevel();
                        });

                    }

                }
            }
            
        }

        private void Next()
        {
            LevelManager.Instance.NextLevel();
        }
    }
 
}
