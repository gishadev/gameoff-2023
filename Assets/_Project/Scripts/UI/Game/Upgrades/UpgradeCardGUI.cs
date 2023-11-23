using gameoff.PlayerManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    public class UpgradeCardGUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private TMP_Text descriptionLabel;

        public void Setup(UpgradeDataSO upgradeData)
        {
            iconImage.sprite = upgradeData.CardIconSprite;
            titleLabel.text = upgradeData.CardTitle;
            descriptionLabel.text = upgradeData.CardDescription;
        }
    }
}