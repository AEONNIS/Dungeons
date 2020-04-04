using Game.Model.Items;
using Game.Presentation.UI.NotificationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Presentation.UI.InventorySystem
{
    public class InventoryNotifier : MonoBehaviour
    {
        [SerializeField] private Notifier _notifier;
        [SerializeField] private List<NotificationMessage> _messages;

        public void TakingItemInHandsShow(Item item)
        {
            _notifier.ShowMessage($"{GetMessage(Notification.TakingItemInHands)}{item.Name}", item.Sprite);
        }

        public void ThrowingItemFromHandsShow(Item item)
        {
            _notifier.ShowMessage($"{GetMessage(Notification.ThrowingItemFromHands)}{item.Name}", item.Sprite);
        }

        public void FullInventoryShow()
        {
            _notifier.ShowMessage(GetMessage(Notification.FullInventory));
        }

        public void ItemsTakenNumberShow(int itemsNumber)
        {
            _notifier.ShowMessage($"{GetMessage(Notification.ItemsTakenNumber)}{itemsNumber}");
        }

        public void NotFitIntoInventoryItemsNumberShow(int itemsNumber)
        {
            _notifier.ShowMessage($"{GetMessage(Notification.NotFitIntoInventoryItemsNumber)}{itemsNumber}");
        }

        private string GetMessage(Notification notification)
        {
            return _messages.First(message => message.Notification == notification).Message;
        }

        [Serializable]
        private class NotificationMessage
        {
            [SerializeField] private Notification _notification;
            [TextArea(2, 5)]
            [SerializeField] private string _message;

            public string Message => _message;
            public Notification Notification => _notification;
        }

        public enum Notification
        {
            TakingItemInHands,
            ThrowingItemFromHands,
            FullInventory,
            ItemsTakenNumber,
            NotFitIntoInventoryItemsNumber
        }
    }
}
