using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class BlasterProjectile : PlayerProjectile
    {
        [SerializeField] private int clearRadiusInPixels = 4;
        [Inject] private ICreepClearing _creepClearing;

        private void LateUpdate()
        {
            _creepClearing.ClearCreep(transform.position, clearRadiusInPixels);
        }
    }
}