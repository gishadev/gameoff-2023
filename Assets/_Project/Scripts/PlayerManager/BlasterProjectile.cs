using System;
using gameoff.Enemy;
using gameoff.World;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class BlasterProjectile : MonoBehaviour
    {
        [SerializeField] private float flySpeed = 4f;
        [SerializeField] private float lifeTime = 0.5f;
        [SerializeField] private int clearRadiusInPixels = 3;

        private int _damageCount = 1;
        private Creep _creep;

        private void Awake()
        {
            _creep = FindObjectOfType<Creep>();
        }

        private void Start()
        {
            // TODO: Pooling neeeded!
            Destroy(gameObject, lifeTime);
        }

        private void Update() => transform.Translate(transform.right * (flySpeed * Time.deltaTime), Space.World);
        private void LateUpdate() => _creep.ClearCreep(transform.position, clearRadiusInPixels);

        public void SetDamage(int damageCount)
        {
            _damageCount = damageCount;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damageCount);

            Die();
        }

        // TODO: Pooling neeeded!
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}