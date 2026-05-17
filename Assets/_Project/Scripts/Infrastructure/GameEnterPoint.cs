using gameoff.Core;
using gameoff.SavingLoading;
using gishadev.tools.SceneLoading;
using UnityEngine;
using Zenject;

namespace gameoff.Infrastructure
{
    public class GameEnterPoint : MonoBehaviour
    {
        [Inject] private ISaveLoadController _saveLoadController;
        private void Awake()
        {
            _saveLoadController.LoadGame();
            SceneLoader.I.AsyncSceneLoad(Constants.MAIN_MENU_SCENE_NAME);
        }
    }
}