using System;
using UnityEngine;

namespace Game.Model
{
    public class Environment : MonoBehaviour, IInfoElement
    {
        [SerializeField] private EnvironmentBase _base;
        [SerializeField] private float _strength;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _destroyedGroundLayer;
        [SerializeField] private Timer _timer;

        public Sprite Sprite => _spriteRenderer.sprite;
        public string Name => _base.Name;
        public string Description => _base.Description;
        public float Strength => _strength;

        public void ToDamage(float damageValue)
        {
            _strength = damageValue <= _strength ? _strength - damageValue : 0.0f;
            SetStateAccordingToStrength(_strength);
        }

        private void SetStateAccordingToStrength(float strength)
        {
            if (strength >= _base.MinStrengthForNoDamage)
                SetState(_base.DefaultSprite, _groundLayer.value, RigidbodyType2D.Static);
            else if (strength >= _base.MinStrengthForStatic)
                SetState(_base.DamagedSprite, _groundLayer.value, RigidbodyType2D.Static);
            else if (strength > 0.0f)
                SetState(_base.DamagedSprite, _groundLayer.value, RigidbodyType2D.Dynamic);
            else
                SetState(_base.DestroyedSprite, _destroyedGroundLayer.value, RigidbodyType2D.Dynamic,
                         () => _timer.StartTimer(_base.ShutdownTimeWhenDestroyed, () => gameObject.SetActive(false)));
        }

        private void SetState(Sprite sprite, int layer, RigidbodyType2D bodyType, Action action = null)
        {
            _spriteRenderer.sprite = sprite;
            gameObject.layer = layer;
            _rigidbody2D.bodyType = bodyType;
            action?.Invoke();
        }
    }
}
