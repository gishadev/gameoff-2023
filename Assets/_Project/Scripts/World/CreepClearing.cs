using gameoff.Core;
using UnityEngine;

namespace gameoff.World
{
    public class CreepClearing : ICreepClearing
    {
        private Texture2D _tmpTexture;
        
        private Creep _creep;

        public CreepClearing()
        {
        }

        public CreepClearing(Texture2D targetTexture)
        {
            _tmpTexture = targetTexture;
        }
        
        public void Init()
        {
            _creep = Object.FindObjectOfType<Creep>();
            _tmpTexture = CopyTexture2D(_creep.CreepAlphaTexture);
        }

        public void SetCreep(Creep creep)
        {
            _creep = creep;
        }
        
        public void ClearCreep(Vector2 worldPos, int brushSize)
        {
            if (_creep == null)
                return;

            Vector2 textureCoordinates = WorldToTextureCoordinates(worldPos);
            UpdateTexturePixels(textureCoordinates, brushSize, Color.clear, out var pixelsChanged);
        }
        
        public void AddCreep(Vector2 worldPos, int brushSize, out int pixelsChanged)
        {
            pixelsChanged = 0;
            if (_creep == null)
                return;

            Vector2 textureCoordinates = WorldToTextureCoordinates(worldPos);
            UpdateTexturePixels(textureCoordinates, brushSize, Color.white, out pixelsChanged);
        }

        private void UpdateTexturePixels(Vector2 centerCoordinates, int brushRadius, Color color, out int pixelsChangedCount)
        {
            pixelsChangedCount = 0;
            // Ensure the texture coordinates are within the valid range (0 to 1)
            centerCoordinates = new Vector2(
                Mathf.Clamp01(centerCoordinates.x),
                Mathf.Clamp01(centerCoordinates.y)
            );

            // Convert texture coordinates to pixel coordinates
            int centerX = Mathf.FloorToInt(centerCoordinates.x * _tmpTexture.width);
            int centerY = Mathf.FloorToInt(centerCoordinates.y * _tmpTexture.height);

            bool pixelsChanged = false;
            // Iterate over a square region around the center (adjust as needed)
            for (int x = Mathf.Max(0, centerX - brushRadius);
                 x < Mathf.Min(_tmpTexture.width, centerX + brushRadius);
                 x++)
            {
                for (int y = Mathf.Max(0, centerY - brushRadius);
                     y < Mathf.Min(_tmpTexture.height, centerY + brushRadius);
                     y++)
                {
                    if (Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY)) <= brushRadius)
                    {
                        if (_tmpTexture.GetPixel(x, y) != color)
                        {
                            _tmpTexture.SetPixel(x, y, color);
                            pixelsChangedCount++;
                            pixelsChanged = true;
                        }
                    }
                }
            }

            if (pixelsChanged)
            {
                // Apply the changes to the texture
                _tmpTexture.Apply();
                foreach (var sr in _creep.SpriteRenderers) 
                    sr.material.SetTexture(Constants.AlphaTextureID, _tmpTexture);
            }
        }

        private Vector2 WorldToTextureCoordinates(Vector3 worldPosition)
        {
            Vector2 textureCoordinates = new Vector2(
                Mathf.InverseLerp(_creep.SpriteRenderers[0].bounds.min.x, _creep.SpriteRenderers[0].bounds.max.x,
                    worldPosition.x),
                Mathf.InverseLerp(_creep.SpriteRenderers[0].bounds.min.y, _creep.SpriteRenderers[0].bounds.max.y,
                    worldPosition.y)
            );

            return textureCoordinates;
        }

        private Texture2D CopyTexture2D(Texture2D copiedTexture)
        {
            var texture = new Texture2D(copiedTexture.width, copiedTexture.height);

            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            int y = 0;
            while (y < texture.height)
            {
                int x = 0;
                while (x < texture.width)
                {
                    texture.SetPixel(x, y, copiedTexture.GetPixel(x, y));
                    ++x;
                }

                ++y;
            }

            texture.Apply();
            return texture;
        }
    }
}