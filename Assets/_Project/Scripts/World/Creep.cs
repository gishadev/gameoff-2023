﻿using UnityEngine;

namespace gameoff.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Creep : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        private static readonly int AlphaTextureID = Shader.PropertyToID("_AlphaTexture");
        private Texture2D _tmpTexture;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _tmpTexture = CopyTexture2D(SpriteRenderer.material.GetTexture(AlphaTextureID) as Texture2D);
        }

        public void ClearCreep(Vector2 worldPos, int brushSize)
        {
            Vector2 textureCoordinates = WorldToTextureCoordinates(worldPos);
            UpdateTexturePixels(textureCoordinates, brushSize);
        }

        private void UpdateTexturePixels(Vector2 centerCoordinates, int brushRadius)
        {
            // Ensure the texture coordinates are within the valid range (0 to 1)
            centerCoordinates = new Vector2(
                Mathf.Clamp01(centerCoordinates.x),
                Mathf.Clamp01(centerCoordinates.y)
            );

            // Convert texture coordinates to pixel coordinates
            int centerX = Mathf.FloorToInt(centerCoordinates.x * _tmpTexture.width);
            int centerY = Mathf.FloorToInt(centerCoordinates.y * _tmpTexture.height);

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
                        _tmpTexture.SetPixel(x, y, Color.clear);
                }
            }

            // Apply the changes to the texture
            _tmpTexture.Apply();
            SpriteRenderer.material.SetTexture(AlphaTextureID, _tmpTexture);
        }

        private Vector2 WorldToTextureCoordinates(Vector3 worldPosition)
        {
            Vector2 textureCoordinates = new Vector2(
                Mathf.InverseLerp(SpriteRenderer.bounds.min.x, SpriteRenderer.bounds.max.x, worldPosition.x),
                Mathf.InverseLerp(SpriteRenderer.bounds.min.y, SpriteRenderer.bounds.max.y, worldPosition.y)
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