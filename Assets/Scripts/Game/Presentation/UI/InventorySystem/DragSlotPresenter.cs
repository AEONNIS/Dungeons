using Game.Model.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.UI.InventorySystem
{
    public class DragSlotPresenter : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private float _partSizeForShiftOnScreen = 0.6f;
        [SerializeField] private Camera _mainCamera;

        private Vector3 _shiftOnScren;

        public void Activate(RectTransform sourceSlotRectTransform, Item sourceSlotItem)
        {
            AlignTo(sourceSlotRectTransform);
            _shiftOnScren = CalculateShiftOnScreen();
            Present(sourceSlotItem.Sprite);
            gameObject.SetActive(true);
        }

        public void PlaceInMousePosition()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane);
            _rectTransform.position = _mainCamera.ScreenToWorldPoint(mousePosition + _shiftOnScren);
        }

        private Vector3 CalculateShiftOnScreen()
        {
            Vector2 _screenSize = (Screen.width / _canvasScaler.referenceResolution.x) * _rectTransform.rect.size;
            return new Vector3(_partSizeForShiftOnScreen * _screenSize.x, -_partSizeForShiftOnScreen * _screenSize.y, 0.0f);
        }

        private void AlignTo(RectTransform rectTransform)
        {
            _rectTransform.position = rectTransform.position;
            _rectTransform.offsetMin = rectTransform.offsetMin;
            _rectTransform.offsetMax = rectTransform.offsetMax;
        }

        private void Present(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}
