using Game.Model.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Presentation.UI.InventorySystem.Controls
{
    public class InventorySlotMouseController : SlotMouseController, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
                                                IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        private RectTransform _handsSlotRectTransform;
        private InventorySlot _handsSlot;

        #region Unity
        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEntered();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExited();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
                _inventory.SwapItemsInSlots(_slot, _handsSlot);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DraggingBegan();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Dragged();
        }

        public void OnDrop(PointerEventData eventData)
        {
            Dropped(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (DraggingEnded())
                FinishDragging();
        }
        #endregion

        public InventorySlot Init(Camera mainCamera, RectTransform inventoryRectTransform, Inventory inventory,
                                  DragSlotPresenter dragSlotPresenter, RectTransform handsSlotRectTransform, InventorySlot handsSlot)
        {
            _mainCamera = mainCamera;
            _inventoryRectTransform = inventoryRectTransform;
            _inventory = inventory;
            _dragSlotPresenter = dragSlotPresenter;
            _handsSlotRectTransform = handsSlotRectTransform;
            _handsSlot = handsSlot;

            return _slot;
        }

        private void FinishDragging()
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_inventoryRectTransform, Input.mousePosition, _mainCamera) == false &&
                RectTransformUtility.RectangleContainsScreenPoint(_handsSlotRectTransform, Input.mousePosition, _mainCamera) == false)
                _inventory.ThrowItemFromSlot(_slot);
            else if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, _mainCamera))
                _presenter.PointerEnterBacklight();
        }
    }
}
