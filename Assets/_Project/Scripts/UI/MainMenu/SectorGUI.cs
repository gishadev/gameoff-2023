using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.UI.MainMenu
{
    public class SectorGUI : MonoBehaviour
    {
        [Button]
        public void ShowClosed()
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);
        }

        [Button]
        public void ShowInfected()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        [Button]
        public void ShowCompleted()
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}