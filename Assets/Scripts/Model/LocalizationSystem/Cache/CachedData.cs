using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Model.LocalizationSystem
{
    [Serializable]
    public class CachedData
    {
        [SerializeField] private List<CachedItem> _item;

        public string GetValue(int index) => _item[index].Value;

        public void ToCache(Localizer localizer)
        {
            _item.ForEach(item => item.ToCache(localizer));
        }
    }
}
