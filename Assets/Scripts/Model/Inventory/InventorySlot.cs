using Game.UI.InventorySystem;
using UnityEngine;

namespace Game.Model.InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private SlotPresenter _presenter;

        private Item _item;

        public Item Item => _item;

        public bool TryPutItem(Item item)
        {
            if (_item == null)
            {
                _presenter.PresentItem(_item = item);
                return true;
            }

            return false;
        }

        public Item PickUpItem()
        {
            if (_item != null)
            {
                Item item = _item;
                _presenter.PresentItem(_item = null);
                return item;
            }

            return null;
        }
    }
}
