

using System;

namespace SpaceShooter.Abstraction
{
    public interface IDamageable
    {
        event Action OnTakeDamage;
        bool CanDamage();
        void TakeDamage(int damage);
    }
}