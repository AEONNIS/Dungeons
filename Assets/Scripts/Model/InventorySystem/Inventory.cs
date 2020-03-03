using Game.InventorySysytem;
using Game.UI.InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private InventorySlot _handsSlot;
        [SerializeField] private InventoryNotification _notification;
        [SerializeField] private ItemDetector _detector;

        private List<InventorySlot> _slots = new List<InventorySlot>();

        #region Unity
        private void Awake()
        {
            _slotsContainer.GetComponentsInChildren(_slots);
        }
        #endregion

        public void SwitchOpeningState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void TakeItemInHands(Item item)
        {
            if (_handsSlot.TrySetItem(item) == false)
            {
                InventorySlot slot = GetFirstEmptySlot();

                if (slot != null)
                {
                    slot.TrySetItem(_handsSlot.RemoveItem());
                    _handsSlot.TrySetItem(item);
                }
                else
                {
                    _notification.FullInventoryShowMessage();
                    return;
                }
            }

            _notification.TakingItemInHandsShowMessage(item);
        }

        public void PickUpAllItemsNearPlayer()
        {
            List<Item> items = _detector.GetItemsNearPlayer();
            List<InventorySlot> emptySlots = _slots.Where(slot => slot.Item == null).ToList();
            if (items.Count == 0 || emptySlots.Count == 0)
                return;

            for (int i = 0, s = 0; i < items.Count && s < emptySlots.Count; i++, s++)
                emptySlots[s].TrySetItem(items[i]);

            ShowNotifications(items.Count, emptySlots.Count);
        }

        private void ShowNotifications(int itemsNumber, int emptySlotsNumber)
        {
            _notification.ItemsPickedUpShowMessage(Mathf.Min(itemsNumber, emptySlotsNumber));

            int notPickedUpItems = itemsNumber - emptySlotsNumber;
            if (notPickedUpItems > 0)
            {
                _notification.ItemsDidNotFitIntoInventoryShowMessage(notPickedUpItems);
                _notification.FullInventoryShowMessage();
            }
        }

        public void SwapItemsInSlots(InventorySlot slotA, InventorySlot slotB)
        {
            Item itemA = slotA.RemoveItem();
            slotA.TrySetItem(slotB.RemoveItem());
            slotB.TrySetItem(itemA);
        }

        public void ThrowItemFromSlot(InventorySlot inventorySlot)
        {
            Item item = _slots.First(slot => slot == inventorySlot).RemoveItem();

            if (item != null)
                PutItemNearPlayer(item);
        }

        private InventorySlot GetFirstEmptySlot()
        {
            return _slots.FirstOrDefault(slot => slot.Item == null);
        }

        private void PutItemNearPlayer(Item item)
        {
            item.transform.position = _playerTransform.position;
            item.gameObject.SetActive(true);
        }
    }
}
