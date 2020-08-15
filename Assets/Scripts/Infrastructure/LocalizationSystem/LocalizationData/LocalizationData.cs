using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class LocalizationData
    {
        [SerializeField] private string _language;
        [SerializeField] private List<LocalizationDataItem> _items;
    }
}
