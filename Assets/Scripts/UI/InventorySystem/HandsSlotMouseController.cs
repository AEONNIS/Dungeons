using Game.Model.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.InventorySystem
{
    public class HandsSlotMouseController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
                                            IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private RectTransform _inventoryRectTransform;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventorySlot _slot;
        [SerializeField] private SlotPresenter _presenter;
        [SerializeField] private DragSlotPresenter _dragSlotPresenter;

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

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out InventorySlot dragSlot) && dragSlot.Item != null)
                _inventory.SwapItemsInSlots(dragSlot, _slot);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragging)
                FinishDragging();
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
            _presenter.ResetBacklight();

            if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, _mainCamera))
                _presenter.PointerEnterBacklight();
            else if (RectTransformUtility.RectangleContainsScreenPoint(_inventoryRectTransform, Input.mousePosition, _mainCamera) == false)
                _inventory.ThrowItemFromSlot(_slot);
        }
    }
}
