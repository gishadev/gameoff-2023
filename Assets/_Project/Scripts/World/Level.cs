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

        public HumanBase HumanBase => humanBase;
        public Creep Creep => creep;

        private bool BaseMustBeNotNull(HumanBase obj) => obj != null;
        private bool CreepMustBeNotNull(Creep obj) => obj != null;
    }
}