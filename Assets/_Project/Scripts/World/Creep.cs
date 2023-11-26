using UnityEngine;

namespace gameoff.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Creep : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spriteRenderers;

        public SpriteRenderer[] SpriteRenderers => spriteRenderers;
    }
}