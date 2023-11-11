using System;
using System.Linq;
using gameoff.Enemy;
using gameoff.World;
using UnityEngine;

namespace gameoff.Core
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Hive.Died += OnHiveDied;
        }

        private void OnDisable()
        {
            Hive.Died -= OnHiveDied;
        }
        
        private void OnHiveDied(Hive hive)
        {
            var hives = FindObjectsOfType<Hive>().Where(x => x != hive).ToArray();
            if (hives.Length <= 0) 
                Win();
        }

        private void Lose()
        {
            Debug.Log("Lose");
        }

        private void Win()
        {
            Debug.Log("Win");
        }
    }
}