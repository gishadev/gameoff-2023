using gameoff.PlayerManager.SOs;

namespace gameoff.PlayerManager
{
    public interface IAbility
    {
        bool IsUsing { get; }
        AbilityDataSO AbilityDataSO { get; }
        void Trigger();
        void Cancel();
    }
}