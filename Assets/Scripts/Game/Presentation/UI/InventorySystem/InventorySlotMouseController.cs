using Game.Model.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.InventorySystem
{
    public class InventorySlotMouseController : SlotMouseController, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
                                                IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private RectTransform _handsSlotRectTransform;
        [SerializeField] private InventorySlot _handsSlot;

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
