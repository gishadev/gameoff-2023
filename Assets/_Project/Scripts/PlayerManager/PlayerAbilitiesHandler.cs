﻿using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using gameoff.Core;
using gishadev.tools.Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(Player), typeof(PlayerMovement))]
    public class PlayerAbilitiesHandler : MonoBehaviour
    {
        private IAbility _specialAbility, _movementAbility;

        [Inject] private DiContainer _diContainer;

        [Inject] private IPlayerUpgradesController _playerUpgradesController;

        private CustomInput _input;
        private bool _isSpecialDelay, _isMovementDelay;

        private void Awake()
        {
            _input = new CustomInput();
            _input.Enable();
        }

        private void Start()
        {
            if (_playerUpgradesController.IsUnlocked(UpgradeEnumType.ABILITY_EXPLOSION))
                _specialAbility = new ExplosionAbility(GetComponent<Player>(), _diContainer);
            if (_playerUpgradesController.IsUnlocked(UpgradeEnumType.ABILITY_DASH))
                _movementAbility = new DashAbility(GetComponent<PlayerMovement>(), _diContainer);

            if (_specialAbility != null)
            {
                _input.Player.SpecialAttack.performed += OnSpecialAbilityPerformed;
                _input.Player.SpecialAttack.canceled += OnSpecialAbilityCanceled;
            }

            if (_movementAbility != null)
            {
                _input.Player.Dash.performed += OnMovementAbilityPerformed;
                _input.Player.Dash.canceled += OnMovementAbilityCanceled;
            }
        }

        private void OnDestroy()
        {
            _input.Disable();
            if (_specialAbility != null)
            {
                _input.Player.SpecialAttack.performed -= OnSpecialAbilityPerformed;
                _input.Player.SpecialAttack.canceled -= OnSpecialAbilityCanceled;
            }

            if (_movementAbility != null)
            {
                _input.Player.Dash.performed -= OnMovementAbilityPerformed;
                _input.Player.Dash.canceled -= OnMovementAbilityCanceled;
            }
        }

        private async void OnSpecialAbilityPerformed(InputAction.CallbackContext value)
        {
            if (_specialAbility == null || _isSpecialDelay)
            {
                AudioManager.I.PlayAudio(SFXAudioEnum.DENIED);
                return;
            }

            _specialAbility.Trigger();
            IAbility.RaiseUsed(_specialAbility);

            _isSpecialDelay = true;
            await UniTask.WaitForSeconds(_specialAbility.AbilityDataSO.AbilityCooldown);
            
            AudioManager.I.PlayAudio(SFXAudioEnum.ABILITY_RELOADED);
            _isSpecialDelay = false;
        }

        private async void OnSpecialAbilityCanceled(InputAction.CallbackContext obj)
        {
            if (_specialAbility == null || !_specialAbility.IsUsing)
                return;

            _specialAbility.Cancel();
            _isSpecialDelay = true;
            await UniTask.WaitForSeconds(_specialAbility.AbilityDataSO.AbilityCooldown);
            
            AudioManager.I.PlayAudio(SFXAudioEnum.ABILITY_RELOADED);
            _isSpecialDelay = false;
        }

        private async void OnMovementAbilityPerformed(InputAction.CallbackContext value)
        {
            if (_movementAbility == null || _isMovementDelay)
            {
                AudioManager.I.PlayAudio(SFXAudioEnum.DENIED);
                return;
            }

            _movementAbility.Trigger();
            IAbility.RaiseUsed(_movementAbility);

            _isMovementDelay = true;
            await UniTask.WaitForSeconds(_movementAbility.AbilityDataSO.AbilityCooldown);
            _isMovementDelay = false;
        }

        private async void OnMovementAbilityCanceled(InputAction.CallbackContext obj)
        {
            if (_movementAbility == null || !_movementAbility.IsUsing)
                return;

            _movementAbility.Cancel();
            _isMovementDelay = true;
            await UniTask.WaitForSeconds(_movementAbility.AbilityDataSO.AbilityCooldown);
            _isMovementDelay = false;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (_movementAbility is DashAbility dashAbility && dashAbility.IsUsing)
            {
                var knockBack = ((DashAbilityDataSO) dashAbility.AbilityDataSO).KnockBackRadius;
                var damageables = Physics2D.OverlapCircleAll(transform.position, knockBack)
                    .Where(x => x.TryGetComponent(out IDamageableWithPhysicsImpact _))
                    .Select(x => x.GetComponent<IDamageableWithPhysicsImpact>());

                foreach (var damageable in damageables)
                {
                    if (damageable.gameObject != gameObject)
                        damageable.PhysicsImpactEffector.Act(transform.position, 20f);
                }
            }
        }
    }
}