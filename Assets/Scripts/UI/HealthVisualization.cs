using System;
using SpaceShooter.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    [RequireComponent(typeof(IHealth))]
    public class HealthVisualization : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        private IHealth _objectWithHealth;

        private void Awake()
        {
            _objectWithHealth = GetComponent<IHealth>();
        }

        private void OnEnable()
        {
            _objectWithHealth.HealthChanged += OnHealthChanged;
            OnHealthChanged(_objectWithHealth.GetMaxHealth());
            healthBar.type = Image.Type.Filled;
            healthBar.fillMethod = Image.FillMethod.Horizontal;
            healthBar.fillOrigin = (int)Image.OriginHorizontal.Right;
        }

        private void OnDisable()
        {
            _objectWithHealth.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int newHealth)
        {
            healthBar.fillAmount = (float)newHealth / _objectWithHealth.GetMaxHealth();
        }
    }
}