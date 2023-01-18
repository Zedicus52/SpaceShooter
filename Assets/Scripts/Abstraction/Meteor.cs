using System;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;
using SpaceShooter.GFX;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SpaceShooter.Abstraction
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Meteor : MonoBehaviour, IDamageable, IPauseHandler, IHealth
    {
        public event Action<int> HealthChanged;
        public event Action OnTakeDamage;
        protected event Action<Meteor> OnMeteorDestroy; 
        public BorderFloat Border { get; private set; }
        public Vector3 SpriteSize { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsAlive => _currentHealth > 0;
        public AudioClip DestroySound => destroySound;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private Particle destroyParticle;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int maxDamage;
        [SerializeField] private int minDamage;
        [SerializeField] private float giveDamageDelay;
        [SerializeField] protected AudioClip destroySound;

        private Transform _transform;
        private int _currentHealth;
        private SpriteRenderer _spriteRenderer;
        private float _currentTime;
        private bool _timerIsWork;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = transform;
        }

        private void Start()
        {
            ProjectContext.Instance.PauseManager.Register(this);
        }

        private void Update()
        {
            if(IsPaused)
                return;

            if (_timerIsWork)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    RestartTimer();
                }
            }
            
            if (_transform.position.y >= Border.UpSide || _transform.position.y <= Border.DownSide)
            {
                gameObject.SetActive(false);
                return;
            }
            
            _transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        private void RestartTimer()
        {
            _currentTime = giveDamageDelay;
            _timerIsWork = false;
        }
        public virtual bool CanDamage() => _currentTime.Equals(giveDamageDelay);
        
        public virtual void TakeDamage(int damage)
        {
            if(IsAlive == false)
                return;
            
            if(damage <= 0)
                Debug.LogError("Damage cannot be negative or zero");

            
            _currentHealth -= damage;
            OnTakeDamage?.Invoke();
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                MeteorDestroy(true);
            }
            OnHealthChanged(_currentHealth);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamageable obj))
            {
                if(obj is Meteor)
                    return;
                if (obj.CanDamage() && CanDamage())
                {
                    obj.TakeDamage(Random.Range(minDamage, maxDamage));
                    MeteorDestroy(false);
                    _timerIsWork = true;
                }
                
            }
        }

        protected abstract void MeteorDestroy(bool destroyByPlayer);

        protected virtual void OnEnable()
        {
            RestartTimer();
            _spriteRenderer.color = Color.white;
            _currentHealth = maxHealth;
            OnHealthChanged(_currentHealth);
        }

        protected virtual void OnDisable()
        {
            _spriteRenderer.color = Color.white;
            RestartTimer();
            _currentHealth = 0;
            OnHealthChanged(_currentHealth);
        }

        public virtual void InitializeMeteor(BorderFloat border)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            SpriteSize = renderer.sprite.bounds.size;
            Border = new BorderFloat(border.LeftSide, border.RightSide, border.DownSide, border.UpSide);
            Border.ExpendLeftSide(SpriteSize.x*0.5f);
            Border.ExpendRightSide(SpriteSize.x*0.5f);
            Border.ExpendDownSide(SpriteSize.y);
            Border.ExpendUpSide(SpriteSize.y);
        }

        protected virtual void OnMeteorDestroyInvoker(Meteor obj)
        {
            OnMeteorDestroy?.Invoke(obj);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
        }

        public virtual void SetPaused(bool isPaused) => IsPaused = isPaused;
        private void OnDestroy() => ProjectContext.Instance.PauseManager.UnRegister(this);

        public virtual int GetMaxHealth() => maxHealth;
        public virtual int GetMinHealth() => 0;
        
        protected virtual void OnHealthChanged(int obj)
        {
            HealthChanged?.Invoke(obj);
        }
    }
}