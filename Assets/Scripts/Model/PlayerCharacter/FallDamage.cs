using System;
using UnityEngine;

namespace Dungeons.Model.PlayerCharacter
{
    public partial class PlayerData : ScriptableObject
    {
        [Serializable]
        private class FallDamage
        {
            [SerializeField] private float _minFallingSpeed;
            [SerializeField] private float _maxFallingSpeed;
            [SerializeField] private float _damage;

            public float Damage => _damage;

            public bool SpeedIsInRangeMinExclusive(float speed)
            {
                return _minFallingSpeed < speed && speed <= _maxFallingSpeed;
            }
        }
    }
}
