using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    public class BlasterController : MonoBehaviour
    {
        private CustomInput _input;
        private ParticleSystem _shootingPS;

        private Camera _cam;
        private bool _isUsingMouse;
        private readonly float _startEmission = 500f;

        private void Awake()
        {
            _cam = Camera.main;
            _input = new CustomInput();
            
            _shootingPS = GetComponentInChildren<ParticleSystem>(true);
            var emission = _shootingPS.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.WeaponRotationGamepad.performed += OnWeaponRotationGamepadPerformed;
            _input.Player.WeaponRotationMouse.performed += OnWeaponRotationMousePerformed;

            _input.Player.PrimaryAttack.performed += OnPrimaryAttackPerformed;
            _input.Player.PrimaryAttack.canceled += OnPrimaryAttackCanceled;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.WeaponRotationGamepad.performed -= OnWeaponRotationGamepadPerformed;
            _input.Player.WeaponRotationMouse.performed -= OnWeaponRotationMousePerformed;

            _input.Player.PrimaryAttack.performed -= OnPrimaryAttackPerformed;
            _input.Player.PrimaryAttack.canceled -= OnPrimaryAttackCanceled;
        }

        private void Update()
        {
            if (_isUsingMouse)
                HandleWeaponRotationMouse(Mouse.current.position.ReadValue());
        }

        private void HandleWeaponRotationMouse(Vector2 position)
        {
            Vector3 worldPos = _cam.ScreenToWorldPoint(position);
            var direction = worldPos - transform.position;
            RotateBlaster(direction);
        }

        private void OnWeaponRotationMousePerformed(InputAction.CallbackContext value)
        {
            _isUsingMouse = true;
            Cursor.visible = true;
        }

        private void OnWeaponRotationGamepadPerformed(InputAction.CallbackContext value)
        {
            _isUsingMouse = false;
            Cursor.visible = false;

            var direction = value.ReadValue<Vector2>();
            RotateBlaster(direction);
        }

        private void RotateBlaster(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);

            transform.rotation = rotation;
            // transform.DORotateQuaternion(rotation, blasterRotateDuration);
        }

        private void OnPrimaryAttackPerformed(InputAction.CallbackContext value)
        {
            var emission = _shootingPS.emission; 
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(_startEmission);
        }

        private void OnPrimaryAttackCanceled(InputAction.CallbackContext value)
        {
            var emission = _shootingPS.emission; 
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }
    }
}