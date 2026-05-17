using gameoff.Core;
using gishadev.tools.Audio;
using gishadev.tools.SceneLoading;
using UnityEngine;

namespace gameoff.UI.MainMenu
{
    public class ReturnToMainMenu : MonoBehaviour
    {
        public void OnMainMenuClicked()
        {
            SceneLoader.I.AsyncSceneLoad(Constants.MAIN_MENU_SCENE_NAME);
            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }
    }
}