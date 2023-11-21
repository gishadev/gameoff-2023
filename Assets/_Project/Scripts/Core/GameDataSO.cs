using gameoff.PlayerManager.SOs;
using gishadev.tools.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Core
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "ScriptableObjects/GameDataSO")]
    public class GameDataSO : ScriptableObject
    {
        [TabGroup("Abilities"), InlineEditor]
        [ShowInInspector] public DashAbilityDataSO DashAbilityDataSO { private set; get; }
        [TabGroup("Abilities"), InlineEditor]
        [ShowInInspector] public ExplosionAbilityDataSO ExplosionAbilityDataSO { private set; get; }

        [field: SerializeField] public DefaultEventChannelSO UnlockEventChannel { private set; get; }
    }
}