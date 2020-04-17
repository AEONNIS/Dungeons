using Game.Model.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.UI.InventorySystem
{
    public class DragSlotPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [Tooltip("Сдвиг слота на экране (в процентах от его размера).")]
        [SerializeField] private Vector2 _shiftOnScreen = new Vector2(0.6f, -0.6f);

        public void Activate(RectTransform sourceSlot, Item item)
        {
            AlignTo(sourceSlot);
            Present(item.Sprite);
            gameObject.SetActive(true);
        }

        public void PlaceInMousePosition()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane);
            _rectTransform.position = _mainCamera.ScreenToWorldPoint(mousePosition + CalculateShiftOnScreen());
        }

        private Vector3 CalculateShiftOnScreen()
        {
            Vector2 _slotSizeOnScreen = _rectTransform.rect.size * Screen.width / _canvasScaler.referenceResolution.x;
            return new Vector3(_slotSizeOnScreen.x * _shiftOnScreen.x, _slotSizeOnScreen.y * _shiftOnScreen.y, 0.0f);
        }

        private void AlignTo(RectTransform sourceSlot)
        {
            _rectTransform.offsetMin = sourceSlot.offsetMin;
            _rectTransform.offsetMax = sourceSlot.offsetMax;
        }

        private void Present(Sprite item)
        {
            _image.sprite = item;
        }
    }
}
