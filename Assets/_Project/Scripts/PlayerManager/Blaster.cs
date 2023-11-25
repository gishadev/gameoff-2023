using System;
using Cysharp.Threading.Tasks;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;

        [SerializeField] private int damage = 1;
        [SerializeField] private float shootingDelay = 0.1f;
        [SerializeField] private float reloadingDelay = 1f;
        [field: SerializeField] public int MaxAmmo { get; private set; } = 100;

        [Inject] private DiContainer _diContainer;

        public event Action<int> AmmoChanged;
        public int CurrentAmmo { get; private set; }

        private ParticleSystem _shootingPS;
        private float _startEmission;
        private bool _isShooting, _isReloading, _isShootingDelay;

        private void Awake()
        {
            CurrentAmmo = MaxAmmo;
            _shootingPS = GetComponentInChildren<ParticleSystem>(true);

            var emission = _shootingPS.emission;
            _startEmission = emission.rateOverTime.constant;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
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
            var emission = _shootingPS.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(_startEmission);
        }

        public void StopShooting()
        {
            _isShooting = false;

            var emission = _shootingPS.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }

        public void StartReloading()
        {
            ReloadingAsync();
        }

        private async void ShootingAsync()
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
            await UniTask.WaitForSeconds(reloadingDelay);
            CurrentAmmo = MaxAmmo;
            _isReloading = false;

            AmmoChanged?.Invoke(CurrentAmmo);
        }

        private void ShootProjectile()
        {
            var projectile = OtherEmitter.I
                .EmitAt(OtherPoolEnum.BLASTER_PROJECTILE, shootPoint.position, shootPoint.rotation)
                .GetComponent<BlasterProjectile>();
            projectile.SetDamage(damage);

            CurrentAmmo--;
            AmmoChanged?.Invoke(CurrentAmmo);

            _diContainer.InjectGameObject(projectile.gameObject);
        }
    }
}