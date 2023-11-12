using System;
using System.Linq;
using gameoff.Enemy;
using gameoff.PlayerManager;
using gameoff.World;
using UnityEngine;

namespace gameoff.Core
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Hive.Died += OnHiveDied;
            Player.Current.Died += Lose;
        }

        private void OnDisable()
        {
            Hive.Died -= OnHiveDied;
            Player.Current.Died -= Lose;
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