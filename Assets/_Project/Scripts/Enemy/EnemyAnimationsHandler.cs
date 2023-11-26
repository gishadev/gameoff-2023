using System;
using DG.Tweening;
using UnityEngine;

namespace gameoff.Enemy
{
    public class EnemyAnimationsHandler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void HandleMovementAnimation(Func<bool> condition)
        {
            if (condition() && !DOTween.IsTweening(_spriteRenderer.transform))
                _spriteRenderer.transform.DOScaleY(.9f, .2f).SetEase(Ease.InSine).OnComplete(() =>
                {
                    _spriteRenderer.transform.DOScaleY(1f, .2f).SetEase(Ease.InSine);
                });
        }

        public void TriggerAttackAnimation(Transform attackTarget)
        {
            if (attackTarget == null || _spriteRenderer == null)
                return;

            _spriteRenderer.transform
                .DOJump(attackTarget.position, 0.5f, 1, 0.1f)
                .OnComplete(() =>
                {
                    if (attackTarget == null || _spriteRenderer == null)
                        return;
                    
                    _spriteRenderer.transform.DOLocalJump(Vector3.zero, 0.5f, 1, 0.1f);
                });
        }
    }
}