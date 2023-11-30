using gameoff.PlayerManager;

namespace gameoff.UI.Game
{
    public class ReloadingCooldownHandler : CooldownHandler
    {
        private Blaster _blaster;

        protected override void Awake()
        {
            base.Awake();
            _blaster = FindObjectOfType<Blaster>();
        }

        private void OnEnable() => _blaster.ReloadingStarted += OnReloadingStarted;
        private void OnDisable() => _blaster.ReloadingStarted -= OnReloadingStarted;
        private void OnReloadingStarted() => ShowCooldown(_blaster.ReloadingDelay);
    }
}