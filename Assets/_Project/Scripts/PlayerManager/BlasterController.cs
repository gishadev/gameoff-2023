using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    public class BlasterController : MonoBehaviour
    {
        [SerializeField] private float blasterRotateDuration = .1f;


        private CustomInput _input;

        private void Awake()
        {
            _input = new CustomInput();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.WeaponRotationMouse.performed += OnWeaponRotationMousePerformed;
            _input.Player.WeaponRotationGamepad.performed += OnWeaponRotationGamepadPerformed;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.WeaponRotationMouse.performed -= OnWeaponRotationMousePerformed;
            _input.Player.WeaponRotationGamepad.performed -= OnWeaponRotationGamepadPerformed;
        }


        private void OnWeaponRotationMousePerformed(InputAction.CallbackContext value)
        {
            var position = value.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
            var direction = worldPos - transform.position;
            RotateBlaster(direction);
        }

        private void OnWeaponRotationGamepadPerformed(InputAction.CallbackContext value)
        {
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
    }
}