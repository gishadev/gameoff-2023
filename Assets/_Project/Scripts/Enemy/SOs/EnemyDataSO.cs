using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy.SOs
{
    public abstract class EnemyDataSO : ScriptableObject
    {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        
        [BoxGroup("Split/Left/General")]
        [ShowInInspector, GUIColor("green")]
        public int StartHealth { private set; get; } = 2;

        [BoxGroup("Split/Left/General")]
        [ShowInInspector]
        public float MoveSpeed { private set; get; } = 100f;
    }
}