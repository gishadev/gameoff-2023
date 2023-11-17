using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TrailRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        [Header("Dashing")] [SerializeField] private float dashingPower = 24f;
        [SerializeField] private float dashingTime = 0.2f;
        [SerializeField] private float dashingCooldown = 1f;

        private bool _canDash = true;
        private bool _isDashing;

        private Rigidbody2D _rb;
        private TrailRenderer _trailRenderer;
        private Animator _animator;

        private CustomInput _input;
        private Vector2 _moveInputVector;

        private CancellationTokenSource _dashingCTS;
        private static readonly int IsRunningID = Animator.StringToHash("IsRunning");

        private void Awake()
        {
            _input = new CustomInput();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
            _trailRenderer = GetComponent<TrailRenderer>();
            _trailRenderer.emitting = false;
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Movement.performed += OnMovementPerformed;
            _input.Player.Movement.canceled += OnMovementCanceled;
            _input.Player.Dash.performed += OnDashPerformed;
            _input.Player.Dash.canceled += OnDashCanceled;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Movement.performed -= OnMovementPerformed;
            _input.Player.Movement.canceled -= OnMovementCanceled;
            _input.Player.Dash.performed -= OnDashPerformed;
            _input.Player.Dash.performed -= OnDashPerformed;
            _input.Player.Dash.canceled -= OnDashCanceled;
        }

        private void Update() => HandleMovementAnimation();
        private void FixedUpdate() => HandleBasicMovement();

        private void HandleBasicMovement()
        {
            if (!_isDashing)
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
            _animator.SetBool(IsRunningID, true);
        }

        private void OnMovementCanceled(InputAction.CallbackContext value)
        {
            _moveInputVector = Vector2.zero;
            _animator.SetBool(IsRunningID, false);
        }

        private async void OnDashPerformed(InputAction.CallbackContext value)
        {
            if (!_canDash)
                return;
            
            _canDash = false;
            _isDashing = true;

            _dashingCTS = new CancellationTokenSource();
            _rb.AddForce(_moveInputVector * dashingPower, ForceMode2D.Impulse);
            _trailRenderer.emitting = true;
            await UniTask.WaitForSeconds(dashingTime, cancellationToken: _dashingCTS.Token)
                .SuppressCancellationThrow();

            _trailRenderer.emitting = false;
            _isDashing = false;

            await UniTask.WaitForSeconds(dashingCooldown);
            _canDash = true;
        }

        private async void OnDashCanceled(InputAction.CallbackContext value)
        {
            if (_isDashing)
            {
                _dashingCTS?.Cancel();
                _trailRenderer.emitting = false;
                _isDashing = false;

                await UniTask.WaitForSeconds(dashingCooldown);
                _canDash = true;
            }
        }
    }
}