using SpaceShooter.Core;
using TMPro;
using UnityEngine;

namespace SpaceShooter.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreVisualization : MonoBehaviour
    {
        private TMP_Text _scoreText;

        private void Awake() => _scoreText = GetComponent<TMP_Text>();

        private void OnEnable() => ProjectContext.Instance.ScoreManager.ScoreUpdated += UpdateText;
        private void OnDisable() => ProjectContext.Instance.ScoreManager.ScoreUpdated -= UpdateText;
        
        private void UpdateText(int score) => _scoreText.text = score.ToString();
    }
}