using Game.Operations.LocalizationSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.Tiles
{
    [CreateAssetMenu(fileName = "Tile", menuName = "Game/Model/Tiles")]
    public class EnvironmentBase : ScriptableObject
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private EnvironmentType _type;
        [SerializeField] private LocalizationTextID _nameKey;
        [SerializeField] private LocalizationTextID _descriptionKey;
        [SerializeField] private List<EnvironmentStrengthState> _strengthStates;
        [SerializeField] private float _shutdownTimeWhenDestroyed = 5.0f;

        private string _name;
        private string _description;

        public EnvironmentType Type => _type;
        public string Name => _name;
        public string Description => _description;
        public float ShutdownTimeWhenDestroyed => _shutdownTimeWhenDestroyed;

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

        public EnvironmentStrengthState GetStrengthState(float strength)
        {
            return _strengthStates.First(state => state.StrengthIsInRangeMinExclusive(strength));
        }

        private void OnLanguageChanged()
        {
            _name = _localizer.GetLocalizedText(_nameKey);
            _description = _localizer.GetLocalizedText(_descriptionKey);
        }
    }

    public enum EnvironmentType { BlackStone, GrayStone, RedStone, Ground, Ice, Tree }
}
