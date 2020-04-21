using Game.Infrastructure.Presentation.UI;
using Game.Model.PlayerCharacter;
using Game.Operations.LocalizationSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private ImageValueBar _healthBar;
        [SerializeField] private Text _powerState;
        [SerializeField] private List<LocalizationTextID> _powerStateNameKeys;

        private List<string> _powerStateNames;

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
            _powerState.text = _powerStateNames[(int)powerState];
        }

        private void OnLanguageChanged()
        {
            _powerStateNames = new List<string>(_powerStateNameKeys.Count);
            _powerStateNameKeys.ForEach(nameKey => _powerStateNames.Add(_localizer.GetLocalizedText(nameKey)));
        }
    }
}
