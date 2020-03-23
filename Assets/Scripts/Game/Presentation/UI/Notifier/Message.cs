using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Message : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        [SerializeField] private Timer _timer;
        [SerializeField] private float _defaultDisplayTime = 5.0f;

        private Notifier _notifier;

        public void Init(Notifier notifier)
        {
            _notifier = notifier;
            gameObject.SetActive(false);
        }

        public void Display(string messageText, Sprite messageImage = null, float displayTime = 0)
        {
            if (messageImage == null)
            {
                _image.gameObject.SetActive(false);
            }
            else
            {
                _image.sprite = messageImage;
                _image.gameObject.SetActive(true);
            }

            displayTime = displayTime <= 0 ? _defaultDisplayTime : displayTime;
            _text.text = messageText;
            gameObject.SetActive(true);
            _timer.StartTimer(displayTime, OnEndMessageDisplay);
        }

        private void OnEndMessageDisplay()
        {
            gameObject.SetActive(false);
            _notifier.ReturnMessageInPool(this);
        }
    }
}
