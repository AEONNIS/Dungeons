using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Model.LocalizationSystemOLD2
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

        public void AddItemToList() => _items.Add(new LocalizationItem());

        public Dictionary<LocalizationTextID, string> GetDictionary()
        {
            Dictionary<LocalizationTextID, string> dictionary = new Dictionary<LocalizationTextID, string>(_items.Capacity);
            _items.ForEach(item => dictionary.Add(item.Key, item.Value));
            return dictionary;
        }
    }
}
