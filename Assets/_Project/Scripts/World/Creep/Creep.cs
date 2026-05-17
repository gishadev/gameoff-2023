using gameoff.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Creep : MonoBehaviour
    {
        [OnValueChanged("UpdateShaderAlphaTexture")]
        [field: SerializeField]
        public Texture2D CreepAlphaTexture { get; private set; }

        [SerializeField] private SpriteRenderer[] spriteRenderers;

        public SpriteRenderer[] SpriteRenderers => spriteRenderers;

        public void SetCreepAlphaTexture(Texture2D newTexture)
        {
            CreepAlphaTexture = newTexture;
            UpdateShaderAlphaTexture();
        }

        [Button]
        private void UpdateShaderAlphaTexture()
        {
            foreach (var sr in SpriteRenderers)
                sr.sharedMaterial.SetTexture(Constants.AlphaTextureID, CreepAlphaTexture);
        }
    }
}