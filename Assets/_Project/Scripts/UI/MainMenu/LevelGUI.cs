using System;
using gameoff.World;
using UnityEngine;
using UnityEngine.EventSystems;

namespace gameoff.UI.MainMenu
{
    public class LevelGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private LevelDataSO levelData;

        public static event Action<LevelDataSO> PointerEnter;
        public static event Action PointerExit;

        public void OnPointerEnter(PointerEventData eventData) => PointerEnter?.Invoke(levelData);

        public void OnPointerExit(PointerEventData eventData) => PointerExit?.Invoke();
    }
}