using System.Collections.Generic;
using System.Linq;
using gishadev.tools.Pooling;
using UnityEngine;

namespace gishadev.tools.Effects
{
    public class VFXEmitter : PoolManager<VFXPoolObject>, IPoolEmitter
    {
        public static VFXEmitter I
        {
            get
            {
                if (_current)
                {
                    _current.Initialize();
                    return _current;
                }

                _current = new GameObject("[VFXEmitter]").AddComponent<VFXEmitter>();
                DontDestroyOnLoad(_current.gameObject);
                _current.Initialize();
                
                return _current;
            }
        }

        private static VFXEmitter _current;

        protected override Transform Parent { get; set; }
        protected override List<VFXPoolObject> PoolObjectsCollection => PoolDataSO.VFXPoolObjects.ToList();

        protected override void Awake()
        {
            base.Awake();
            Parent = transform;
        }

        public GameObject EmitAt(int index, Vector3 position, Quaternion rotation)
        {
            if (!TryInstantiate(index, out var obj))
                return null;

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        public GameObject EmitAt(VisualEffectsEnum enumEntry, Vector3 position, Quaternion rotation) =>
            EmitAt((int) enumEntry, position, rotation);
    }
}