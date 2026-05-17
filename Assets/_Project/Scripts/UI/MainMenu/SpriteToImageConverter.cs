using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.MainMenu
{
    public class SpriteToImageConverter : MonoBehaviour
    {
        [Button("Convert to images")]
        private void Convert()
        {
            var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (var sr in spriteRenderers)
            {
                var image = sr.gameObject.AddComponent<Image>();
                image.sprite = sr.sprite;
                DestroyImmediate(sr);
            }
        }
    }
}