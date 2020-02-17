using System;
using UnityEngine;

namespace Game.Model.PlayerCharacter
{
    public partial class PlayerBase : ScriptableObject
    {
        [Serializable]
        private class FallDamage
        {
            [SerializeField] private float _minFallingSpeed;
            [SerializeField] private float _maxFallingSpeed;
            [SerializeField] private float _damage;

            public float Damage => _damage;

            public bool SpeedInRange(float speed)
            {
                return _minFallingSpeed < speed && speed <= _maxFallingSpeed;
            }
        }
    }
}
