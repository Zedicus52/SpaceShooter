using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.Bonuses
{
    public class ShieldBonus : Bonus
    {
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out ShieldHolder holder))
            {
                holder.AddShield();
                Destroy(gameObject);
            }
        }
    }
}