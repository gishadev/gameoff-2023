using DG.Tweening;
using gameoff.PlayerManager;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    [RequireComponent(typeof(Image))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float healthChangingAnimationTime = 0.5f;

        private Image _image;

        private void Awake() => _image = GetComponent<Image>();

        private void OnEnable() => Player.HealthChanged += OnHealthChanged;
        private void OnDisable() => Player.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int healthCount)
        {
            var player = Player.Current;
            var newScale = (float)healthCount / player.StartHealth;

            _image.transform
                .DOScaleX(newScale, healthChangingAnimationTime)
                .SetEase(Ease.InSine);
        }
    }
}