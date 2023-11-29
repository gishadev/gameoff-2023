using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace gameoff.Core
{
    public class PhysicsImpactEffector
    {
        private readonly Rigidbody2D _damageableRigidbody;
        private readonly MonoBehaviourWithMovementEffector _movementEffector;

        private float _impactMinVelocity = 0.5f;
        private float _impactMaxTime = .05f;

        private CancellationTokenSource _cts;
        
        public PhysicsImpactEffector(Rigidbody2D damageableRigidbody,
            MonoBehaviourWithMovementEffector movementEffector)
        {
            _damageableRigidbody = damageableRigidbody;
            _movementEffector = movementEffector;
            _cts = new CancellationTokenSource();
            _cts.RegisterRaiseCancelOnDestroy(_damageableRigidbody);
        }

        public async void Act(Vector2 damageOrigin, float impactIntensity)
        {
            if (_cts.Token.IsCancellationRequested)
                return;
            
            _movementEffector.DisableDefaultMovement();
            var impactDirection = (_damageableRigidbody.position - damageOrigin).normalized;
            _damageableRigidbody.AddForce(impactDirection * impactIntensity, ForceMode2D.Impulse);

            var timeTask = UniTask.WaitForSeconds(_impactMaxTime);
            var velocityTask = UniTask.WaitUntil(() => _damageableRigidbody.velocity.magnitude < _impactMinVelocity);
            await UniTask.WhenAny(timeTask, velocityTask);
            
            if (_cts.Token.IsCancellationRequested)
                return;
            
            _movementEffector.EnableDefaultMovement();
        }
    }
}