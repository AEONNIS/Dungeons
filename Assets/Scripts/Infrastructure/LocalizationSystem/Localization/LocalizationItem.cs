using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class LocalizationItem
    {
        [JsonProperty("ID")]
        [SerializeField] private int _id;
        [SerializeField] private NamespaceTag _tag;
        [JsonProperty("Text")]
        [SerializeField] private string _text;
    }
}
