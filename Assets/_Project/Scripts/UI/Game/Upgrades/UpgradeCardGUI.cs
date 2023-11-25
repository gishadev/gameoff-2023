using System;
using gameoff.PlayerManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    [RequireComponent(typeof(Outline), typeof(Button))]
    public class UpgradeCardGUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private TMP_Text descriptionLabel;

        public UpgradeEnumType UpgradeEnumType { get; private set; }
        public event Action<UpgradeCardGUI> OnClicked;
        public Outline Outline { get; private set; }

        private Button _button;

        private void Awake()
        {
            Outline = GetComponent<Outline>();
            _button = GetComponent<Button>();
        }

        private void OnEnable() => _button.onClick.AddListener(() => OnClicked?.Invoke(this));
        private void OnDisable() => _button.onClick.RemoveAllListeners();

        public void Setup(UpgradeDataSO upgradeData)
        {
            iconImage.sprite = upgradeData.CardIconSprite;
            titleLabel.text = upgradeData.CardTitle;
            descriptionLabel.text = upgradeData.CardDescription;

            UpgradeEnumType = upgradeData.UpgradeEnumType;
        }
    }
}