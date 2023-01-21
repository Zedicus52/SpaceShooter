using System;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHealth
    {
        public event Action<int> HealthChanged;
        public event Action OnTakeDamage;
        public event Action<int, int> MaxHealthChanged;
        public event Action PlayerDeath; 
        public bool IsAlive => _currentHealth > 0;
        public int MaxLevelForMaxHealthBonus => maxLevelForMaxHealthBonus;

        [SerializeField] private int maxLevelForMaxHealthBonus;
        [SerializeField] private int maxHealth;
        [SerializeField] private float takeDamageDelay;
        [SerializeField] private AudioSource audioSource;

        private int _currentHealth;
        private float _currentTime;
        private bool _timerIsWork;
        private int _currentLevelMaxHealthBonus;

        private void OnEnable()
        {
            _currentHealth = maxHealth;
            RestartTimer();
        } 

        private void OnDisable()
        {
            _currentHealth = 0; 
            RestartTimer();
        }


        
        public bool CanDamage() =>  _currentTime.Equals(takeDamageDelay);
       
        private void Update()
        {
            if (_timerIsWork)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    RestartTimer();
                }
            }
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamageable obj))
            {
                if (obj is Meteor)
                    _timerIsWork = true;
            }
        }

        public void TakeDamage(int damage)
        {
            if(IsAlive == false || CanDamage() == false)
                return;
            
            if(damage <= 0)
                Debug.LogError("Damage cannot be negative or zero");
            
            _currentHealth -= damage;
            OnTakeDamage?.Invoke();
            audioSource.Play();
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnHealthChanged(0);
                PlayerDeath?.Invoke();
                gameObject.SetActive(false);
            }
            OnHealthChanged(_currentHealth);
            
        }

        public void RecoverHealth(int health)
        {
            if(IsAlive == false || _currentHealth == maxHealth)
                return;
            
            if(health <= 0)
                Debug.LogError("Health cannot be negative or zero");

            if (_currentHealth + health > maxHealth)
                _currentHealth += maxHealth - _currentHealth;
            else
                _currentHealth += health;
            OnHealthChanged(_currentHealth);
        }

        public void IncreaseMaxHealth(int health)
        {
            if(IsAlive == false || _currentLevelMaxHealthBonus >= maxLevelForMaxHealthBonus)
                return;
            
            if(health <= 0)
                Debug.LogError("Health cannot be negative or zero");

            maxHealth += health;
            _currentHealth += health;
            
            _currentLevelMaxHealthBonus += 1;
            MaxHealthChanged?.Invoke(_currentLevelMaxHealthBonus, maxLevelForMaxHealthBonus);
            OnHealthChanged(_currentHealth);

        }

        private void RestartTimer()
        {
            _currentTime = takeDamageDelay;
            _timerIsWork = false;
        }


        public int GetMaxHealth() => maxHealth;
        public int GetMinHealth() => 0;
        

        protected virtual void OnHealthChanged(int obj)
        {
            HealthChanged?.Invoke(obj);
        }
    }
}