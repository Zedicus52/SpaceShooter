
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class MaxHealthVisualisation : BonusVisualisation
    {
        [SerializeField] private PlayerHealth playerHealth;
        protected override void Awake()
        {
            text.text = $"0/{playerHealth.MaxLevelForMaxHealthBonus}";
        }

        protected override void OnEnable() => playerHealth.MaxHealthChanged += OnMaxHealthChanged;
        
        protected override void OnDisable() => playerHealth.MaxHealthChanged -= OnMaxHealthChanged;

        private void OnMaxHealthChanged(int currentLevel, int maxLevel) =>
            text.text = $"{currentLevel}/{maxLevel}";


    }
}