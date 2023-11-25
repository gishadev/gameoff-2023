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
        [OdinSerialize, ShowInInspector, GUIColor("green")]
        public int StartHealth { private set; get; } = 2;

        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public float MoveSpeed { private set; get; } = 100f;

        private bool CanOnlyBeEnemyEnum(OtherPoolEnum poolEnum, ref string errorMessage)
        {
            if (poolEnum.ToString().Contains("ENEMY"))
                return true;
            return false;
        }
    }
}