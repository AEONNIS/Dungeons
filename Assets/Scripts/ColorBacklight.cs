using System;
using UnityEngine;

namespace Game
{
    public class ColorBacklight : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private SmoothColorChanger _colorChanger;
        [Header("Colors:")]
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _playerCloseColor = new Color(0.0f, 1.0f, 0.53f);
        [SerializeField] private Color _playerFarColor = new Color(1.0f, 0.723f, 0.0f);
        [SerializeField] private Color _dealingDamageColor = new Color(1.0f, 0.0f, 0.0f);
        [Header("Durations:")]
        [SerializeField] private float _colorChangeDuration = 0.45f;
        [SerializeField] private float _colorBlinkingDuration = 0.15f;
        [SerializeField] private int _blinksCount = 3;

        private Action _blinks;

        #region Unity
        private void Awake()
        {
            _blinks = BlinksColorOnDealingDamage(_blinksCount);
        }
        #endregion

        public void WhenPointerHover(bool playerIsClose)
        {
            (playerIsClose ? (Action)WhenPlayerClose : WhenPlayerFar)();
        }

        public void ResetBacklight()
        {
            _colorChanger.StartColorChange(_renderer, _defaultColor, _colorChangeDuration);
        }

        public void BlinksColorOnDealingDamage()
        {
            _blinks.Invoke();
        }

        private void WhenPlayerClose()
        {
            _colorChanger.StartColorChange(_renderer, _playerCloseColor, _colorChangeDuration);
        }

        private void WhenPlayerFar()
        {
            _colorChanger.StartColorChange(_renderer, _playerFarColor, _colorChangeDuration);
        }

        private Action BlinksColorOnDealingDamage(int blinksCount)
        {
            Action blinks = () => BlinkColorOnceOnDealingDamage(null);

            for (int i = 0; i < blinksCount - 1; i++)
                blinks = () => BlinkColorOnceOnDealingDamage(blinks);

            return blinks;
        }

        private void BlinkColorOnceOnDealingDamage(Action onEnd = null)
        {
            _colorChanger.StartColorChange(_renderer, _dealingDamageColor, _colorBlinkingDuration * 0.5f, () =>
              {
                  _colorChanger.StartColorChange(_renderer, _defaultColor, _colorBlinkingDuration * 0.5f, onEnd);
              });
        }
    }
}
