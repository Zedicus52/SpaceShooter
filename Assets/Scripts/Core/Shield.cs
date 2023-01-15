using System;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class Shield : MonoBehaviour, IDamageable
    {
        public event Action ShieldDestroy;
        [SerializeField] private int maxHealth;

        private int _currentHealth;

        private void OnEnable() => _currentHealth = maxHealth;
      
        public bool CanDamage() => true;
        
        public void TakeDamage(int damage)
        {
            if(damage <= 0)
                Debug.LogError("Damage cannot be negative or zero");
            
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                ShieldDestroy?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}