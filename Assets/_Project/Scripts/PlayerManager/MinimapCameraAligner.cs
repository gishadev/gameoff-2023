using gameoff.World;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class MinimapCameraAligner : MonoBehaviour
    {
        private void Awake()
        {
            var creep = FindObjectOfType<Level>().Creep;
            var targetPosition =
                new Vector3(creep.transform.position.x, creep.transform.position.y, transform.position.z);
            transform.position = targetPosition;
        }
    }
}