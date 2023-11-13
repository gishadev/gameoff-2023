using gameoff.World;
using Zenject;

namespace gameoff.Infrastructure
{
    public class GameMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<ICreepClearing>().To<CreepClearing>().AsSingle().NonLazy();
        }
    }
}