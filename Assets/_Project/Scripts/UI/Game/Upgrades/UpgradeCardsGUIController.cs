using System.Collections.Generic;
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
        
        private List<UpgradeCardGUI> _cards = new();
        private UpgradeCardGUI _selectedCard;

        private void OnEnable() => _playerUpgradesController.UpgradesShowed += OnUpgradesShowed;

        private void OnDisable()
        {
            _playerUpgradesController.UpgradesShowed -= OnUpgradesShowed;

            foreach (var card in _cards)
                card.OnClicked -= OnCardClicked;
        }

        public void OnApproveUpgradeClicked()
        {
            if (_selectedCard == null)
                return;

            _playerUpgradesController.Upgrade(_selectedCard.UpgradeEnumType);
        }

        private void OnUpgradesShowed(UpgradeDataSO[] upgradesToShow)
        {
            ClearCards();

            foreach (var upgrade in upgradesToShow)
            {
                var cardGUI = Instantiate(_gameDataSO.UpgradeCardPrefab, cardsParent).GetComponent<UpgradeCardGUI>();
                cardGUI.Setup(upgrade);

                _cards.Add(cardGUI);
                cardGUI.OnClicked += OnCardClicked;
            }
        }

        private void OnCardClicked(UpgradeCardGUI upgradeCardGUI)
        {
            foreach (var card in _cards)
                card.Outline.enabled = false;

            _selectedCard = upgradeCardGUI;
            upgradeCardGUI.Outline.enabled = true;
        }

        private void ClearCards()
        {
            for (int i = 0; i < cardsParent.childCount; i++) Destroy(cardsParent.GetChild(i).gameObject);
            _cards.Clear();
        }
    }
}