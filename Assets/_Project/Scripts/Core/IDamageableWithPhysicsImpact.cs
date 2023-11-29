namespace gameoff.Core
{
    public interface IDamageableWithPhysicsImpact : IDamageable
    {
        PhysicsImpactEffector PhysicsImpactEffector { get; }
    }
}