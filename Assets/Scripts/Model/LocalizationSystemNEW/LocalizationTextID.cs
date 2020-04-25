using System;
using UnityEngine;

namespace Dungeons.Model.LocalizationSystemNEW
{
    [Serializable]
    public class LocalizationTextID
    {
        [SerializeField] private int _index;
        [SerializeField] private string _name;

        public int Index => _index;
        public string Name => _name;
    }
}
