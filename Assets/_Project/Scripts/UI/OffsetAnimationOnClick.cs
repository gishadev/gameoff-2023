using UnityEngine;
using UnityEngine.EventSystems;

namespace gameoff.UI
{
    public class OffsetAnimationOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform objectToMove;

        [SerializeField] private float yOffset = 1f;

        private Vector2 _startPos;

        private void Awake() => _startPos = objectToMove.anchoredPosition;

        public void OnPointerDown(PointerEventData eventData) =>
            objectToMove.anchoredPosition = _startPos + Vector2.up * yOffset;

        public void OnPointerUp(PointerEventData eventData) => objectToMove.anchoredPosition = _startPos;
    }
}