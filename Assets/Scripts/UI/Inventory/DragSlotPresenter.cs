using Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.InventorySystem
{
    public class DragSlotPresenter : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private float _partSizeForShiftOnScreen = 0.6f;
        [SerializeField] private Camera _mainCamera;

        private Vector3 _shiftOnScren;

        #region Unity
        private void Awake()
        {
            _shiftOnScren = CalculateShiftOnScreen();
        }
        #endregion

        public void Activate(RectTransform sourceSlotRectTransform, Item sourceSlotItem)
        {
            AlignTo(sourceSlotRectTransform);
            Present(sourceSlotItem.Base.InventorySprite);
            gameObject.SetActive(true);
        }

        public void PlaceInMousePosition()
        {
            _rectTransform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition + _shiftOnScren);
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
