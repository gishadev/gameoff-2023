using gameoff.Core;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using UnityEngine;

namespace gameoff.UI.MainMenu
{
    public class MainMenuController : MenuController
    {
        public void OnPlayClicked()
        {
            SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}