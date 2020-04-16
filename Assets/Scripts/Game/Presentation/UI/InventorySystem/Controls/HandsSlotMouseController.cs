using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Presentation.UI.InventorySystem.Controls
{
    public class HandsSlotMouseController : SlotMouseController, IPointerEnterHandler, IPointerExitHandler,
                                            IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        #region Unity
        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEntered();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExited();
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
            if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, _mainCamera))
                _presenter.PointerEnterBacklight();
            else if (RectTransformUtility.RectangleContainsScreenPoint(_inventoryRectTransform, Input.mousePosition, _mainCamera) == false)
                _inventory.ThrowItemFromSlot(_slot);
        }
    }
}
