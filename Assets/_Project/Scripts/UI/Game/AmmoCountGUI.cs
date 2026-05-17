using gameoff.PlayerManager;
using TMPro;
using UnityEngine;

namespace gameoff.UI.Game
{
    public class AmmoCountGUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text ammoTMPText;
        private Blaster _blaster;

        private void Awake() => _blaster = FindObjectOfType<Blaster>();
        private void Start() => ammoTMPText.text = $"{_blaster.CurrentAmmo}/{_blaster.MaxAmmo}";
        private void OnEnable() => _blaster.AmmoChanged += OnAmmoChanged;
        private void OnDisable() => _blaster.AmmoChanged -= OnAmmoChanged;
        private void OnAmmoChanged(int ammoCount) => ammoTMPText.text = $"{ammoCount}/{_blaster.MaxAmmo}";
    }
}