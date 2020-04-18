using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    [Serializable]
    public class LocalizationData
    {
        [SerializeField] private string _languageDesignation;
        [SerializeField] private List<LocalizationItem> _items;

        public LocalizationData(string languageDesignation)
        {
            _languageDesignation = languageDesignation;
            _items = new List<LocalizationItem>();
        }

        public string LanguageDesignation => _languageDesignation;
        public List<LocalizationItem> Items => _items;
    }
}
