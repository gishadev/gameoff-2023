using gameoff.PlayerManager;

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