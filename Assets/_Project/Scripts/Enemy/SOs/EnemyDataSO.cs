using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace gameoff.Enemy.SOs
{
    public abstract class EnemyDataSO : SerializedScriptableObject
    {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [BoxGroup("Split/Left/General")]
        [ValidateInput(nameof(CanOnlyBeEnemyEnum), "This spawn spot is for ENEMY only")]
        [OdinSerialize, ShowInInspector, InfoBox("Select only ENEMY enum!")]
        public OtherPoolEnum PoolEnumType { private set; get; }

        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public bool IsBoss { private set; get; }
        
        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector, GUIColor("green")]
        public int StartHealth { private set; get; } = 2;

        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public float MoveSpeed { private set; get; } = 100f;
        
        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public float StartAreaRadius { private set; get; } = 1.5f;
        
        [BoxGroup("Split/Left/General"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float MeleeAttackRadius { private set; get; } = 1f;

        [BoxGroup("Split/Left/General"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float MeleeAttackDelay { private set; get; } = 0.5f;

        [BoxGroup("Split/Left/General"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public int MeleeAttackDamage { private set; get; } = 2;
        
        [BoxGroup("Split/Left/General"), GUIColor("blue")]
        [OdinSerialize, ShowInInspector]
        public float FollowRadius { private set; get; } = 10f;

        private bool CanOnlyBeEnemyEnum(OtherPoolEnum poolEnum, ref string errorMessage)
        {
            if (poolEnum.ToString().Contains("ENEMY"))
                return true;
            return false;
        }
    }
}