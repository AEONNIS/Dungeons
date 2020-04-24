using Dungeons.Attributes;
using Dungeons.Infrastructure;
using System;
using UnityEngine;

namespace Dungeons.Model.Tiles
{
    [Serializable]
    public struct TileStrengthState
    {
        [SerializeField] private FloatRange _strengthRange;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private RigidbodyType2D _bodyType;
        [Layer]
        [SerializeField] private int _layer;

        public Sprite Sprite => _sprite;
        public RigidbodyType2D BodyType => _bodyType;
        public int Layer => _layer;

        public bool StrengthIsInRangeMinExclusive(float strength)
        {
            return _strengthRange.MinValue < strength && strength <= _strengthRange.MaxValue;
        }
    }
}
