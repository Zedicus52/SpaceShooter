using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using SpaceShooter.GFX;
using UnityEngine;

namespace SpaceShooter.Bonuses
{
    public class HealthRecovery : Bonus
    {
        [SerializeField] private int minHealthRecover;
        [SerializeField] private int maxHealthRecover;
        protected override  void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerHealth health))
            {
                EffectAudioSource.Instance.PlayOneShot(getSound);
                health.RecoverHealth(Random.Range(minHealthRecover, maxHealthRecover));
                Destroy(gameObject);
            }
        }
    }
}