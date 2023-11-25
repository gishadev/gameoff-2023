using gameoff.Enemy.SOs;
using gishadev.tools.Effects;
using UnityEngine;

namespace gameoff.Enemy
{
    public class SpawnSpot : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO enemyToSpawn;


        private void Start()
        {
            OtherEmitter.I.EmitAt(enemyToSpawn.PoolEnumType, transform.position, Quaternion.identity)
                .GetComponent<Roach>();
        }
    }
}