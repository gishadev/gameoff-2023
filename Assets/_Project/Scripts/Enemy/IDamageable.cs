using System;

namespace gameoff.Enemy
{
    public interface IDamageable
    {
        int StartHealth { get; }
        int CurrentHealth { get; }
        event Action<int> HealthChanged;
        void TakeDamage(int count);
    }
}