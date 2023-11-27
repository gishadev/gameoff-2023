using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.World
{
    public class Level : MonoBehaviour
    {
        [ValidateInput(nameof(BaseMustBeNotNull), "This field must not be null.")]
        [SerializeField] private HumanBase humanBase;
        [ValidateInput(nameof(CreepMustBeNotNull), "This field must not be null.")]
        [SerializeField] private Creep creep;
        [ValidateInput(nameof(LevelBoundsMustBeNotNull), "This field must not be null.")]
        [SerializeField] private LevelBounds levelBounds;

        public HumanBase HumanBase => humanBase;
        public Creep Creep => creep;
        public LevelBounds LevelBounds => levelBounds;
        
        
        private bool BaseMustBeNotNull(HumanBase obj) => obj != null;
        private bool CreepMustBeNotNull(Creep obj) => obj != null;
        private bool LevelBoundsMustBeNotNull(LevelBounds obj) => obj != null;
    }
}