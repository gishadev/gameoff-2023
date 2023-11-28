using System;
using gameoff.Core;
using gameoff.World;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace gameoff.UI.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class LevelGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private LevelDataSO levelData;
        [SerializeField] private TMP_Text numberLabel;

        [SerializeField] private SectorGUI[] occupiedSectors;
        [SerializeField] private GameObject completedIcon, infectedIcon, closedIcon;

        public static event Action<LevelDataSO> PointerEnter;
        public static event Action PointerExit;
        public LevelDataSO LevelData => levelData;

        private Button _button;

        public void OnPointerEnter(PointerEventData eventData) => PointerEnter?.Invoke(LevelData);
        public void OnPointerExit(PointerEventData eventData) => PointerExit?.Invoke();

        private void Awake()
        {
            _button = GetComponent<Button>();
            numberLabel.text = (LevelData.LevelOrder).ToString();
        }

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveAllListeners();

        private void OnClick()
        {
            GameManager.SetCurrentLevel(LevelData.LevelOrder);
            MainMenuController.OnPlayClicked();
        }

        [Button]
        public void SetCompleted()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            foreach (var sector in occupiedSectors)
                sector.ShowCompleted();

            closedIcon.SetActive(false);
            infectedIcon.SetActive(false);
            completedIcon.SetActive(true);

            _button.interactable = true;
        }

        [Button]
        public void SetClosed()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            foreach (var sector in occupiedSectors)
                sector.ShowClosed();

            closedIcon.SetActive(true);
            infectedIcon.SetActive(false);
            completedIcon.SetActive(false);

            _button.interactable = false;
        }

        [Button]
        public void SetInfected()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            foreach (var sector in occupiedSectors)
                sector.ShowInfected();

            closedIcon.SetActive(false);
            infectedIcon.SetActive(true);
            completedIcon.SetActive(false);

            _button.interactable = true;
        }
    }
}