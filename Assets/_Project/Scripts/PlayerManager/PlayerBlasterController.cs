using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Player))]
    public class PlayerBlasterController : MonoBehaviour
    {
        [SerializeField, InlineEditor] private Blaster blaster;

        private IAbility _ability;

        private CustomInput _input;
        private Camera _cam;
        private Player _player;

        private bool _isUsingMouse;
        private bool _isSpecialAttackDelay;

        private void Awake()
        {
            _player = GetComponent<Player>();

            _cam = Camera.main;
            _input = new CustomInput();
        }


        private void OnEnable()
        {
            _input.Enable();

            _input.Player.WeaponRotationGamepad.performed += OnWeaponRotationGamepadPerformed;
            _input.Player.WeaponRotationMouse.performed += OnWeaponRotationMousePerformed;

            _input.Player.PrimaryAttack.performed += OnPrimaryAttackPerformed;
            _input.Player.PrimaryAttack.canceled += OnPrimaryAttackCanceled;
            
            _input.Player.Reload.performed += OnReloadPerformed;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.WeaponRotationGamepad.performed -= OnWeaponRotationGamepadPerformed;
            _input.Player.WeaponRotationMouse.performed -= OnWeaponRotationMousePerformed;

            _input.Player.PrimaryAttack.performed -= OnPrimaryAttackPerformed;
            _input.Player.PrimaryAttack.canceled -= OnPrimaryAttackCanceled;
            
            _input.Player.Reload.performed -= OnReloadPerformed;
        }

        private void Update()
        {
            if (_isUsingMouse)
                HandleWeaponRotationMouse(Mouse.current.position.ReadValue());
        }

        #region Input Actions

        private void HandleWeaponRotationMouse(Vector2 position)
        {
            Vector3 worldPos = _cam.ScreenToWorldPoint(position);
            var direction = worldPos - transform.position;

            _player.SpriteRenderer.flipX = direction.x < 0;
            blaster.RotateBlaster(direction);
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
            
            _player.SpriteRenderer.flipX = direction.x < 0;
            blaster.RotateBlaster(direction);
        }

        private void OnPrimaryAttackPerformed(InputAction.CallbackContext value) => blaster.StartShooting();
        private void OnPrimaryAttackCanceled(InputAction.CallbackContext value) => blaster.StopShooting();
        private void OnReloadPerformed(InputAction.CallbackContext value) => blaster.StartReloading();

        #endregion
    }
}