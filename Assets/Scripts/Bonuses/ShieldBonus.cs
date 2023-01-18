using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.GFX;
using UnityEngine;

namespace SpaceShooter.Bonuses
{
    public class ShieldBonus : Bonus
    {
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out ShieldHolder holder))
            {
                EffectAudioSource.Instance.PlayOneShot(getSound);
                holder.AddShield();
                Destroy(gameObject);
            }
        }
    }
}