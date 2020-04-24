using System;
using UnityEngine;

namespace Dungeons.Model.InteractionSystem
{
    [Serializable]
    public class Damage
    {
        [SerializeField] private float _item;
        [SerializeField] private float _tile;

        public float Item => _item;
        public float Tile => _tile;
    }
}
