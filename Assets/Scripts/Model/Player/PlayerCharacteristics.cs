using System;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public class PlayerCharacteristics
    {
        [SerializeField] private PowerState _powerState;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

        public PowerState PowerState => _powerState;
        public float Speed => _speed;
        public float JumpForce => _jumpForce;
    }
}
