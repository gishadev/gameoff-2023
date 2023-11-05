using gameoff.Core;
using gishadev.tools.SceneLoading;
using UnityEngine;

namespace gameoff.Infrastructure
{
    public class GameEnterPoint : MonoBehaviour
    {
        private void Awake()
        {
            SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        }
    }
}