using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.Enemy.SOs
{
    [CreateAssetMenu(fileName = "DefaultRoachData", menuName = "ScriptableObjects/DefaultRoachData")]
    public class DefaultRoachDataSO : EnemyDataSO
    {
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float FollowRadius { private set; get; } = 10f;
    }
}