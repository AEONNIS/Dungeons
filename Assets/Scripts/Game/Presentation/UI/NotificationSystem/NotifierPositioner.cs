using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.UI.NotificationSystem
{
    public class NotifierPositioner : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _playerPanel;
        [SerializeField] private RectTransform _inventoryPanel;

        #region Unity
        private void Start()
        {
            UpdatePosition();
        }
        #endregion

        private void UpdatePosition()
        {
            _rectTransform.offsetMax = new Vector2(_rectTransform.rect.width, _playerPanel.offsetMin.y);
            _rectTransform.offsetMin = new Vector2(0.0f, -_canvasScaler.referenceResolution.x * Screen.height / Screen.width + _inventoryPanel.offsetMax.y);
        }
    }
}
