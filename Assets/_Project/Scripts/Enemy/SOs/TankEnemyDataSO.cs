using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.Enemy.SOs
{
    [CreateAssetMenu(fileName = "TankEnemyData", menuName = "ScriptableObjects/Enemies/TankEnemyData")]
    public class TankEnemyDataSO : EnemyDataSO
    {
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float ProjectileShieldRadius { private set; get; } = 5f;
    }
}