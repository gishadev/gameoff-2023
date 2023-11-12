using System;
using Cysharp.Threading.Tasks;
using gameoff.Enemy;
using gameoff.World;
using gishadev.tools.Pooling;
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
        private float _clearTriggerSqrDst;
        private Vector2 _lastClearPos;

        private async void OnEnable()
        {
            _creep = FindObjectOfType<Creep>();
            _clearTriggerSqrDst = Mathf.Pow(clearRadiusInPixels, 2) * Time.deltaTime;

            // TODO: Pooling should change position and than enable an object. This Yield is a workaround.
            await UniTask.Yield();
            _lastClearPos = transform.position;
            await UniTask.WaitForSeconds(lifeTime);
            Die();
        }

        private void Update()
        {
            transform.Translate(transform.right * (flySpeed * Time.deltaTime), Space.World);
        }

        private void LateUpdate()
        {
            var clearSqrDst = ((Vector2) transform.position - _lastClearPos).sqrMagnitude;
            if (clearSqrDst >= _clearTriggerSqrDst)
            {
                _creep.ClearCreep(transform.position, clearRadiusInPixels);
                _lastClearPos = transform.position;
            }
        }

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

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}