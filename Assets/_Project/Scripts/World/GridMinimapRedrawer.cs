using gameoff.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace gameoff.World
{
    public class GridMinimapRedrawer : MonoBehaviour
    {
        [SerializeField] private GameObject gridMainObject;

        private Transform _redrawParent;

        private void Awake()
        {
            if (gridMainObject == null)
            {
                Debug.LogError("No grid main object assigned");
                return;
            }

            var tilemaps = gridMainObject.GetComponentsInChildren<TilemapRenderer>();
            foreach (var tilemap in tilemaps) 
                tilemap.sortingLayerName = "Ground";

            CreateGridCopyAndAssignMinimapLayer();
        }

        private void CreateGridCopyAndAssignMinimapLayer()
        {
            var redrawParentObject = new GameObject("[Minimap Grid Redraw]");
            redrawParentObject.transform.SetParent(transform);
            _redrawParent = redrawParentObject.transform;

            var gridCopy = Instantiate(gridMainObject, _redrawParent);
            foreach (var go in gridCopy.GetComponentsInChildren<Transform>())
            {
                go.gameObject.layer = LayerMask.NameToLayer(Constants.MINIMAP_LAYER_NAME);

                if (go.TryGetComponent(out Collider2D coll))
                    coll.enabled = false;
                
                if (go.TryGetComponent(out TilemapRenderer tilemapRenderer))
                    tilemapRenderer.sortingOrder = -1;
            }
        }
    }
}