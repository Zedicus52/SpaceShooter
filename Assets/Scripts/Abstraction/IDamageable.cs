namespace SpaceShooter.Abstraction
{
    public interface IDamageable
    {
        bool CanDamage();
        void TakeDamage(int damage);
    }
}