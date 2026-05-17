using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public abstract class UpgradeDataSO : SerializedScriptableObject
    {
        [OdinSerialize, ShowInInspector] public UpgradeEnumType UpgradeEnumType { private set; get; }

        [BoxGroup("Card Settings")]
        [OdinSerialize, ShowInInspector]
        public Sprite CardIconSprite { private set; get; }

        [BoxGroup("Card Settings")]
        [OdinSerialize, ShowInInspector]
        public string CardTitle { private set; get; }

        [BoxGroup("Card Settings")]
        [OdinSerialize, ShowInInspector, MultiLineProperty(10)]
        public string CardDescription { private set; get; }
    }
}