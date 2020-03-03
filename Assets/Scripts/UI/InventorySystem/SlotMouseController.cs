using Game.Model.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.InventorySystem
{
    public class SlotMouseController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
                                       IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private InventorySlot _slot;
        [SerializeField] private SlotPresenter _presenter;
        [SerializeField] private DragSlotPresenter _dragSlotPresenter;
        [SerializeField] private Model.InventorySystem.Inventory _inventory;
        [SerializeField] private RectTransform _inventoryRectTransform;

        private bool _dragging;

        #region Unity
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_dragging == false)
                _presenter.PointerEnterBacklight();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_dragging == false)
                _presenter.ResetBacklight();
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_slot.Item != null)
                BeginDragging();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragging)
                _dragSlotPresenter.PlaceInMousePosition();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragging)
                FinishDragging();
        }

        public void OnDrop(PointerEventData eventData)
        {
            SwapItems(eventData);
        }
        #endregion

        private void BeginDragging()
        {
            _dragging = true;
            _dragSlotPresenter.Activate(_rectTransform, _slot.Item);
            _presenter.DraggingBacklight();
        }

        private void FinishDragging()
        {
            _dragging = false;
            _dragSlotPresenter.gameObject.SetActive(false);

            if (RectTransformUtility.RectangleContainsScreenPoint(_inventoryRectTransform, Input.mousePosition) == false)
            {
                _inventory.ThrowItemFromSlot(_slot);
                _presenter.ResetBacklight();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition))
            {
                _presenter.PointerEnterBacklight();
            }
        }

        private void SwapItems(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out InventorySlot slot))
                _inventory.SwapItemsInSlots(_slot, slot);
        }
    }
}
