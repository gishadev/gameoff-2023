using gameoff.Core;
using gameoff.Enemy;
using gameoff.PlayerManager;

namespace gameoff.UI.Game
{
    public class PlayerHealthBarGUI : HealthBarGUI
    {
        protected override IDamageable Damageable { get; set; }

        protected override void OnEnable()
        {
            Damageable = FindObjectOfType<Player>();
            base.OnEnable();
        }
    }
}