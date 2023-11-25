using System;
using gameoff.Enemy.SOs;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
       public abstract EnemyDataSO EnemyDataSO { get; }

       public event Action<int> HealthChanged;
       public int StartHealth => EnemyDataSO.StartHealth;
       public IEnemySpawnData SpawnData { private set; get; }
       protected StateMachine StateMachine { get;  set; }
       public int CurrentHealth { get; private set; }
       protected EnemyMovement EnemyMovement { get; private set; }
       
       private void Awake()
       {
           EnemyMovement = GetComponent<EnemyMovement>();
       }

       private void OnEnable()
       {
           CurrentHealth = EnemyDataSO.StartHealth;
           InitStateMachine();
       }
       
       private void Update() => StateMachine.Tick();

       protected abstract void InitStateMachine();
       
       public void TakeDamage(int count)
       {
           CurrentHealth -= count;
           HealthChanged?.Invoke(CurrentHealth);
       }
       
       public void SetSpawnData(IEnemySpawnData spawnData) => SpawnData = spawnData;

    }
}