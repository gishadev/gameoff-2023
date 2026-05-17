using System.Linq;
using gameoff.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    public class HivesScaleGUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text hivesCountTMP;
        [SerializeField] private Image hivesScaleImage;

        private int _hivesMaxCount;

        private void Awake() => _hivesMaxCount = FindObjectsOfType<Hive>().Length;

        private void Start()
        {
            var hives = FindObjectsOfType<Hive>();
            UpdateGUI(hives);
        }

        private void OnEnable() => Hive.Died += OnHiveDied;
        private void OnDisable() => Hive.Died -= OnHiveDied;

        private void OnHiveDied(Hive hive)
        {
            var hives = FindObjectsOfType<Hive>().Where(x => x != hive).ToArray();
            UpdateGUI(hives);
        }

        private void UpdateGUI(Hive[] hives)
        {
            hivesCountTMP.text = $"{hives.Length}/{_hivesMaxCount} HIVES";
            hivesScaleImage.transform.localScale = new Vector3(hives.Length / (float) _hivesMaxCount, 1f, 1f);
        }
    }
}