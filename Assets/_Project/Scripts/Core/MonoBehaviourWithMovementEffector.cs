using UnityEngine;

namespace gameoff.Core
{
    public abstract class MonoBehaviourWithMovementEffector : MonoBehaviour
    {
        public bool IsDefaultMovementEnabled { get; private set; } = true;

        private void OnEnable() => EnableDefaultMovement();

        public void EnableDefaultMovement() => IsDefaultMovementEnabled = true;

        public void DisableDefaultMovement() => IsDefaultMovementEnabled = false;
    }
}