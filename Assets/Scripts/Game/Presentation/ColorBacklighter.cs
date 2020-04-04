using Game.Infrastructure.Presentation;
using System;
using UnityEngine;

namespace Game.Presentation
{
    public class ColorBacklighter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private SmoothColorChanger _colorChanger;
        [Header("Colors:")]
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _primaryBacklightColor = new Color(0.0f, 1.0f, 0.55f);
        [SerializeField] private Color _secondaryBacklightColor = new Color(1.0f, 0.725f, 0.0f);
        [SerializeField] private Color _flashingColor = new Color(1.0f, 0.0f, 0.0f);
        [Header("Durations:")]
        [SerializeField] private float _colorChangeDuration = 0.5f;
        [SerializeField] private float _flashingDuration = 0.15f;
        [SerializeField] private int _flashesNumber = 1;

        private Color _currentEndingColor;

        public void Backlight(bool primaryColor)
        {
            _currentEndingColor = primaryColor ? _primaryBacklightColor : _secondaryBacklightColor;
            _colorChanger.Begin(_renderer, _currentEndingColor, _colorChangeDuration);
        }

        public void ResetSmoothly()
        {
            _colorChanger.Begin(_renderer, _currentEndingColor = _defaultColor, _colorChangeDuration);
        }

        public void ResetImmediately()
        {
            _renderer.color = _currentEndingColor = _defaultColor;
        }

        public void ToFlash()
        {
            ToFlash(_flashesNumber, _currentEndingColor);
        }

        private void ToFlash(int flashesNumber, Color endingColor)
        {
            Action allFlash = () => ToFlashOnce(_flashingColor, endingColor);

            for (int i = 0; i < flashesNumber - 1; i++)
            {
                var flash = allFlash;
                allFlash = () => ToFlashOnce(_flashingColor, endingColor, flash);
            }

            allFlash.Invoke();
        }

        private void ToFlashOnce(Color flashingColor, Color endingColor, Action onEnd = null)
        {
            _colorChanger.Begin(_renderer, flashingColor, _flashingDuration * 0.5f,
                                () => _colorChanger.Begin(_renderer, endingColor, _flashingDuration * 0.5f, onEnd));
        }
    }
}
