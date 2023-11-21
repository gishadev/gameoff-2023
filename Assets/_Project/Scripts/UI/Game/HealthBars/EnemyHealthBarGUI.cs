using gameoff.Enemy;
using gameoff.PlayerManager;
using UnityEngine;

namespace gameoff.UI.Game
{
    public class EnemyHealthBarGUI : HealthBarGUI
    {
        [SerializeField] private GameObject enemyObject;
        
        protected override IDamageable Damageable { get; set; }

        protected override void OnEnable()
        {
            Damageable = enemyObject.GetComponent<IDamageable>();
            base.OnEnable();
        }
    }
}