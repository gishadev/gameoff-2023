using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using gishadev.tools.Audio;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private BlasterBarrel mainBarrel;
        [SerializeField] private BlasterBarrel[] additionalBarrels;


        [SerializeField] private int damage = 1;
        [SerializeField] private float shootingDelay = 0.1f;
        [SerializeField] private float reloadingDelay = 1f;
        [field: SerializeField] public int MaxAmmo { get; private set; } = 100;

        [Inject] private DiContainer _diContainer;
        [Inject] private IPlayerUpgradesController _upgradesController;

        public event Action<int> AmmoChanged;
        public event Action ReloadingStarted;
        public int CurrentAmmo { get; private set; }

        public float ReloadingDelay => reloadingDelay;

        private bool _isShooting, _isReloading, _isShootingDelay;
        private bool _isFullAuto;
        private List<BlasterBarrel> _blasterBarrels = new();

        private void Start()
        {
            _blasterBarrels.Add(mainBarrel);
            CurrentAmmo = MaxAmmo;
            
            _isFullAuto = _upgradesController.IsUnlocked(UpgradeEnumType.EXTENSION_BLASTER_AUTOMATIC);
            if (_upgradesController.IsUnlocked(UpgradeEnumType.EXTENSION_BLASTER_MULTISHOOT))
                _blasterBarrels.AddRange(additionalBarrels);
        }

        public void RotateBlaster(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);

            transform.rotation = rotation;
        }

        public void StartShooting()
        {
            if (CurrentAmmo <= 0 || _isReloading || _isShootingDelay)
                return;

            _isShooting = true;

            ShootingAsync();

            foreach (var barrel in _blasterBarrels) barrel.StartShooting();
        }

        public void StopShooting()
        {
            _isShooting = false;
            foreach (var barrel in _blasterBarrels) barrel.StopShooting();
        }

        public void StartReloading()
        {
            if (CurrentAmmo == MaxAmmo || _isReloading)
                return;
            
            ReloadingAsync();
            ReloadingStarted?.Invoke();
        }

        private async void ShootingAsync()
        {
            if (_isFullAuto)
                await FullAutoShoot();
            else
                await SemiAutoShoot();
        }

        private async Task SemiAutoShoot()
        {
            if (_isShooting && !_isReloading)
            {
                _isShootingDelay = true;
                if (CurrentAmmo <= 0)
                {
                    StopShooting();
                    StartReloading();
                    return;
                }

                ShootProjectile();
                await UniTask.WaitForSeconds(shootingDelay);
            }

            _isShootingDelay = false;
            StopShooting();
        }

        private async Task FullAutoShoot()
        {
            while (_isShooting && !_isReloading)
            {
                _isShootingDelay = true;
                if (CurrentAmmo <= 0)
                {
                    StopShooting();
                    StartReloading();
                    break;
                }

                ShootProjectile();
                await UniTask.WaitForSeconds(shootingDelay);
                _isShootingDelay = false;
            }

            _isShootingDelay = false;

            StopShooting();
        }

        private async void ReloadingAsync()
        {
            _isReloading = true;
            
            AudioManager.I.PlayAudio(SFXAudioEnum.BLASTER_RELOAD);
            await UniTask.WaitForSeconds(ReloadingDelay);
            CurrentAmmo = MaxAmmo;
            _isReloading = false;

            AmmoChanged?.Invoke(CurrentAmmo);
            
            AudioManager.I.PlayAudio(SFXAudioEnum.BLASTER_RELOADED);
        }

        private void ShootProjectile()
        {
            foreach (var barrel in _blasterBarrels)
            {
                var projectile = barrel.CreateProjectile();
                projectile.SetDamage(damage);
                _diContainer.InjectGameObject(projectile.gameObject);
            }

            CurrentAmmo--;
            AmmoChanged?.Invoke(CurrentAmmo);
            
            AudioManager.I.PlayAudio(SFXAudioEnum.BLASTER_SHOOT);
        }
    }
}