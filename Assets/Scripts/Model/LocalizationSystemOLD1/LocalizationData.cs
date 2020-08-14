using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Model.LocalizationSystemOLD1
{
    [Serializable]
    public class LocalizationData : Data
    {
        [SerializeField] private List<Data> _data = new List<Data>();

        public IReadOnlyList<Data> Data => _data;

        public void AddItems()
        {
            _data.Add(new LocalizationItems());
        }

        public void AddData()
        {
            _data.Add(new LocalizationData());
        }
    }
}
