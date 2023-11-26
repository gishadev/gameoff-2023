using System;
using System.Collections.Generic;
using gameoff.PlayerManager;
using UnityEngine;

namespace gameoff.Enemy
{
    public class EnemyProjectileShield : MonoBehaviour
    {
        [SerializeField] private Collider2D projectileShieldCollider;

        private Player _player;
        private float _radius;

        private void Awake()
        {
            _radius = transform.localScale.x / 2f;
        }

        private void OnEnable()
        {
            _player = Player.Current;
        }

        private List<EnemyProjectileShield> GetNearbyShields()
        {
            var shields = FindObjectsOfType<EnemyProjectileShield>();
            List<EnemyProjectileShield> result = new List<EnemyProjectileShield>();

            foreach (var shield in shields)
            {
                var distance = Vector3.Distance(shield.transform.position, transform.position) - _radius;
                distance = Mathf.Max(0f, distance);
                if (distance < _radius)
                    result.Add(shield);
            }

            return result;
        }

        private bool IsPlayerInsideOneOfShields(List<EnemyProjectileShield> shields)
        {
            foreach (var shieldToCheck in shields)
            {
                var distance = Vector3.Distance(shieldToCheck.transform.position, _player.transform.position);
                if (distance < _radius)
                    return true;
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) && !other.TryGetComponent(out EnemyProjectileShield _))
                return;

            var nearbyShields = GetNearbyShields();
            foreach (var shield in nearbyShields) shield.projectileShieldCollider.enabled = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) && !other.TryGetComponent(out EnemyProjectileShield _))
                return;

            var nearbyShields = GetNearbyShields();
            var isInside = IsPlayerInsideOneOfShields(nearbyShields);
            if (!isInside)
                foreach (var shield in nearbyShields)
                    shield.projectileShieldCollider.enabled = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}