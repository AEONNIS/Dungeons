using Dungeons.Model.Items;
using Dungeons.Model.Tiles;
using System;
using UnityEngine;

namespace Dungeons.Model.InteractionSystem
{
    [Serializable]
    public class InteractionCouple
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private TileType _tileType;

        public ItemType ItemType => _itemType;
        public TileType TileType => _tileType;
    }
}
