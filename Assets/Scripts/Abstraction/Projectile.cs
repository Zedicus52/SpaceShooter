using SpaceShooter.Core;
using SpaceShooter.DataStructures;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Abstraction
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class Projectile : MonoBehaviour, IPauseHandler
    {
        public float SpriteHeight { get; private set; }
        public bool IsPaused { get; private set; }
        [SerializeField] private int minDamage;
        [SerializeField] private int maxDamage;

        [SerializeField] protected float speed;
        protected Transform _transform;
        private bool _isInitialized;

        public int GetDamage() => Random.Range(minDamage, maxDamage);

        public virtual void InitializeProjectile(bool isInverted = false)
        {
            if(_isInitialized)
                return;
            SpriteRenderer component = GetComponent<SpriteRenderer>();
            _transform = transform;
            SpriteHeight = component.sprite.bounds.size.y;
            if (isInverted)
            {
                speed *= -1;
                _transform.Rotate(Vector3.forward,180);
            }
            
            ProjectContext.Instance.PauseManager.Register(this);
            _isInitialized = true;  
        }

       
        protected virtual void Update()
        {
            if(IsPaused)
                return;
            
            if (_transform.position.y >= ScreenBounds.UpSide || _transform.position.y <= ScreenBounds.DownSide)
            { 
                gameObject.SetActive(false);
                return;
            }

            float newY = _transform.position.y + speed * Time.deltaTime;
            
            _transform.position = new Vector3(_transform.position.x, newY, transform.position.z);

        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(IsPaused)
                return;
            
            if (other.TryGetComponent(out IDamageable obj))
            {
                obj.TakeDamage(GetDamage());
                gameObject.SetActive(false);
            }
        }

        public void SetPaused(bool isPaused) =>  IsPaused = isPaused;
        private void OnDestroy() => ProjectContext.Instance.PauseManager.UnRegister(this);
        
    }
}