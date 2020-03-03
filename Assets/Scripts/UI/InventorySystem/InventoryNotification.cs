using Game.Model;
using UnityEngine;

namespace Game.UI.InventorySystem
{
    public class InventoryNotification : MonoBehaviour
    {
        [SerializeField] private Notifier _notifier;
        [TextArea(2, 5)]
        [SerializeField] private string _takeItemInHandsMessage;
        [TextArea(2, 5)]
        [SerializeField] private string _throwItemFromHandMessage;
        [TextArea(2, 5)]
        [SerializeField] private string _fullInventoryMessage;
        [TextArea(2, 5)]
        [SerializeField] private string _itemsPickedUpMessage;
        [TextArea(2, 5)]
        [SerializeField] private string _itemsDidNotFitIntoInventoryMessage;

        public void TakingItemInHandsShowMessage(Item item)
        {
            _notifier.ShowMessage($"{_takeItemInHandsMessage}{item.Name}", item.Sprite);
        }

        public void ThrowingItemFromHandsShowMessage(Item item)
        {
            _notifier.ShowMessage($"{_throwItemFromHandMessage}{item.Name}", item.Sprite);
        }

        public void FullInventoryShowMessage()
        {
            _notifier.ShowMessage(_fullInventoryMessage);
        }

        public void ItemsPickedUpShowMessage(int itemsNumber)
        {
            _notifier.ShowMessage($"{_itemsPickedUpMessage}{itemsNumber}");
        }

        public void ItemsDidNotFitIntoInventoryShowMessage(int itemsNumber)
        {
            _notifier.ShowMessage($"{_itemsDidNotFitIntoInventoryMessage}{itemsNumber}");
        }
    }
}
