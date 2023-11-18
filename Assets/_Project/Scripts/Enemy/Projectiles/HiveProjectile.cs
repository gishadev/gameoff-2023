using System;
using gameoff.Core;
using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.Enemy.Projectiles
{
    public class HiveProjectile : Projectile
    {
        [SerializeField] private int creepRadius;
        
        [Inject] private ICreepClearing _creepClearing;
        
        private void LateUpdate()
        {
            _creepClearing.AddCreep(transform.position, creepRadius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, creepRadius / 10f);
        }
    }
}