using Dungeons.Model.Items;
using Dungeons.Presentation.InventorySystem;
using UnityEngine;

namespace Dungeons.Model.InventorySystem
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private SlotPresenter _presenter;

        private Item _item;

        public Item Item => _item;

        public bool TrySet(Item item)
        {
            if (_item == null && item != null)
            {
                item.PlaceIn(ItemPlace.Inventory);
                _presenter.Present(_item = item);
                return true;
            }

            return false;
        }

        public Item PullOutItem()
        {
            if (_item != null)
            {
                Item item = _item;
                _item.PlaceIn(ItemPlace.Scene);
                _presenter.Present(_item = null);
                return item;
            }

            return null;
        }
    }
}
