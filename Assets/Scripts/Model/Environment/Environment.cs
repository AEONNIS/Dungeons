using UnityEngine;

namespace Game.Model
{
    public partial class Environment : MonoBehaviour, IInfoElement
    {
        [SerializeField] private EnvironmentBase _base;
        [SerializeField] private float _strength;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Sprite Sprite => _renderer.sprite;
        public string Name => _base.Name;
        public string Description => _base.Description;
        public float Strength => _strength;

        public void ToDamage(float damageValue)
        {
            _strength = damageValue <= _strength ? _strength - damageValue : 0.0f;
            SetStateDependingOnStrength(_base.GetStrengthState(_strength));

            if (_strength <= 0)
                Destroy(gameObject, _base.ShutdownTimeWhenDestroyed);
        }

        private void SetStateDependingOnStrength(EnvironmentStrengthState strengthState)
        {
            _renderer.sprite = strengthState.Sprite;
            _rigidbody2D.bodyType = strengthState.BodyType;
            gameObject.layer = strengthState.Layer;
        }
    }
}
