namespace gameoff.Enemy
{
    public interface IDamageable
    {
        int CurrentHealth { get; }
        void TakeDamage(int count);
    }
}