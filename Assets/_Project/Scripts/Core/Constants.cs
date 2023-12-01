using UnityEngine;

namespace gameoff.Core
{
    public static class Constants
    {
        public const string GAME_SCENE_NAME = "Game";
        public const string MAIN_MENU_SCENE_NAME = "MainMenu";
        public const string WIN_SCENE_NAME = "Win";
        public const string RESOURCES_UPGRADES_PATH = "Upgrades";
        public const string SAVE_FILE_STRING = "savefile";
        
        public const string CREEP_TEXTURES_FOLDER_PATH = "Assets/_Project/Textures/CreepMaps";
        public const string CREEP_TEXTURE_ORIGINAL_NAME = "CreepMapOriginal";
        
        public static readonly int AlphaTextureID = Shader.PropertyToID("_AlphaTexture");
        
        public const string MINIMAP_LAYER_NAME = "Minimap";
    }
}