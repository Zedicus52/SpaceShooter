using UnityEngine;

namespace SpaceShooter.Abstraction
{
    public abstract class PlayerWeapon : Weapon
    {
        public Sprite WeaponIcon => weaponIcon;
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
            float delay = shootDelay * percent * 0.01f;
            shootDelay -= delay;
            _currentLevelDelayBonus += 1;
        }
    }
}