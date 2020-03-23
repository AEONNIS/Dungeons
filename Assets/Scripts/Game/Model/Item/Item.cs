using Game.UI;
using UnityEngine;

namespace Game.Model
{
    public class Item : MonoBehaviour, IInfoElement
    {
        [SerializeField] private ItemBase _base;
        [SerializeField] private ItemPlace _place;
        [SerializeField] private float _strength;
        [SerializeField] private Notifier _notifier;

        public Sprite Sprite => _base.GetPlaceState(_place).Sprite;
        public string Name => _base.Name;
        public string Description => _base.Description;
        public float Strength => _strength;
        public ItemType Type => _base.Type;

        public void PlaceIn(ItemPlace place)
        {
            _place = place;
            gameObject.SetActive(_base.GetPlaceState(place).IsActiveOnScene);
        }

        public void ToDamage(float damageValue)
        {
            _strength = damageValue <= _strength ? _strength - damageValue : 0.0f;
            ItemStrengthState strengthState = _base.GetStrengthState(_strength);

            if (strengthState.NotifierMessage.Length > 0)
                _notifier.ShowMessage(strengthState.NotifierMessage, Sprite);
        }
    }
}
