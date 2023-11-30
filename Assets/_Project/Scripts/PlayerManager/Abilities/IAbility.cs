using System;

namespace gameoff.PlayerManager
{
    public interface IAbility
    {
        bool IsUsing { get; }
        AbilityDataSO AbilityDataSO { get; }
        void Trigger();
        void Cancel();

        static event Action<IAbility> Used;
        public static void RaiseUsed(IAbility ability) => Used?.Invoke(ability);
    }
}