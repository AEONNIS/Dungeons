using System;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class LocalizationDataItem
    {
        [SerializeField] private int _id;
        [SerializeField] private NamespaceTag _tag;
        [SerializeField] private string _text;
    }
}
