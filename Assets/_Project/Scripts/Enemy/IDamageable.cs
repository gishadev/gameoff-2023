namespace gameoff.Enemy
{
    public interface IDamageable
    {
        int StartHealth { get; }
        int CurrentHealth { get; }
        void TakeDamage(int count);
    }
}