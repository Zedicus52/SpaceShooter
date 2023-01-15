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
                float delay = shooter.ShootDelay * percent * 0.01f;
                shooter.DecreaseShootDelay(delay);
                Destroy(gameObject);
            }
        }
    }
}