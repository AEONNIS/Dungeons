using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    [Serializable]
    public class LocalizationData
    {
        [SerializeField] private List<LocalizationItem> _items;

        public List<LocalizationItem> Items => _items;
    }
}
