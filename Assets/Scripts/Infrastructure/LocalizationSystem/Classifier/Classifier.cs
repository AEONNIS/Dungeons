using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class Classifier
    {
        [JsonProperty("Items")]
        [SerializeField] private List<ClassifierItem> _items = new List<ClassifierItem>();
    }
}
