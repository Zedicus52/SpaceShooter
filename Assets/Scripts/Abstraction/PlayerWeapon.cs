using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter.Abstraction
{
    public abstract class PlayerWeapon : Weapon
    {
        public event Action<PlayerWeapon> DecreaseShootDelayEvent;
        public Sprite WeaponIcon => weaponIcon;
        public int MaxLevelForDelayBonus => maxLevelForDelayBonus;
        public int CurrentLevelForDelayBonus => _currentLevelDelayBonus+1;
        public Sprite WeaponIconBackground => weaponIconBackground;
        [SerializeField] protected int maxLevelForUpgrade;
        [SerializeField] protected int maxLevelForDelayBonus;
        [SerializeField] protected Sprite weaponIcon;
        [SerializeField] protected Sprite weaponIconBackground;
        protected int _currentLevel;
        protected int _currentLevelDelayBonus;
        
        public void DecreaseShootDelay(float percent)
        {
            if(_currentLevelDelayBonus >= maxLevelForDelayBonus)
                return;
            DecreaseShootDelayEvent?.Invoke(this);
            float delay = shootDelay * percent * 0.01f;
            shootDelay -= delay;
            _currentLevelDelayBonus += 1;
        }
    }
}