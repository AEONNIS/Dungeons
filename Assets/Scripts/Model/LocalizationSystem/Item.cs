using System;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class Item
    {
        [SerializeField] private int _id;
        [SerializeField] private Tag _tags;
        [SerializeField] private string _value;

        public int ID { get { return _id; } set { _id = value; } }
    }
}
