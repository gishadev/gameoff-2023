using gameoff.Core;
using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.Enemy.Projectiles
{
    public class HiveProjectile : Projectile
    {
        [SerializeField] private int creepRadius;
        [SerializeField] private int creepPixelsChangedLimit = 100;

        [Inject] private ICreepClearing _creepClearing;
        private int _pixelsChangedOverall;

        protected override void OnEnable()
        {
            base.OnEnable();
            _pixelsChangedOverall = 0;
        }

        private void LateUpdate()
        {
            _creepClearing.AddCreep(transform.position, creepRadius, out var pixelsChanged);
            _pixelsChangedOverall += pixelsChanged;
            
            if (_pixelsChangedOverall >= creepPixelsChangedLimit)
                Die();
        }

        // protected override void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.TryGetComponent(out HumanBase humanBase)) 
        //         Die();
        // }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, creepRadius / 10f);
        }
    }
}