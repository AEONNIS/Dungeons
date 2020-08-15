using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class LocalizationFile
    {
        [SerializeField] private string _language;
        [SerializeField] private List<LocalizationFileItem> _items;
    }
}
