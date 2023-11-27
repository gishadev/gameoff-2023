using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace gameoff.World
{
    public class LevelConverter : MonoBehaviour
    {
        [SerializeField] private GameObject spawnSpotPrefab;
        [SerializeField] private Transform spawnSpotsParent;
        
        [Button("Enemies into spawn spots")]
        public void ConvertEnemiesIntoSpawnSpots()
        {
            var enemies = FindObjectsOfType<Enemy.Enemy>();

            foreach (var enemy in enemies)
            {
                var prefabObject = PrefabUtility.InstantiatePrefab(this.spawnSpotPrefab, spawnSpotsParent) as GameObject;
                prefabObject.transform.position = enemy.transform.position;
                enemy.gameObject.SetActive(false);
            }
        }
    }
}