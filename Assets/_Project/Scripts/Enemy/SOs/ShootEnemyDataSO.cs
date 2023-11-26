using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.Enemy.SOs
{
    [CreateAssetMenu(fileName = "ShootEnemyData", menuName = "ScriptableObjects/Enemies/ShootEnemyData")]
    public class ShootEnemyDataSO : EnemyDataSO
    {
        [Title("Distances")]
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [InfoBox("If distance to player is less than ShootMinRadius - move away to handle PreferableDistanceToPlayer")]
        [OdinSerialize, ShowInInspector]
        public float ShootMinRadius { private set; get; } = 2f;

        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float ShootMaxRadius { private set; get; } = 6f;

        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("green")]
        [OdinSerialize, ShowInInspector]
        public float PreferableDistanceToPlayer { private set; get; } = 5f;

        [Title("Shooting")]
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float ShootDelay { private set; get; } = 1f;

        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public int ShootProjectileDamage { private set; get; } = 5;
    }
}