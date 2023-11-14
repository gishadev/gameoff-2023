using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.Infrastructure
{
    public class GameSceneController : MonoBehaviour
    {
        [Inject] private ICreepClearing _creepClearing;

        private void Awake()
        {
            _creepClearing.Init();
        }
    }
}