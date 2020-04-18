using Game.Operations.LocalizationSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.LocalizationSystem
{
    [Serializable]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private Text _text;
        [SerializeField] private string _key;

        public void SetText(string key = null)
        {
            _text.text = string.IsNullOrWhiteSpace(key) ? _localizer.GetLocalizedText(_key) : _localizer.GetLocalizedText(_key = key);
        }
    }
}
