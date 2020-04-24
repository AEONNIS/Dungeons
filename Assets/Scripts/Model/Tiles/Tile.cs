using UnityEngine;

namespace Dungeons.Model.Tiles
{
    public partial class Tile : MonoBehaviour, IInfoElement
    {
        [SerializeField] private TileData _data;
        [SerializeField] private float _strength;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Sprite Sprite => _renderer.sprite;
        public string Name => _data.Name;
        public string Description => _data.Description;
        public float Strength => _strength;
        public TileType Type => _data.Type;

        public void ToDamage(float damageValue)
        {
            _strength = damageValue <= _strength ? _strength - damageValue : 0.0f;
            SetStateDependingOnStrength(_data.GetStrengthState(_strength));

            if (_strength <= 0)
                Destroy(gameObject, _data.ShutdownTimeWhenDestroyed);
        }

        private void SetStateDependingOnStrength(TileStrengthState strengthState)
        {
            _renderer.sprite = strengthState.Sprite;
            _rigidbody2D.bodyType = strengthState.BodyType;
            gameObject.layer = strengthState.Layer;
        }
    }
}
