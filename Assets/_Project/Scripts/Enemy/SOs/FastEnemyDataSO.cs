using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.Enemy.SOs
{
    [CreateAssetMenu(fileName = "FastEnemyData", menuName = "ScriptableObjects/FastEnemyData")]
    public class FastEnemyDataSO : EnemyDataSO
    {
        [Title("Flee")]
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public int MinDamageToFlee { private set; get; } = 1;

        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float FleeMoveSpeedMultiplier { private set; get; } = 1.2f;
        
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float FleeTime { private set; get; } = 2f;
    }
}