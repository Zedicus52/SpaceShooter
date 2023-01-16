using UnityEngine;

namespace SpaceShooter.Abstraction
{
    public abstract class PlayerWeapon : Weapon
    {
        [SerializeField] protected int maxLevelForUpgrade;
        [SerializeField] protected int maxLevelForDelayBonus;
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