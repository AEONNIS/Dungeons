using System;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public struct FloatRange
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _minValue;

        public float MaxValue => _maxValue;
        public float MinValue => _minValue;
    }
}
