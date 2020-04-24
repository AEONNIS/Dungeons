using Dungeons.Model.LocalizationSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dungeons.Model.Tiles
{
    [CreateAssetMenu(fileName = "TileData", menuName = "Dungeons/Model/Tiles/TileData")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private TileType _type;
        [SerializeField] private LocalizationTextID _nameKey;
        [SerializeField] private LocalizationTextID _descriptionKey;
        [SerializeField] private List<TileStrengthState> _strengthStates;
        [SerializeField] private float _shutdownTimeWhenDestroyed = 5.0f;

        private string _name;
        private string _description;

        public TileType Type => _type;
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

        public TileStrengthState GetStrengthState(float strength)
        {
            return _strengthStates.First(state => state.StrengthIsInRangeMinExclusive(strength));
        }

        private void OnLanguageChanged()
        {
            _name = _localizer.GetLocalizedText(_nameKey);
            _description = _localizer.GetLocalizedText(_descriptionKey);
        }
    }

    public enum TileType { BlackStone, GrayStone, RedStone, Ground, Ice, Tree }
}
