using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class Localization
    {
        [JsonProperty("Language")]
        [SerializeField] private string _language;
        [JsonProperty("Items")]
        [SerializeField] private List<LocalizationItem> _items;
    }
}
