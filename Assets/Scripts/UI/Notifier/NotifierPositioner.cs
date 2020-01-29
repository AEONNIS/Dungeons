using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class NotifierPositioner : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _playerUIPanel;
        [SerializeField] private RectTransform _inventoruPanel;
        [SerializeField] private CanvasScaler _canvasScaler;

        private void Start()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _rectTransform.offsetMax = new Vector2(_rectTransform.rect.width, _playerUIPanel.offsetMin.y);
            _rectTransform.offsetMin = new Vector2(0.0f, -_canvasScaler.referenceResolution.x * Screen.height / Screen.width + _inventoruPanel.offsetMax.y);
        }
    }
}
