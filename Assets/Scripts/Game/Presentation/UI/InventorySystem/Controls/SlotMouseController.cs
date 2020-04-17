using Game.Model.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Presentation.UI.InventorySystem.Controls
{
    public class SlotMouseController : MonoBehaviour
    {
        [SerializeField] private protected Camera _mainCamera;
        [SerializeField] private protected RectTransform _inventoryRectTransform;
        [SerializeField] private protected RectTransform _rectTransform;
        [SerializeField] private protected Inventory _inventory;
        [SerializeField] private protected Slot _slot;
        [SerializeField] private protected SlotPresenter _presenter;
        [SerializeField] private protected DragSlotPresenter _dragSlotPresenter;

        private bool _dragging;

        private protected void PointerEntered()
        {
            if (_dragging == false)
                _presenter.PointerEnterBacklight();
        }

        private protected void PointerExited()
        {
            if (_dragging == false)
                _presenter.ResetBacklight();
        }

        private protected void DraggingBegan()
        {
            if (_slot.Item != null)
            {
                _dragging = true;
                _dragSlotPresenter.Activate(_rectTransform, _slot.Item);
                _presenter.DraggingBacklight();
            }
        }

        private protected void Dragged()
        {
            if (_dragging)
                _dragSlotPresenter.PlaceInMousePosition();
        }

        private protected void Dropped(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out Slot dragSlot) && dragSlot.Item != null)
                _inventory.SwapItemsInSlots(dragSlot, _slot);
        }

        private protected bool DraggingEnded()
        {
            if (_dragging)
            {
                _dragging = false;
                _dragSlotPresenter.gameObject.SetActive(false);
                _presenter.ResetBacklight();
                return true;
            }

            return false;
        }
    }
}