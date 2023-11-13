using UnityEngine;

namespace gameoff.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Creep : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                    
                return _spriteRenderer;
            }
        }
    }
}