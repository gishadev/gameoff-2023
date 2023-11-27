using gameoff.PlayerManager;
using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.Core
{
    public class LevelLoader
    {
        private readonly DiContainer _diContainer;
        private readonly Transform _sceneParent;
        private readonly GameDataSO _gameDataSO;

        public LevelLoader(DiContainer diContainer, Transform sceneParent)
        {
            _diContainer = diContainer;
            _sceneParent = sceneParent;
            _gameDataSO = _diContainer.Resolve<GameDataSO>();
        }

        public void LoadLevel(int index)
        {
            var levelIndex = index;
            if (levelIndex > _gameDataSO.Levels.Length - 1)
            {
                levelIndex = _gameDataSO.Levels.Length - 1;
                Debug.Log("Out of levels. Loading last one.");
            }

            var levelPrefab = _gameDataSO.Levels[levelIndex].LevelPrefab;
            var level = _diContainer.InstantiatePrefab(levelPrefab, _sceneParent).GetComponentInChildren<Level>();

            SpawnPlayer(level);
        }

        private void SpawnPlayer(Level level)
        {
            var player = _diContainer.InstantiatePrefab(_gameDataSO.PlayerPrefab, _sceneParent)
                .GetComponentInChildren<Player>();
            player.transform.position = level.HumanBase.transform.position;
        }
    }
}