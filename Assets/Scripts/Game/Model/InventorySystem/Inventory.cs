using Game.Infrastructure.InventorySysytem;
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
        [SerializeField] private InventoryNotifier _notification;
        [SerializeField] private ItemDetector _detector;

        private readonly List<InventorySlot> _inventorySlots = new List<InventorySlot>();
        private List<InventorySlot> _allSlots = new List<InventorySlot>();

        public Item InHandsItem => _handsSlot.Item;

        #region Unity
        private void Awake()
        {
            _slotsContainer.GetComponentsInChildren(_inventorySlots);
            _allSlots = _inventorySlots;
            _allSlots.Add(_handsSlot);
        }
        #endregion

        public void SwitchOpeningState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public bool TryTakeItemInHands(Item item)
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
                    _notification.FullInventoryShow();
                    return false;
                }
            }

            _notification.TakingItemInHandsShow(item);
            return true;
        }

        public void PickUpAllItemsNearPlayer()
        {
            List<Item> items = _detector.DetectItems();
            List<InventorySlot> emptySlots = _inventorySlots.Where(slot => slot.Item == null).ToList();
            if (items.Count == 0 || emptySlots.Count == 0)
                return;

            for (int i = 0, s = 0; i < items.Count && s < emptySlots.Count; i++, s++)
                emptySlots[s].TrySetItem(items[i]);

            ShowNotifications(items.Count, emptySlots.Count);
        }

        private void ShowNotifications(int itemsNumber, int emptySlotsNumber)
        {
            _notification.ItemsTakenNumberShow(Mathf.Min(itemsNumber, emptySlotsNumber));

            int notPickedUpItems = itemsNumber - emptySlotsNumber;
            if (notPickedUpItems > 0)
                _notification.NotFitIntoInventoryItemsNumberShow(notPickedUpItems);
        }

        public void SwapItemsInSlots(InventorySlot slotA, InventorySlot slotB)
        {
            if (slotA.Item != null || slotB.Item != null)
            {
                Item itemA = slotA.RemoveItem();
                slotA.TrySetItem(slotB.RemoveItem());
                slotB.TrySetItem(itemA);
            }
        }

        public void ThrowItemFromSlot(InventorySlot inventorySlot)
        {
            Item item = _allSlots.First(slot => slot == inventorySlot).RemoveItem();

            if (item != null)
                PutItemNearPlayer(item);
        }

        private InventorySlot GetFirstEmptySlot()
        {
            return _inventorySlots.FirstOrDefault(slot => slot.Item == null);
        }

        private void PutItemNearPlayer(Item item)
        {
            item.transform.position = _playerTransform.position;
            item.gameObject.SetActive(true);
        }
    }
}
