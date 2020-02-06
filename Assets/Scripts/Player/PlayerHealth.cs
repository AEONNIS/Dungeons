using UnityEngine;

namespace Game.Model
{
    public class PlayerHealth
    {
        [SerializeField] private float _health = 100.0f;

        public float Health
        {
            get { return _health; }
            set { _health = 0.0f <= value && value <= 100.0f ? value : _health; }
        }

        public bool ToDamageAndCheckDeath(float damageValue)
        {
            _health = damageValue < _health ? _health - damageValue : 0.0f;
            return _health == 0.0f;
        }
    }
}
