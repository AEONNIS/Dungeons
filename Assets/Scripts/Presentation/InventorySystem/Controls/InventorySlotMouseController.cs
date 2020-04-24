using Dungeons.Model.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dungeons.Presentation.InventorySystem
{
    public class InventorySlotMouseController : SlotMouseController, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
                                                IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        private RectTransform _handsSlotRectTransform;
        private Slot _handsSlot;

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

        public Slot Init(Camera mainCamera, RectTransform inventoryRectTransform, Inventory inventory,
                                  DragSlotPresenter dragSlotPresenter, RectTransform handsSlotRectTransform, Slot handsSlot)
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
                _inventory.PullOutItemFrom(_slot);
            else if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, _mainCamera))
                _presenter.PointerEnterBacklight();
        }
    }
}
