using UnityEngine;
using System;
using Game.Model.Tiles;
using Game.Model.Items;

namespace Game.Model.InteractionSystem
{
    [Serializable]
    public class InteractionCouple
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private EnvironmentType _environmentType;

        public ItemType ItemType => _itemType;
        public EnvironmentType EnvironmentType => _environmentType;
    }
}
