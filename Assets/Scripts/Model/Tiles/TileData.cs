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
        [SerializeField] private CachedItem _name;
        [SerializeField] private CachedItem _description;
        [SerializeField] private List<TileStrengthState> _strengthStates;
        [SerializeField] private float _shutdownTimeWhenDestroyed = 5.0f;

        public TileType Type => _type;
        public string Name => _name.Value;
        public string Description => _description.Value;
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
            _name.ToCache(_localizer);
            _description.ToCache(_localizer);
        }
    }

    public enum TileType { BlackStone, GrayStone, RedStone, Ground, Ice, Tree }
}
