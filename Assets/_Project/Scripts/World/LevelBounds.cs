using UnityEngine;

namespace gameoff.World
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelBounds : MonoBehaviour
    {
        private Collider2D _collider;

        public Collider2D Collider => _collider;

        private void Awake() => _collider = GetComponent<Collider2D>();
    }
}