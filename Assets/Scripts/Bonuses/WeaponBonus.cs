using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.Bonuses
{
    public class WeaponBonus : Bonus
    {
        [SerializeField] private PlayerWeapon weapon;
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerShooter shooter))
            {
                shooter.OnWeaponChanged(weapon);
                Destroy(gameObject);
            }
        }
    }
}