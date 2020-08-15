using System;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class ClassifierItem
    {
        [SerializeField] private int _id;
        [SerializeField] private NamespaceTag _tag;
    }
}
