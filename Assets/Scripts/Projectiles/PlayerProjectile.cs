using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.Projectiles
{
    public class PlayerProjectile : Projectile
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(IsPaused)
                return;
            
            if (other.TryGetComponent(out IDamageable obj))
            {
                if(obj is PlayerHealth or Shield)
                    return;
                obj.TakeDamage(GetDamage());
                gameObject.SetActive(false);
            }
        }
    }
}