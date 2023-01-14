using System;
using System.Threading.Tasks;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHealth
    {
        public event Action<int> HealthChanged;
        public bool IsAlive => _currentHealth > 0;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private float takeDamageDelay;

        private int _currentHealth;
        private float _currentTime;
        private bool _timerIsWork;

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
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnHealthChanged(0);
                gameObject.SetActive(false);
            }
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