
using System;

namespace SpaceShooter.Abstraction
{
    public interface IHealth
    {
        event Action<int> HealthChanged;
        int GetMaxHealth();
        int GetMinHealth();
    }
}