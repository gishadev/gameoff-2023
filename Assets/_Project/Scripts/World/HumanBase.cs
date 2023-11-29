using System;
using gameoff.Enemy.Projectiles;
using UnityEngine;
using Zenject;

namespace gameoff.World
{
    public class HumanBase : MonoBehaviour
    {
        [SerializeField] private int spawnClearCreepRadius = 300;
        [field: SerializeField] public Transform Spawnpoint { private set; get; }
        
        [Inject] private ICreepClearing _creepClearing;
        
        public static event Action CreepReachedBase;
        private bool _isCreepReached;

        private void Start()
        {
            _creepClearing.ClearCreep(transform.position, spawnClearCreepRadius);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isCreepReached)
                return;

            if (other.TryGetComponent(out HiveProjectile hiveProjectile))
            {
                CreepReachedBase?.Invoke();
                _isCreepReached = true;
            }
        }
    }
}