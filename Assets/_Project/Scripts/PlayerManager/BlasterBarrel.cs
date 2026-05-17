using gishadev.tools.Effects;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class BlasterBarrel : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        private ParticleSystem _shootingPS;
        private float _startEmission;

        private void Awake()
        {
            _shootingPS = GetComponentInChildren<ParticleSystem>(true);
            var emission = _shootingPS.emission;
            _startEmission = emission.rateOverTime.constant;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }

        public void StartShooting()
        {
            var emission = _shootingPS.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(_startEmission);
        }

        public void StopShooting()
        {
            var emission = _shootingPS.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }

        public BlasterProjectile CreateProjectile()
        {
            return OtherEmitter.I
                .EmitAt(OtherPoolEnum.BLASTER_PROJECTILE, shootPoint.position, shootPoint.rotation)
                .GetComponent<BlasterProjectile>();
        }
    }
}