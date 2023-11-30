using DG.Tweening;
using gameoff.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TrailRenderer))]
    public class PlayerMovement : MonoBehaviourWithMovementEffector
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private ParticleSystem movementVFX;

        public Rigidbody2D Rigidbody { get; private set; }
        public TrailRenderer TrailRenderer { get; private set; }

        private Animator _animator;

        private CustomInput _input;
        public Vector2 MoveInputVector { get; private set; }

        private static readonly int IsRunningID = Animator.StringToHash("IsRunning");

        private void Awake()
        {
            _input = new CustomInput();
            Rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
            TrailRenderer = GetComponent<TrailRenderer>();
            TrailRenderer.emitting = false;
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

        private void Update() => HandleMovementAnimation();
        private void FixedUpdate() => HandleBasicMovement();

        private void HandleBasicMovement()
        {
            if (IsDefaultMovementEnabled)
                Rigidbody.velocity = MoveInputVector * (moveSpeed * Time.deltaTime);
        }

        private void HandleMovementAnimation()
        {
            if (MoveInputVector.magnitude > 0f && !DOTween.IsTweening(transform))
                transform.DOScaleY(.9f, .1f).SetEase(Ease.InSine).OnComplete(() =>
                {
                    transform.DOScaleY(1f, .1f).SetEase(Ease.InSine);
                });
        }

        private void OnMovementPerformed(InputAction.CallbackContext value)
        {
            MoveInputVector = value.ReadValue<Vector2>();
            _animator.SetBool(IsRunningID, true);
            movementVFX.Play();
        }

        private void OnMovementCanceled(InputAction.CallbackContext value)
        {
            MoveInputVector = Vector2.zero;
            _animator.SetBool(IsRunningID, false);
            movementVFX.Stop();
        }
    }
}