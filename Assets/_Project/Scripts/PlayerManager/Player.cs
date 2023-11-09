using System;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class Player : MonoBehaviour
    {
        public static Player Current { get; private set; }

        private void Awake()
        {
            Current = this;
        }
    }
}