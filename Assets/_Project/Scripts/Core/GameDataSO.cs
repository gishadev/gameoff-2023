using gameoff.PlayerManager;
using gishadev.tools.Events;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using UnityEngine;

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
        [SerializeField, ShowInInspector, OnCollectionChanged(after: nameof(OnPackChanged))]
        private UpgradesPack[] upgradesPack;

        public UpgradesPack[] UpgradesPack => upgradesPack;

        [Title("Upgrades Settings")]
        [TabGroup("Upgrades"), InlineEditor]
        [OdinSerialize, ShowInInspector]
        public DashAbilityDataSO DashAbilityDataSO { private set; get; }

        [TabGroup("Upgrades"), InlineEditor]
        [OdinSerialize, ShowInInspector]
        public ExplosionAbilityDataSO ExplosionAbilityDataSO { private set; get; }


        [TabGroup("UI")]
        [OdinSerialize, ShowInInspector, AssetsOnly]
        public GameObject UpgradeCardPrefab { private set; get; }

        [TabGroup("UI")]
        [field: SerializeField]
        public DefaultEventChannelSO UnlockEventChannel { private set; get; }

        public void OnPackChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType != CollectionChangeType.Add)
                return;

            Debug.Log("Received callback AFTER CHANGE with the following info: " + info +
                      ", and the following collection instance: " + value);

            var newPack = info.Value as UpgradesPack;
            if (newPack == null)
                return;
            
            if (upgradesPack.Length > 1)
                newPack.TargetLevelNumber = upgradesPack[^2].TargetLevelNumber + UpgradesStep;
            else
                newPack.TargetLevelNumber = UpgradesStep;
        }
    }
}