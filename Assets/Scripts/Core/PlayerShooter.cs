using System.Collections;
using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Core
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerShooter : MonoBehaviour, IPauseHandler
    {
        public bool IsPaused { get; private set; }
        public float ShootDelay => shootDelay;
        [SerializeField] private int basicCount;
        [SerializeField] private Projectile prefab;
        [SerializeField] private float shootDelay;
        [SerializeField] private int maxLevelForShootDelayBonus;
        [SerializeField] private Transform parentForProjectiles;
        private Pool<Projectile> _projectilePool;
        private bool _canShoot = true;
        private IEnumerator _shootCoroutine;
        private int _currentLevelBonus;
        
        

        private void Awake()
        {
            _projectilePool = new Pool<Projectile>(prefab, parentForProjectiles, true, basicCount);
            ProjectContext.Instance.PauseManager.Register(this);
            _shootCoroutine = Shoot();
        }

        private void OnEnable() => StartShoot();
      
        private void OnDisable() => StopShoot();
       
        private IEnumerator Shoot()
        {
            while (_canShoot)
            {
                var projectile = _projectilePool.GetObject();
                projectile.InitializeProjectile();
                var position = transform.position;
                projectile.transform.position = new Vector3(position.x,
                    position.y+projectile.SpriteHeight, position.z);
                projectile.gameObject.SetActive(true);
                yield return new WaitForSeconds(shootDelay);
            }
        }

        private void StopShoot()
        {
            _canShoot = false;
            StopCoroutine(_shootCoroutine);
        }
        private void StartShoot()
        {
            _canShoot = true;
            StartCoroutine(_shootCoroutine);
        }
        public void SetPaused(bool isPaused) => IsPaused = isPaused;

        private void OnDestroy() => ProjectContext.Instance.PauseManager.UnRegister(this);

        public void DecreaseShootDelay(float delay)
        {
            if(_currentLevelBonus >= maxLevelForShootDelayBonus)
                return;
            
            if(delay <= 0)
                Debug.LogError("Delay cannot be negative or zero");
            
            shootDelay -= delay;
            _currentLevelBonus += 1;
        }

    }
}
 