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
        [SerializeField] private LocalizationTextID _key;

        public void SetText(LocalizationTextID key = LocalizationTextID.Null)
        {
            _text.text = key == LocalizationTextID.Null ? _localizer.GetLocalizedText(_key) : _localizer.GetLocalizedText(_key = key);
        }
    }
}
