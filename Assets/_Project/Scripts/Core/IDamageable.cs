using System;
using UnityEngine;

namespace gameoff.Core
{
    public interface IDamageable
    {
        int StartHealth { get; }
        int CurrentHealth { get; }
        event Action<int> HealthChanged;
        void TakeDamage(int count);

        GameObject gameObject { get; }
        Transform transform { get; }
    }
}