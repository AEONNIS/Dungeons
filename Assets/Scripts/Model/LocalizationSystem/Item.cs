using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class Item
    {
        [SerializeField] private int _id;
        [SerializeField] private NamespaceTag _namespaces;
        [SerializeField] private string _text;
    }

    [Serializable]
    public class ClassifierItem
    {
        [SerializeField] private int _id;
        [SerializeField] private NamespaceTag _tag;
    }

    [Serializable]
    public class Classifier
    {
        [SerializeField] private List<ClassifierItem> _items;
    }
}
