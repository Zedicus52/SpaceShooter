using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Animations
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(IDamageable))]
    public class DamageableAnimator : MonoBehaviour
    {
        [SerializeField] private string triggerName = "TakeDamage";
        private Animator _controller;
        private IDamageable _damageable;

        private void Awake()
        {
            _controller = GetComponent<Animator>();
            _damageable = GetComponent<IDamageable>();
        } 
      
        private void OnEnable() => _damageable.OnTakeDamage += OnTakeDamage;

        private void OnDisable()
        {
            _controller.ResetTrigger(triggerName);
            _damageable.OnTakeDamage -= OnTakeDamage;
        }
        
        private void OnTakeDamage() =>  _controller.SetTrigger(triggerName);
       
    }
}