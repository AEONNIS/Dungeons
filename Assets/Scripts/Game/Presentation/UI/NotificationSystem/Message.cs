using Game.Infrastructure;
using Game.Infrastructure.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.UI.NotificationSystem
{
    public class Message : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private RectTransformStretcher _textStretcher;
        [SerializeField] private Image _icon;
        [SerializeField] private Timer _timer;
        [SerializeField] private float _defaultDisplayTime = 5.0f;

        private Notifier _notifier;

        public void Init(Notifier notifier)
        {
            _textStretcher.Init();
            _notifier = notifier;
            gameObject.SetActive(false);
        }

        public void Display(string text, Sprite icon = null, float displayTime = 0)
        {
            _text.text = text;
            Set(icon);
            gameObject.SetActive(true);
            _timer.StartOff(displayTime > 0 ? displayTime : _defaultDisplayTime, OnEndMessageDisplay);
        }

        private void Set(Sprite icon)
        {
            if (icon != null)
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(true);
                _textStretcher.StretchToInitialRect();
            }
            else
            {
                _icon.gameObject.SetActive(false);
                _textStretcher.StretchToBasis();
            }
        }

        private void OnEndMessageDisplay()
        {
            _notifier.ReturnToPool(this);
            gameObject.SetActive(false);
        }
    }
}
