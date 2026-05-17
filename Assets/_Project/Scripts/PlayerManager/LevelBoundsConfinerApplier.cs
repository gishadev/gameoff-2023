using Cinemachine;
using gameoff.World;
using UnityEngine;

namespace gameoff.PlayerManager
{
    [RequireComponent(typeof(CinemachineConfiner))]
    public class LevelBoundsConfinerApplier : MonoBehaviour
    {
        private CinemachineConfiner _confiner;

        private void Awake()
        {
            _confiner = GetComponent<CinemachineConfiner>();
            _confiner.m_BoundingShape2D = FindObjectOfType<Level>().LevelBounds.Collider;
        }
    }
}