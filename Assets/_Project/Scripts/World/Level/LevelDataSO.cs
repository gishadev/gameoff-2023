using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.World
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObject/LevelData")]
    public class LevelDataSO : ScriptableObject
    {
        [TabGroup("Main")]
        [field: SerializeField, ShowInInspector]
        private int levelOrder;

        public int LevelOrder => levelOrder;

        [TabGroup("Main"),
         ValidateInput(nameof(MustContainLevel), "Only for level prefab!")]
        [field: SerializeField, ShowInInspector, AssetsOnly]
        private GameObject levelPrefab;

        public GameObject LevelPrefab => levelPrefab;

        [TabGroup("UI")]
        [field: SerializeField, ShowInInspector, TextArea(2, 3)]
        private string levelName;

        public string LevelName => levelName;

        [TabGroup("UI")]
        [field: SerializeField, ShowInInspector, Range(1, 3)]
        private int difficulty;

        public int Difficulty => difficulty;

        [TabGroup("UI")]
        [Button("1")]
        private void Set1Difficulty() => difficulty = 1;

        [TabGroup("UI")]
        [Button("2")]
        private void Set2Difficulty() => difficulty = 2;

        [TabGroup("UI")]
        [Button("3")]
        private void Set3Difficulty() => difficulty = 3;

        private bool MustContainLevel(GameObject prefab) => prefab.TryGetComponent(out Level _);

        public void SetLevelIndex(int index) => levelOrder = index;
    }
}