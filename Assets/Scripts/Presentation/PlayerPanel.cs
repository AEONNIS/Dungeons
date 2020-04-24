using Dungeons.Infrastructure.Presentation;
using Dungeons.Model.LocalizationSystem;
using Dungeons.Model.PlayerCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Presentation
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private ImageValueBar _healthBar;
        [SerializeField] private Text _powerState;
        [SerializeField] private CachedData _powerStateNames;

        #region Unity
        private void OnEnable()
        {
            _localizer.LanguageChanged += OnLanguageChanged;
        }

        private void OnDisable()
        {
            _localizer.LanguageChanged -= OnLanguageChanged;
        }
        #endregion

        public void Present(float health, PowerState powerState)
        {
            PresentHealth(health);
            PresentPowerState(powerState);
        }

        public void PresentHealth(float health)
        {
            _healthBar.Present(health);
        }

        public void PresentPowerState(PowerState powerState)
        {
            _powerState.text = _powerStateNames.GetValue((int)powerState);
        }

        private void OnLanguageChanged()
        {
            _powerStateNames.ToCache(_localizer);
        }
    }
}
