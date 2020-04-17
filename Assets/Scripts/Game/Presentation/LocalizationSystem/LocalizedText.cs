using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.LocalizationSystem
{
    [Serializable]
    public class LocalizedText
    {
        [SerializeField] private string _key;
        [SerializeField] private Text _text;

        public string Key => _key;
        public Text Text => _text;
    }
}
