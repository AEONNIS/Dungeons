using Game.Operations.LocalizationSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game/Model/Items")]
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private ItemType _type;
        [SerializeField] private LocalizationTextID _nameKey;
        [SerializeField] private LocalizationTextID _descriptionKey;
        [SerializeField] private List<ItemPlaceState> _placeStates;
        [SerializeField] private List<ItemStrengthState> _strengthStates;

        private string _name;
        private string _description;

        public ItemType Type => _type;
        public string Name => _name;
        public string Description => _description;

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

        public ItemPlaceState GetPlaceState(ItemPlace place)
        {
            return _placeStates.First(state => state.Place == place);
        }

        public ItemStrengthState GetStrengthState(float strength)
        {
            return _strengthStates.First(state => state.StrengthIsInRangeMinExclusive(strength));
        }

        private void OnLanguageChanged()
        {
            _name = _localizer.GetLocalizedText(_nameKey);
            _description = _localizer.GetLocalizedText(_descriptionKey);
        }
    }

    public enum ItemType { Axe, Pick, Shovel }
}
