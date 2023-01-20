using System.Collections.Generic;
using SpaceShooter.Core;
using TMPro;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private PlayerHealth health;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private List<GameObject> objectsToDisable;

        private void OnEnable() => health.PlayerDeath += OnPlayerDeath;

        private void OnDisable() => health.PlayerDeath -= OnPlayerDeath;

        private void OnPlayerDeath()
        {
            ProjectContext.Instance.PauseManager.SetPaused(true);
            scoreText.text = ProjectContext.Instance.ScoreManager.Score.ToString();
            DisableObjects();
            losePanel.gameObject.SetActive(true);
        }
        
        private void DisableObjects()
        {
            foreach (var obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }
}