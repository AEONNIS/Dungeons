using Game.UI.InventorySystem;
using UnityEngine;

namespace Game.Model.InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private SlotPresenter _presenter;

        private Item _item;

        public Item Item => _item;

        public bool TrySetItem(Item item)
        {
            if (_item == null && item != null)
            {
                item.PlaceIn(ItemPlace.Inventory);
                _presenter.PresentItem(_item = item);
                return true;
            }

            return false;
        }

        public Item RemoveItem()
        {
            if (_item != null)
            {
                Item item = _item;
                item.PlaceIn(ItemPlace.Scene);
                _presenter.PresentItem(_item = null);
                return item;
            }

            return null;
        }
    }
}
