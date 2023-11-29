using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace gameoff.Core
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] protected float FlySpeed { private set; get; } = 30f;
        [field: SerializeField] protected float LifeTime { private set; get; } = 0.5f;
        [field: SerializeField] protected float ImpulseImpactForce { private set; get; } = 1f;

        private CancellationTokenSource _cts;

        protected virtual void OnEnable()
        {
            _cts = new CancellationTokenSource();
            _cts.RegisterRaiseCancelOnDestroy(this);

            LifetimeAsync();
        }

        protected virtual void Update()
        {
            transform.Translate(transform.right * (FlySpeed * Time.deltaTime), Space.World);
        }

        protected void Die()
        {
            gameObject.SetActive(false);
        }

        private async void LifetimeAsync()
        {
            await UniTask.WaitForSeconds(LifeTime);
            if (!_cts.IsCancellationRequested)
                Die();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageableWithPhysicsImpact damageable))
                damageable.PhysicsImpactEffector.Act(transform.position, ImpulseImpactForce);
        }
    }
}