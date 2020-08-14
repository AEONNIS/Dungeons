using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Model.LocalizationSystemOLD1
{
    [Serializable]
    public class LocalizationItems : Data
    {
        [SerializeField] private List<LocalizationItem> _items = new List<LocalizationItem>();

        public IReadOnlyList<LocalizationItem> Items => _items;

        public void AddItem()
        {
            _items.Add(new LocalizationItem());
        }
    }
}
