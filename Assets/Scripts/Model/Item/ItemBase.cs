using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Item", menuName = "Model/Item")]
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private string _name;
        [TextArea(3, 7)]
        [SerializeField] private string _description;
        [SerializeField] private List<ItemPlaceState> _placeStates;
        [SerializeField] private List<ItemStrengthState> _strengthStates;

        public ItemType Type => _type;
        public string Name => _name;
        public string Description => _description;

        public ItemPlaceState GetPlaceState(ItemPlace place)
        {
            return _placeStates.First(state => state.Place == place);
        }

        public ItemStrengthState GetStrengthState(float strength)
        {
            return _strengthStates.First(state => state.StrengthIsInRangeMinExclusive(strength));
        }
    }

    public enum ItemType { Axe, Pick, Shovel }
}
