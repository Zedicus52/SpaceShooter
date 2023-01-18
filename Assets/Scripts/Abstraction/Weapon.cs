using SpaceShooter.Core;
using UnityEngine;


namespace SpaceShooter.Abstraction
{
    public abstract class Weapon : MonoBehaviour
    {
        public float ShootDelay => shootDelay;
        public Projectile Projectile => projectile;
        public AudioClip ShootSound => shootSound;
        [SerializeField] protected float shootDelay;
        [SerializeField] protected Projectile projectile;
        [SerializeField] protected int basicCount;
        [SerializeField] protected bool invertedDirectionForProjectile;
        [SerializeField] protected AudioClip shootSound;

        protected Pool<Projectile> _pool;
        private bool _isInitialized;

        public void InitializeWeapon(Transform projectileParent)
        {
            if(_isInitialized)
                return;
            _pool = new Pool<Projectile>(projectile,projectileParent, true, basicCount);
            _isInitialized = true;
        }

        public virtual void Shoot()
        {
            var proj = _pool.GetObject();
            proj.InitializeProjectile(invertedDirectionForProjectile);
            proj.transform.position = new Vector3(transform.position.x, transform.position.y-proj.SpriteHeight, 
                    transform.position.z);
            proj.gameObject.SetActive(true);
        }
    }
}