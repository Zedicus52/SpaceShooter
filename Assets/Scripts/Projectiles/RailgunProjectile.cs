using System;
using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;
using UnityEngine;

namespace SpaceShooter.Projectiles
{
    public class RailgunProjectile : Projectile
    {
        [Range(-1,1)]
        [SerializeField] private int waveDirection = 1;
        [SerializeField] private float amplitude = 4f;
        [SerializeField] private float waveFrequency = 2f;
        private float _theta;
        private readonly float _thetaStep = Mathf.PI / 64f;
        private float _offsetX;


        private void OnEnable()
        {
            if (_transform == null)
                _transform = GetComponent<Transform>();
            _offsetX = _transform.position.x;
        }

        protected override void Update()
        {
            if(IsPaused)
                return;

            if (_transform.position.y >= ScreenBounds.UpSide || _transform.position.y <= ScreenBounds.DownSide)
            { 
                gameObject.SetActive(false);
                _theta = 0;
                
                return;
            }
            
            float newXPos = waveDirection * amplitude * Mathf.Sin(_theta * waveFrequency) + _offsetX;
            float xStep = newXPos - _transform.position.x;

            _transform.Translate(new Vector3(xStep, speed * Time.deltaTime), Space.Self);

            _theta += _thetaStep;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(IsPaused)
                return;
            
            if (other.TryGetComponent(out IDamageable obj))
            {
                if(obj is PlayerHealth or Shield)
                    return;
                obj.TakeDamage(GetDamage());
            }
        }
    }
}