using System;
using UnityEngine;

namespace SpaceShooter.GFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Particle : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if(_particleSystem.IsAlive() == false)
                Destroy(gameObject);
        }

        private void OnEnable() => _particleSystem.Play();

        private void OnDisable() => _particleSystem.Stop();

       
    }
}