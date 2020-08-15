using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class Classifier
    {
        [SerializeField] private List<ClassifierItem> _items = new List<ClassifierItem>();
    }
}
