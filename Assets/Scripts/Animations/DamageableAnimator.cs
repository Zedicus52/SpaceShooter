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

        private void Awake() => _controller = GetComponent<Animator>();
      
        private void OnEnable() => GetComponent<IDamageable>().OnTakeDamage += OnTakeDamage;
        
        private void OnDisable() =>  GetComponent<IDamageable>().OnTakeDamage -= OnTakeDamage;
        
        private void OnTakeDamage() =>  _controller.SetTrigger(triggerName);
       
    }
}