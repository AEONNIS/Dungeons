using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class Localization
    {
        [SerializeField] private string _language;
        [SerializeField] private List<Item> _items;
    }
}
