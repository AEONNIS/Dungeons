using Game.Model.InventorySystem;
using Game.Presentation.UI.InventorySystem.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation.UI.InventorySystem
{
    public class SlotsCreater : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private RectTransform _inventoryRectTransform;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private RectTransform _handsSlotRectTransform;
        [SerializeField] private InventorySlot _handsSlot;
        [SerializeField] private DragSlotPresenter _dragSlotPresenter;
        [SerializeField] private int _slotsNumber = 16;
        [SerializeField] private InventorySlotMouseController _slotTemplate;
        [SerializeField] private RectTransform _slotsContainer;

        #region Unity
        private void Awake()
        {
            _inventory.Init(CreateSlots());
        }
        #endregion

        private List<InventorySlot> CreateSlots()
        {
            List<InventorySlot> slots = new List<InventorySlot>(_slotsNumber);

            for (int i = 0; i < _slotsNumber; i++)
            {
                InventorySlotMouseController slotController = Instantiate(_slotTemplate, _slotsContainer);
                slots.Add(slotController.Init(_mainCamera, _inventoryRectTransform, _inventory, _dragSlotPresenter, _handsSlotRectTransform, _handsSlot));
            }

            return slots;
        }
    }
}
