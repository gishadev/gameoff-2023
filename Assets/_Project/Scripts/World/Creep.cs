using UnityEngine;

namespace gameoff.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Creep : MonoBehaviour
    {
        private SpriteRenderer[] _spriteRenderers;

        public SpriteRenderer[] SpriteRenderers
        {
            get
            {
                if (_spriteRenderers == null)
                    _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
                    
                return _spriteRenderers;
            }
        }
    }
}