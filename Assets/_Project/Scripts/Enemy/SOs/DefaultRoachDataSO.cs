using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.Enemy.SOs
{
    [CreateAssetMenu(fileName = "DefaultRoachData", menuName = "ScriptableObjects/DefaultRoachData")]
    public class DefaultRoachDataSO : EnemyDataSO
    {
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Attack"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float FollowRadius { private set; get; } = 10f;

        [BoxGroup("Split/Right/Attack"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float AttackRadius { private set; get; } = 1f;

        [BoxGroup("Split/Right/Attack"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float AttackDelay { private set; get; } = 0.5f;

        [BoxGroup("Split/Right/Attack"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public int AttackDamage { private set; get; } = 2;
    }
}