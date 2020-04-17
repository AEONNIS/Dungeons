using System;
using UnityEngine;

namespace Game.Operations.LocalizationSystem
{
    [Serializable]
    public class LocalizationItem
    {
        [SerializeField] private string _key;
        [SerializeField] private string _value;

        public string Key => _key;
        public string Value => _value;
    }
}
