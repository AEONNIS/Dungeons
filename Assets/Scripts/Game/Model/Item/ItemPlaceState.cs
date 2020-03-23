using System;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public struct ItemPlaceState
    {
        [SerializeField] private ItemPlace _place;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _isActiveOnScene;

        public ItemPlace Place => _place;
        public Sprite Sprite => _sprite;
        public bool IsActiveOnScene => _isActiveOnScene;
    }

    public enum ItemPlace { Scene, Inventory }
}
