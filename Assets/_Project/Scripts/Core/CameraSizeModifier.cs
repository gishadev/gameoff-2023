using Cinemachine;
using gameoff.PlayerManager;
using UnityEngine;

namespace gameoff.Core
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraSizeModifier : MonoBehaviour
    {
        [SerializeField] private float minSize = 7f;          
        [SerializeField] private float maxSize = 8.5f;         
        [SerializeField] private float maxVelocityMagnitude = 5f;          

        private CinemachineVirtualCamera _cam;
        private Rigidbody2D _targetRigidbody;

        private void Awake()
        {
            _cam = GetComponent<CinemachineVirtualCamera>();
            _targetRigidbody = FindObjectOfType<Player>().GetComponentInChildren<Rigidbody2D>();
        }

        private void Update()
        {
            if (_targetRigidbody == null || _cam == null)
                return;

            float speed = _targetRigidbody.velocity.magnitude;

            float newSize = Mathf.Lerp(minSize, maxSize, speed / maxVelocityMagnitude);
            _cam.m_Lens.OrthographicSize = Mathf.Lerp(_cam.m_Lens.OrthographicSize, newSize, Time.deltaTime);
        }
    }
}