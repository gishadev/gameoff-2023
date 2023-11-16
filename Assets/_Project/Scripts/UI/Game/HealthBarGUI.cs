using DG.Tweening;
using gameoff.PlayerManager;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    public class HealthBarGUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private float healthChangingAnimationTime = 0.5f;

        private void OnEnable() => Player.HealthChanged += OnHealthChanged;
        private void OnDisable() => Player.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int healthCount)
        {
            var player = Player.Current;
            var newScale = (float)healthCount / player.StartHealth;

            image.transform
                .DOScaleX(newScale, healthChangingAnimationTime)
                .SetEase(Ease.InSine);
        }
    }
}