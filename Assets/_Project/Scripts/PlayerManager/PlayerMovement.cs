using DG.Tweening;
using UnityEngine;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private Vector2 _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            HandleMovementAnimation();
        }

        private void HandleMovementAnimation()
        {
            var rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (rawInput.magnitude > 0f && !DOTween.IsTweening(transform))
                transform.DOScaleY(.9f, .1f).SetEase(Ease.InSine).OnComplete(() =>
                {
                    transform.DOScaleY(1f, .1f).SetEase(Ease.InSine);
                });
        }

        private void FixedUpdate()
        {
            _rb.velocity = _input * (moveSpeed * Time.deltaTime);
        }
    }
}