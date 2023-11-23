using gameoff.PlayerManager;
using gishadev.tools.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace gameoff.Core
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "ScriptableObjects/GameDataSO")]
    public class GameDataSO : SerializedScriptableObject
    {
        [TabGroup("Abilities"), InlineEditor]
        [OdinSerialize, ShowInInspector] public DashAbilityDataSO DashAbilityDataSO { private set; get; }
        [TabGroup("Abilities"), InlineEditor]
        [OdinSerialize, ShowInInspector] public ExplosionAbilityDataSO ExplosionAbilityDataSO { private set; get; }

        [TabGroup("UI")]
        [OdinSerialize, ShowInInspector, AssetsOnly] public GameObject UpgradeCardPrefab  { private set; get; }
        
        [field: SerializeField] public DefaultEventChannelSO UnlockEventChannel { private set; get; }
    }
}