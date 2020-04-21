using System;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    [Serializable]
    public class LocalizationItem
    {
        [SerializeField] private LocalizationTextID _key;
        [TextArea(1, 10)]
        [SerializeField] private string _value;

        public LocalizationTextID Key => _key;
        public string Value => _value;
    }
}
