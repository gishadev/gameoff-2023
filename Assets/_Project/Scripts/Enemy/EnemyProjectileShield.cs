using gameoff.PlayerManager;
using UnityEngine;

namespace gameoff.Enemy
{
    public class EnemyProjectileShield : MonoBehaviour
    {
        [SerializeField] private Collider2D projectileShieldCollider;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player player))
                return;

            projectileShieldCollider.enabled = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player player))
                return;
            
            projectileShieldCollider.enabled = true;
        }
    }
}