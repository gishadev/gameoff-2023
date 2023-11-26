using System.Linq;
using gameoff.PlayerManager;
using gameoff.World;
using gishadev.tools.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace gameoff.Core
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "ScriptableObjects/GameDataSO")]
    public class GameDataSO : SerializedScriptableObject
    {
        [Title("Upgrades giving rules")]
        [TabGroup("Upgrades"), InfoBox("Winning every [x] level will give to player an upgrade to choose")]
        [OdinSerialize, ShowInInspector, PropertyOrder(-1)]
        public int UpgradesStep { private set; get; }

        [TabGroup("Upgrades")]
        [SerializeField, ShowInInspector]
#if UNITY_EDITOR
        [OnCollectionChanged(after: nameof(OnPackChanged))]
#endif
        private UpgradesPack[] upgradesPack;

        public UpgradesPack[] UpgradesPack => upgradesPack;

        [Title("Upgrades Settings")]
        [TabGroup("Upgrades"), InlineEditor]
        [OdinSerialize, ShowInInspector]
        public DashAbilityDataSO DashAbilityDataSO { private set; get; }

        [TabGroup("Upgrades"), InlineEditor]
        [OdinSerialize, ShowInInspector]
        public ExplosionAbilityDataSO ExplosionAbilityDataSO { private set; get; }

        [TabGroup("Levels"), InfoBox("Make sure that every prefab is only level prefab."),
         ValidateInput(nameof(MustContainLevel), "Only for levels!")]
        [field: SerializeField, ShowInInspector, AssetsOnly]
        private GameObject[] levelPrefabs;

        public GameObject[] LevelPrefabs => levelPrefabs;

        [TabGroup("Levels"), ValidateInput(nameof(MustContainPlayer), "Must be player prefab!")]
        [field: SerializeField, ShowInInspector, AssetsOnly]
        private GameObject playerPrefab;

        public GameObject PlayerPrefab => playerPrefab;


        [TabGroup("UI")]
        [OdinSerialize, ShowInInspector, AssetsOnly]
        public GameObject UpgradeCardPrefab { private set; get; }

        [TabGroup("UI")]
        [field: SerializeField]
        public DefaultEventChannelSO UnlockEventChannel { private set; get; }


#if UNITY_EDITOR
        public void OnPackChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType != CollectionChangeType.Add)
                return;

            Debug.Log("Received callback AFTER CHANGE with the following info: " + info +
                      ", and the following collection instance: " + value);

            if (info.Value is not UpgradesPack newPack)
                return;

            if (upgradesPack.Length > 1)
                newPack.TargetLevelNumber = upgradesPack[^2].TargetLevelNumber + UpgradesStep;
            else
                newPack.TargetLevelNumber = UpgradesStep;
        }
#endif

        private bool MustContainLevel(GameObject[] prefabs)
            => prefabs.All(x => x.GetComponentInChildren<Level>() != null);

        private bool MustContainPlayer(GameObject prefab)
            => prefab.GetComponentInChildren<Player>() != null;
    }
}