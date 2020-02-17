using Game.UI;
using UnityEngine;

namespace Game.Model
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemBase _base;
        [SerializeField] private float _strength;
        [SerializeField] private Notifier _notifier;

        private bool _warningShown;

        public ItemBase Base => _base;

        public void ToDamage(float damageValue)
        {
            _strength = damageValue <= _strength ? _strength - damageValue : 0.0f;
        }

        public void SetStateAccordingToStrength(float strength)
        {

        }
    }
}
