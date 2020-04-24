using Dungeons.Presentation.NotificationSystem;
using UnityEngine;

namespace Dungeons.Model.Items
{
    public class Item : MonoBehaviour, IInfoElement
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private ItemPlace _place;
        [SerializeField] private float _strength;
        [SerializeField] private Notifier _notifier;

        public Sprite Sprite => _data.GetPlaceState(_place).Sprite;
        public string Name => _data.Name;
        public string Description => _data.Description;
        public float Strength => _strength;
        public ItemType Type => _data.Type;

        public void PlaceIn(ItemPlace place)
        {
            _place = place;
            gameObject.SetActive(_data.GetPlaceState(place).IsActiveOnScene);
        }

        public void ToDamage(float damageValue)
        {
            _strength = damageValue <= _strength ? _strength - damageValue : 0.0f;
            ItemStrengthState strengthState = _data.GetStrengthState(_strength);

            if (strengthState.NotifierMessage.Length > 0)
                _notifier.ShowMessage(strengthState.NotifierMessage, Sprite);
        }
    }
}
