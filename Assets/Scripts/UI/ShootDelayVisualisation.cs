using SpaceShooter.Abstraction;
using SpaceShooter.Core;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class ShootDelayVisualisation : BonusVisualisation
    {
        [SerializeField] private PlayerShooter playerShooter;

        protected override void Awake()
        {
            text.text = $"0/{playerShooter.CurrentWeapon.MaxLevelForDelayBonus}";
        }

        protected override void OnEnable() => playerShooter.CurrentWeapon.DecreaseShootDelayEvent += OnNewWeaponSet;
        protected override void OnDisable() => playerShooter.CurrentWeapon.DecreaseShootDelayEvent -= OnNewWeaponSet;

        private void OnNewWeaponSet(PlayerWeapon obj) => 
            text.text = $"{obj.CurrentLevelForDelayBonus}/{obj.MaxLevelForDelayBonus}";
     
    }
}