using UnityEngine;

namespace gameoff.PlayerManager
{
    public class Blaster
    {
        private readonly GameObject _blasterObj;
        private ParticleSystem _shootingPS;

        private readonly float _startEmission = 500f;

        public Blaster(GameObject blasterObj)
        {
            _blasterObj = blasterObj;
            _shootingPS = blasterObj.GetComponentInChildren<ParticleSystem>(true);
            var emission = _shootingPS.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }
        
        public void RotateBlaster(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);

            _blasterObj.transform.rotation = rotation;
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
    }
}