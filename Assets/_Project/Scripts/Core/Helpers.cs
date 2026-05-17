using System.Collections.Generic;
using UnityEngine;

namespace gameoff.Core
{
    public static class Helpers
    {
        public static List<T> FindScriptableObjects<T>(string resourcesPath) where T : ScriptableObject
        {
            T[] scriptableObjects = Resources.LoadAll<T>(resourcesPath);
            return new List<T>(scriptableObjects);
        }
    }
}