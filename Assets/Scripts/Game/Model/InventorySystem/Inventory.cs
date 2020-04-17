using Game.Infrastructure;
using Game.Model.Items;
using Game.Model.PlayerCharacter;
using Game.Presentation.UI.InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Slot _handsSlot;
        [SerializeField] private InventoryPresenter _presenter;
        [SerializeField] private InventoryNotifier _notification;
        [SerializeField] private Detector _itemDetector;
        [SerializeField] private Player _player;

        private List<Slot> _inventorySlots;
        private List<Slot> _allSlots;

        public Item InHandsItem => _handsSlot.Item;

        public void Init(List<Slot> slots)
        {
            _inventorySlots = slots;
            _allSlots = slots;
            _allSlots.Add(_handsSlot);
        }

        public void SwitchState()
        {
            _presenter.SwitchState();
        }

        public bool TryToTakeInHands(Item item)
        {
            if (_handsSlot.TrySet(item) == false)
            {
                Slot slot = GetFirstEmptySlot();

                if (slot != null)
                {
                    slot.TrySet(_handsSlot.PullOutItem());
                    _handsSlot.TrySet(item);
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
            List<Item> items = _itemDetector.Detect<Item>();
            List<Slot> emptySlots = _inventorySlots.Where(slot => slot.Item == null).ToList();
            if (items.Count == 0 || emptySlots.Count == 0)
                return;

            for (int i = 0, s = 0; i < items.Count && s < emptySlots.Count; i++, s++)
                emptySlots[s].TrySet(items[i]);

            ShowNotifications(items.Count, emptySlots.Count);
        }

        private void ShowNotifications(int itemsNumber, int emptySlotsNumber)
        {
            _notification.ItemsTakenNumberShow(Mathf.Min(itemsNumber, emptySlotsNumber));

            int notPickedUpItems = itemsNumber - emptySlotsNumber;
            if (notPickedUpItems > 0)
                _notification.NotFitIntoInventoryItemsNumberShow(notPickedUpItems);
        }

        public void SwapItemsInSlots(Slot slotA, Slot slotB)
        {
            if (slotA.Item != null || slotB.Item != null)
            {
                Item itemA = slotA.PullOutItem();
                slotA.TrySet(slotB.PullOutItem());
                slotB.TrySet(itemA);
            }
        }

        public void PullOutItemFrom(Slot slot)
        {
            Item item = _allSlots.First(_slot => _slot == slot).PullOutItem();

            if (item != null)
                _player.PutNear(item);
        }

        private Slot GetFirstEmptySlot()
        {
            return _inventorySlots.FirstOrDefault(slot => slot.Item == null);
        }
    }
}
