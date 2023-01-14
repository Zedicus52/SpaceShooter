using System;
using System.Collections;
using SpaceShooter.Abstraction;
using SpaceShooter.DataStructures;
using UnityEngine;

namespace SpaceShooter.Projectiles
{
    public class LaserProjectile : Projectile
    {
        public float AwaitTime => stepForDrawing * 0.5f - lifeTime * 0.5f;
        [SerializeField] private float stepForDrawing;
        [SerializeField] private float lifeTime;
        private int _maxSpriteHeight;
        private SpriteRenderer _renderer;
        private BoxCollider2D _collider;
        private float _startSize;
        private float _startOffset;
        private void Awake()
        {
            _renderer= GetComponent<SpriteRenderer>();
            _collider= GetComponent<BoxCollider2D>();
            _transform = transform;
            _renderer.drawMode = SpriteDrawMode.Tiled;
            _startSize = _renderer.size.y;
            _startOffset = _collider.offset.y;
            _maxSpriteHeight = Mathf.CeilToInt(ScreenBounds.UpSide) * 2;
        }

        public override void InitializeProjectile(bool isInverted = false)
        {
            base.InitializeProjectile(isInverted);
            _transform.position =
                new Vector3(_transform.position.x, _transform.position.y - 0.55f, _transform.position.z);
        }

        protected void OnDisable()
        {
            _renderer.size = new Vector2(_renderer.size.x, _startSize);
            _collider.size = _renderer.size;
            _collider.offset = new Vector2(_collider.size.x, _startOffset);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(IsPaused)
                return;
            
            if (other.TryGetComponent(out IDamageable obj))
            {
                if(obj is Enemy)
                    return;
                obj.TakeDamage(GetDamage());
            }
        }

        protected override void Update()
        {
            if (_renderer.size.y < _maxSpriteHeight)
            {
                _renderer.size = new Vector2(_renderer.size.x, _renderer.size.y +( stepForDrawing * Time.deltaTime));
                _collider.size = _renderer.size;
                _collider.offset = new Vector2(_collider.offset.x, -_renderer.size.y * 0.45f);
                if (_renderer.size.y >= _maxSpriteHeight)
                {
                    StartCoroutine(LifeCycle());
                }
                    
            }
        }

        private IEnumerator LifeCycle()
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }
    }
}