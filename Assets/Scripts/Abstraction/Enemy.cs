    using System;
using System.Collections;
using System.Threading.Tasks;
using SpaceShooter.Core;
using SpaceShooter.DataStructures;
using SpaceShooter.GFX;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Abstraction
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Weapon))]
    public abstract class Enemy : MonoBehaviour, IDamageable, IPauseHandler, IHealth
    {
        public event Action<int> HealthChanged;
        public event Action<Enemy> EnemyDestroyed;
        public event Action OnTakeDamage;

        public int Score { get; protected set; }
        public Vector3 SpriteSize { get; private set; }
        public BorderFloat Border { get; private set; }
        public AudioClip DestroyClip => destroyClip;
        public bool IsAlive => _currentHealth > 0;

        [SerializeField] protected bool isDebug;

        [SerializeField] protected WeightRandomList<Bonus> bonuses;
        [Range(0f,1f)]
        [SerializeField] protected float bonusDropChance;
        [SerializeField] protected float speed;
        [SerializeField] protected int maxHealth;
        [SerializeField] protected Particle destroyParticle;
        [SerializeField] protected AudioClip destroyClip;
        [SerializeField] protected AudioSource audioSource;
        protected Transform _transform;
        protected Weapon _weapon;

        protected IEnumerator _shootCoroutine;
        protected bool _enemyCanShoot;
        protected bool _canMove;
        protected int _currentHealth;
        protected SpriteRenderer _spriteRenderer;
        protected bool _isIntialized;

        protected virtual void Awake()
        {
            _shootCoroutine = Shoot();
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected void Initialize(Transform projectileParentObject)
        {
            if(_isIntialized)
                return;
            if (TryGetComponent(out Weapon weapon))
            {
                _weapon = weapon;
                _weapon.InitializeWeapon(projectileParentObject);
                _enemyCanShoot = true;
            }
            _canMove = true;
            
            CalculateSpriteSize();
            _isIntialized = true;
        }

        public void InitializeEnemy(Transform projectileParentObject) => Initialize(projectileParentObject);
        

        public virtual async void OnEnable()
        {
            if(isDebug)
                Initialize(transform);
            _spriteRenderer.color = Color.white;
            _currentHealth = maxHealth;
            _canMove = true;
            _enemyCanShoot = false;
            await Task.Delay(100);
            _enemyCanShoot = true;
            if(_weapon != null && gameObject.activeInHierarchy)
                StartCoroutine(_shootCoroutine);
        }
        public abstract void Update();

        protected virtual void OnDisable()
        {
            _canMove = false;
            _spriteRenderer.color = Color.white;
            _enemyCanShoot = false;
            if(_weapon != null)
                StopCoroutine(_shootCoroutine);
        }
      

        protected virtual IEnumerator Shoot()
        {
            while(_enemyCanShoot)
            {
                _weapon.Shoot();
                audioSource.PlayOneShot(_weapon.ShootSound);
                yield return new WaitForSeconds(_weapon.ShootDelay);
            }
        }

        private void CalculateSpriteSize()
        {
            var renderer = GetComponent<SpriteRenderer>();
            SpriteSize = renderer.sprite.bounds.size;
            CalculateBorder();
        }
        private void CalculateBorder()
        {
            Border = new BorderFloat(ScreenBounds.LeftSide, ScreenBounds.RightSide, ScreenBounds.DownSide, ScreenBounds.UpSide);
            Border.ExpendLeftSide(SpriteSize.x*0.5f);
            Border.ExpendRightSide(SpriteSize.x*0.5f);
            Border.ExpendUpSide(SpriteSize.y*0.5f);
            Border.ExpendDownSide(SpriteSize.y*0.5f);
        }
        
        public virtual bool CanDamage() => true;
       

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
                OnEnemyDestroyed();
            }
            OnHealthChanged(_currentHealth);
        }

        public void RestoreHealth(int health)
        {
            if(IsAlive == false)
                return;
            
            if(health <= 0)
                Debug.LogError("Damage cannot be negative or zero");

            if(_currentHealth >= maxHealth)
                return;
            _currentHealth += health;
            OnHealthChanged(_currentHealth);

        }
        
        public virtual void SetPaused(bool isPaused)
        {
            _canMove = !isPaused;
        }

        public virtual int GetMaxHealth() => maxHealth;


        public virtual int GetMinHealth() => 0;
       

        protected virtual void OnHealthChanged(int obj)
        {
            HealthChanged?.Invoke(obj);
        }

        protected virtual void OnEnemyDestroyed()
        {
            ProjectContext.Instance.ScoreManager.AddPoints(Score);
            EnemyDestroyed?.Invoke(this);
            Instantiate(destroyParticle, _transform.position, Quaternion.identity);
            if (bonuses.Count != 0)
            {
                if (Random.value < bonusDropChance)
                {
                    var bonus = bonuses.GetRandom();
                    Instantiate(bonus.gameObject,  _transform.position, Quaternion.identity);
                }
            }
            gameObject.SetActive(false);
        }
    }
}