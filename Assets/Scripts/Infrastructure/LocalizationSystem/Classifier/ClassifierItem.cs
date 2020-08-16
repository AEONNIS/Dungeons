using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class ClassifierItem
    {
        [JsonProperty("ID")]
        [SerializeField] private int _id;
        [JsonProperty("Tag")]
        [SerializeField] private NamespaceTag _tag;
    }
}
