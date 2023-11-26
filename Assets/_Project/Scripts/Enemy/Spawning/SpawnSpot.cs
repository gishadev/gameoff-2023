using gameoff.Enemy.SOs;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;

namespace gameoff.Enemy
{
    public class SpawnSpot : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO enemyToSpawn;
        [Inject] private DiContainer _diContainer;

        private void Start()
        {
            var enemy = OtherEmitter.I.EmitAt(enemyToSpawn.PoolEnumType, transform.position, Quaternion.identity)
                .GetComponent<Enemy>();
            _diContainer.Inject(enemy);
        }
    }
}