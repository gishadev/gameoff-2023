using gishadev.tools.Effects;
using UnityEngine;

namespace gameoff.Enemy
{
    public class SpawnSpot : MonoBehaviour
    {
        private void Start()
        {
            OtherEmitter.I.EmitAt(OtherPoolEnum.ROACH, transform.position, Quaternion.identity)
                .GetComponent<Roach>();
        }
    }
}