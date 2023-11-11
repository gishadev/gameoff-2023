using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Player))]
    public class PlayerWeaponsController : MonoBehaviour
    {
        [SerializeField] private Blaster blaster;
        [SerializeField] private float specialAttackDelay = 1f;

        private IAbility _ability;

        private CustomInput _input;
        private Camera _cam;

        private bool _isUsingMouse;
        private bool _isSpecialAttackDelay;

        private void Awake()
        {
            _ability = new ExplosionAbility(GetComponent<Player>());

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
            _input.Player.SpecialAttack.performed += OnSpecialAttackPerformed;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.WeaponRotationGamepad.performed -= OnWeaponRotationGamepadPerformed;
            _input.Player.WeaponRotationMouse.performed -= OnWeaponRotationMousePerformed;

            _input.Player.PrimaryAttack.performed -= OnPrimaryAttackPerformed;
            _input.Player.PrimaryAttack.canceled -= OnPrimaryAttackCanceled;
            _input.Player.SpecialAttack.performed -= OnSpecialAttackPerformed;
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
            blaster.RotateBlaster(direction);
        }

        private void OnPrimaryAttackPerformed(InputAction.CallbackContext value)
        {
            blaster.StartShooting();
        }

        private void OnPrimaryAttackCanceled(InputAction.CallbackContext value)
        {
            blaster.StopShooting();
        }

        private async void OnSpecialAttackPerformed(InputAction.CallbackContext value)
        {
            if (_isSpecialAttackDelay)
                return;

            _ability.Trigger();
            _isSpecialAttackDelay = true;
            await UniTask.WaitForSeconds(specialAttackDelay);
            _isSpecialAttackDelay = false;
        }

        #endregion
    }
}