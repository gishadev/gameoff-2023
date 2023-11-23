using gameoff.Core;
using gameoff.PlayerManager;
using UnityEngine;
using Zenject;

namespace gameoff.UI.Game
{
    public class UpgradeCardsGUIController : MonoBehaviour
    {
        [SerializeField] private Transform cardsParent;

        [Inject] private IPlayerUpgradesController _playerUpgradesController;
        [Inject] private GameDataSO _gameDataSO;

        private void OnEnable() => _playerUpgradesController.UpgradesShowed += OnUpgradesShowed;
        private void OnDisable() => _playerUpgradesController.UpgradesShowed -= OnUpgradesShowed;

        private void OnUpgradesShowed(UpgradeDataSO[] upgradesToShow)
        {
            ClearCards();

            foreach (var upgrade in upgradesToShow)
            {
                var cardGUI = Instantiate(_gameDataSO.UpgradeCardPrefab, cardsParent).GetComponent<UpgradeCardGUI>();
                cardGUI.Setup(upgrade);
            }
        }

        private void ClearCards()
        {
            for (int i = 0; i < cardsParent.childCount; i++) Destroy(cardsParent.GetChild(i).gameObject);
        }
    }
}