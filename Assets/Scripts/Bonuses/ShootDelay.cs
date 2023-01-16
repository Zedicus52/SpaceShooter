using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.Bonuses
{
    public class ShootDelay : Bonus
    {
        [Range(1,5)]
        [SerializeField] private float minPercent;
        [Range(5,10)]
        [SerializeField] private float maxPercent;
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerShooter shooter))
            { 
                float percent = Random.Range(minPercent, maxPercent);
                shooter.OnWeaponShootDelayChanged(percent);
                Destroy(gameObject);
            }
        }
    }
}