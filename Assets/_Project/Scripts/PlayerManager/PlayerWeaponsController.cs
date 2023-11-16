using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Player))]
    public class PlayerWeaponsController : MonoBehaviour
    {
        [SerializeField] private Blaster blaster;
        [SerializeField] private SpecialAbilitySettings specialAbilitySettings;

        [Inject] private DiContainer _diContainer;

        private IAbility _ability;

        private CustomInput _input;
        private Camera _cam;
        private Player _player;

        private bool _isUsingMouse;
        private bool _isSpecialAttackDelay;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _ability = new ExplosionAbility(GetComponent<Player>(), _diContainer, specialAbilitySettings);

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
            await UniTask.WaitForSeconds(specialAbilitySettings.AbilityDelay);
            _isSpecialAttackDelay = false;
        }

        #endregion
    }

    [Serializable]
    public class SpecialAbilitySettings
    {
        [field: SerializeField] public float AbilityDelay { private set; get; } = 1f;
        [field: SerializeField] public int ProjectileDamage { private set; get; } = 5;
        [field: SerializeField] public int ProjectileCount { private set; get; } = 10;

        [field: SerializeField] public int ClearIterations { private set; get; } = 10;
        [field: SerializeField] public float ClearMaxRadius { private set; get; } = 5f;
        [field: SerializeField] public float FullClearExpandingTime { private set; get; } = 1f;
    }
}