using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.Bonuses
{
    public class MaxHealth : Bonus
    {
        [Range(1,10)]
        [SerializeField] private float minIncreasePercent;
        [Range(11,30)]
        [SerializeField] private float maxIncreasePercent;
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerHealth player))
            {
                float percent = Random.Range(minIncreasePercent, maxIncreasePercent);
                int healthForIncrease = Mathf.FloorToInt(player.GetMaxHealth() * percent * 0.01f);
                player.IncreaseMaxHealth(healthForIncrease);
                Destroy(gameObject);
            }
        }
    }
}