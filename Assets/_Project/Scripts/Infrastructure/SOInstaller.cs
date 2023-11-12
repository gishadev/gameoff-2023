using gameoff.Core;
using UnityEngine;
using Zenject;

namespace gameoff.Infrastructure
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        [SerializeField] private GameDataSO gameDataSO;
        
        public override void InstallBindings()
        {
            Container.BindInstances(gameDataSO);
        }
    }
}