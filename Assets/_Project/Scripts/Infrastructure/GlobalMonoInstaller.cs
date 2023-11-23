using gameoff.SavingLoading;
using Zenject;

namespace gameoff.Infrastructure
{
    public class GlobalMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<ISaveLoadController>().To<SaveLoadController>().AsSingle().NonLazy();
        }
    }
}