using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceShooter.Abstraction;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceShooter.Projectiles
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BombardierProjectile : Projectile
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionStep;

        private IEnumerator _lifeCycleCoroutine;
        private CircleCollider2D _circleCollider;
        private List<Collider2D> _colliders;
        private float _defaultRadius;
        private void Awake()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            _lifeCycleCoroutine = LifeCycle();
            _defaultRadius = _circleCollider.radius;
        }

        protected override void Update()
        {
        }

        private void OnEnable()
        {
            _colliders = new List<Collider2D>();
            _circleCollider.radius = _defaultRadius;    
            StartCoroutine(LifeCycle());
        }
        
        private IEnumerator LifeCycle()
        {
            yield return new WaitForSeconds(lifeTime);
            StartCoroutine(Explode());
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(IsPaused)
                return;
            _colliders.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(IsPaused)
                return;
            _colliders.Remove(other);
        }

        private IEnumerator Explode()
        {
            while (_circleCollider.radius < explosionRadius)
            {
                _circleCollider.radius += explosionStep * Time.deltaTime;
            }

            yield return new WaitForSeconds(0.5f);
            foreach (var col in _colliders)
            {
                if (col.TryGetComponent(out IDamageable obj))
                {
                    obj.TakeDamage(GetDamage());
                }
            }
            gameObject.SetActive(false);

        }
    }
}