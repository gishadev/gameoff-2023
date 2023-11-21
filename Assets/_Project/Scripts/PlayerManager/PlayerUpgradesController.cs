namespace gameoff.PlayerManager
{
    public class PlayerUpgradesController
    {
        public void UnlockAbility(IAbility ability)
        {
            
        }

        public void Upgrade(IUpgradeable upgradeable)
        {
            
        }
    }

    public interface IUpgradeable
    {
        public void OnUpgrade();
    }
}