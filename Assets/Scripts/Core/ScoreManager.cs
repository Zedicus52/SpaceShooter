using System;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class ScoreManager
    {
        public event Action<int> ScoreUpdated;

        public int Score { get; private set; }
        
        public void AddPoints(int points)
        {
            if(points <= 0)
                Debug.LogError($"Point cannot be equals zero or negative {GetType()}");
            Score += points;
            OnScoreUpdated(Score);
        }

        public void ResetPoints()
        {
            Score = 0;
            OnScoreUpdated(Score);
        }

        protected virtual void OnScoreUpdated(int obj) => ScoreUpdated?.Invoke(obj);
       
    }
}