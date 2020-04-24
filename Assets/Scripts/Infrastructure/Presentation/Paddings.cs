using System;
using UnityEngine;

namespace Dungeons.Infrastructure.Presentation
{
    [Serializable]
    public struct Paddings
    {
        [SerializeField] private float _left;
        [SerializeField] private float _right;
        [SerializeField] private float _top;
        [SerializeField] private float _bottom;

        public Paddings(float left, float right, float top, float bottom)
        {
            _left = left;
            _right = right;
            _top = top;
            _bottom = bottom;
        }

        public float Left => _left;
        public float Right => _right;
        public float Top => _top;
        public float Bottom => _bottom;
        public Vector2 LeftTop => new Vector2(_left, _top);
        public Vector2 RightBottom => new Vector2(_right, _bottom);
    }
}
