using Cysharp.Threading.Tasks;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform shootPoint;

        [SerializeField] private int damage = 1;
        [SerializeField] private float shootingDelay = 0.1f;

        private ParticleSystem _shootingPS;
        private float _startEmission;
        private bool _isShooting;

        private void Awake()
        {
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

        private async void ShootingAsync()
        {
            while (_isShooting)
            {
                ShootProjectile();
                await UniTask.WaitForSeconds(shootingDelay);
            }
        }

        // TODO: pooling needed.
        private void ShootProjectile()
        {
            var projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation)
                .GetComponent<BlasterProjectile>();
            projectile.SetDamage(damage);
        }
    }
}