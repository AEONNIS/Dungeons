using Game.Operations.LocalizationSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation.LocalizationSystem
{
    public class LocalizedContent : MonoBehaviour
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private List<LocalizedText> _content;

        public void SetLocalizedValue()
        {
            _content.ForEach(text => text.Text.text = _localizer.GetLocalizedValue(text.Key));
        }
    }
}
