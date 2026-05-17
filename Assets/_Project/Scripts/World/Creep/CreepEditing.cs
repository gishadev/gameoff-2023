using System.IO;
using gameoff.Core;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace gameoff.World
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Creep))]
    public class CreepEditing : MonoBehaviour
    {
        [SerializeField] private string suffix;

        [SerializeField] private Texture2D textureToEdit;
        [SerializeField] private int brushSize = 5;

        private CreepClearing _creepClearing;
        private Creep _creep;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
                return;

            _creep = GetComponent<Creep>();
            _creepClearing = new CreepClearing(textureToEdit);
            _creepClearing.SetCreep(_creep);

            if (Keyboard.current.cKey.isPressed)
            {
                // Get the mouse position in GUI space
                Vector2 mousePositionGUI = Event.current.mousePosition;

                // Convert the mouse position from GUI space to world space using the SceneView's camera
                Ray ray = HandleUtility.GUIPointToWorldRay(mousePositionGUI);
                _creepClearing.ClearCreep(ray.origin, brushSize);

                AssetDatabase.RefreshSettings();
                AssetDatabase.SaveAssets();
            }

            if (Keyboard.current.fKey.isPressed)
            {
                // Get the mouse position in GUI space
                Vector2 mousePositionGUI = Event.current.mousePosition;

                // Convert the mouse position from GUI space to world space using the SceneView's camera
                Ray ray = HandleUtility.GUIPointToWorldRay(mousePositionGUI);
                _creepClearing.AddCreep(ray.origin, brushSize, out var pixelsChanged);

                AssetDatabase.RefreshSettings();
                AssetDatabase.SaveAssets();
            }
        }

        [Button("Copy texture")]
        private void CopyTexture()
        {
            var originalPath = $"{Constants.CREEP_TEXTURES_FOLDER_PATH}/{Constants.CREEP_TEXTURE_ORIGINAL_NAME}.jpg";

            var filesCount = Directory.GetFiles(Constants.CREEP_TEXTURES_FOLDER_PATH).Length;
            var copyName = $"{Constants.CREEP_TEXTURE_ORIGINAL_NAME}_Copy_{suffix}_{filesCount + 1}.jpg";
            var copyPath = $"{Constants.CREEP_TEXTURES_FOLDER_PATH}/{copyName}";

            if (!AssetDatabase.CopyAsset(originalPath, copyPath))
                Debug.LogWarning($"Failed to copy {originalPath}");

            textureToEdit = AssetDatabase.LoadAssetAtPath(copyPath, typeof(Texture2D)) as Texture2D;
            _creep.SpriteRenderers[0].sharedMaterial.SetTexture(Constants.AlphaTextureID, textureToEdit);
        }

        [Button("Save texture")]
        private void SaveTexture()
        {
            var path = $"{Constants.CREEP_TEXTURES_FOLDER_PATH}/{textureToEdit.name}.jpg";

            byte[] bytes = textureToEdit.EncodeToJPG();
            File.WriteAllBytes(path, bytes);
        }
#endif
    }
}