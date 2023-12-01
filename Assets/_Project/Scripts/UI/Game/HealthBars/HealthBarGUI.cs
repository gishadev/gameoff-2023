using DG.Tweening;
using gameoff.Core;
using gameoff.Enemy;
using gameoff.PlayerManager;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    public  abstract class HealthBarGUI : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private float healthChangingAnimationTime = 0.5f;

        protected abstract IDamageable Damageable { get; set; }
        
        protected virtual void OnEnable()
        {
            OnHealthChanged(Damageable.StartHealth);
            Damageable.HealthChanged += OnHealthChanged;
        }

        protected virtual void OnDisable()
        {
            Damageable.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int healthCount)
        {
            var newScale = (float)healthCount / Damageable.StartHealth;

            fillImage.transform
                .DOScaleX(newScale, healthChangingAnimationTime)
                .SetEase(Ease.InSine);
        }
    }
}