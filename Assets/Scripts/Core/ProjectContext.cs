using System;
using SpaceShooter.UI;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class ProjectContext : MonoBehaviour
    {
        public static ProjectContext Instance { get; private set; }
        public PauseManager PauseManager { get; private set; }
        public ScoreManager ScoreManager { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Initialize();
                return;
            }
            Destroy(gameObject);
        }

        private void Initialize()
        {
            PauseManager = new PauseManager();
            ScoreManager = new ScoreManager();
        }
    }
}