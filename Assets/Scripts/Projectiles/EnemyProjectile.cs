using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Projectiles
{
    public class EnemyProjectile : Projectile
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(IsPaused)
                return;
            
            if (other.TryGetComponent(out IDamageable obj))
            {
                if(obj is Enemy)
                    return;
                obj.TakeDamage(GetDamage());
                gameObject.SetActive(false);
            }
        }
    }
}