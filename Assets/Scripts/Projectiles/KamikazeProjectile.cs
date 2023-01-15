using SpaceShooter.Abstraction;
using UnityEngine;

namespace SpaceShooter.Projectiles
{
    public class KamikazeProjectile : Projectile
    {
        protected override void Update()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable obj))
            {
                if(obj is Enemy)
                    return;
                obj.TakeDamage(GetDamage());
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}