using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _slotsContainer;

        private List<InventorySlot> _slots;

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

        public void SwapItemsInSlots(InventorySlot slotA, InventorySlot slotB)
        {
            Item itemA = slotA.PickUpItem();
            slotA.TryPutItem(slotB.PickUpItem());
            slotB.TryPutItem(itemA);
        }

        public void ThrowItemFromSlot(InventorySlot inventorySlot)
        {
            Item item = _slots.First(slot => slot == inventorySlot).PickUpItem();

            if (item != null)
                PutItemNearPlayer(item);
        }

        private void PutItemNearPlayer(Item item)
        {
            item.transform.position = _playerTransform.position;
            item.gameObject.SetActive(true);
        }
    }
}
