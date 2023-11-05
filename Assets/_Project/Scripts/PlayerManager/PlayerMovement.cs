using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private CustomInput _input;
        private Vector2 _moveInputVector;

        private void Awake()
        {
            _input = new CustomInput();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Movement.performed += OnMovementPerformed;
            _input.Player.Movement.canceled += OnMovementCanceled;
        }


        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Movement.performed -= OnMovementPerformed;
            _input.Player.Movement.canceled -= OnMovementCanceled;
        }

        private void Update()
        {
            HandleMovementAnimation();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _moveInputVector * (moveSpeed * Time.deltaTime);
        }

        private void HandleMovementAnimation()
        {
            if (_moveInputVector.magnitude > 0f && !DOTween.IsTweening(transform))
                transform.DOScaleY(.9f, .1f).SetEase(Ease.InSine).OnComplete(() =>
                {
                    transform.DOScaleY(1f, .1f).SetEase(Ease.InSine);
                });
        }
        
        private void OnMovementPerformed(InputAction.CallbackContext value)
        {
            _moveInputVector = value.ReadValue<Vector2>();
        }

        private void OnMovementCanceled(InputAction.CallbackContext value)
        {
            _moveInputVector = Vector2.zero;
        }
    }
}