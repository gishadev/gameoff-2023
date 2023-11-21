using gameoff.Core;
using gishadev.tools.SceneLoading;
using UnityEngine;

namespace gameoff.Infrastructure
{
    public class GameEnterPoint : MonoBehaviour
    {
        private void Awake() => SceneLoader.I.AsyncSceneLoad(Constants.MAIN_MENU_SCENE_NAME);
    }
}