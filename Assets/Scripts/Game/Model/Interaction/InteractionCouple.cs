using UnityEngine;
using System;

namespace Game.Model
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
