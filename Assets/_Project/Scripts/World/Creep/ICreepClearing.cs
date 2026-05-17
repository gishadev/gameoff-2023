using UnityEngine;

namespace gameoff.World
{
    public interface ICreepClearing
    {
        void Init();
        void ClearCreep(Vector2 worldPos, int brushSize);
        void AddCreep(Vector2 worldPos, int brushSize, out int pixelsChanged);
    }
}