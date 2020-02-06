using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "PlayerBase", menuName = "Model/PlayerBase")]
    public partial class PlayerBase : ScriptableObject
    {
        [SerializeField] private float _health = 100.0f;
        [SerializeField] private List<PlayerCharacteristics> _characteristicsSet;
        [Header("Parts from TotalForce:")]
        [SerializeField] private float _speedPartsFromTotalForce = 0.34f;
        [SerializeField] private float _jumpForcePartsFromTotalForce = 0.66f;
        [Header("Fall characteristics:")]
        [SerializeField] private float _maxSafeFalingSpeed;
        [SerializeField] private List<FallDamage> _fallDamages;

        public float Health => _health;

        #region Unity
        private void OnValidate()
        {
            ValidateTotalForceParts();
            ValidateHealth();
        }
        #endregion

        public bool FallingDamageAndCheckDeath(float impactSpeed)
        {
            return ToDamageAndCheckDeath(GetDamageForFallingSpeed(impactSpeed));
        }

        public float GetSpeedForPowerState(PowerState powerState)
        {
            return _characteristicsSet.First(characteristics => characteristics.PowerState == powerState).Speed;
        }

        public float GetJumpForceForPowerState(PowerState powerState)
        {
            return _characteristicsSet.First(characteristics => characteristics.PowerState == powerState).JumpForce;
        }

        public Vector2 GetTotalForceVectorForPowerState(PowerState powerState)
        {
            float totalForce = GetTotalForceValueForPowerState(powerState);
            return new Vector2(_speedPartsFromTotalForce * totalForce, _jumpForcePartsFromTotalForce * totalForce);
        }

        private void ValidateTotalForceParts()
        {
            if (_speedPartsFromTotalForce <= 0 || _speedPartsFromTotalForce >= 1)
                _speedPartsFromTotalForce = 0.34f;
            if (_jumpForcePartsFromTotalForce <= 0 || _jumpForcePartsFromTotalForce >= 1)
                _jumpForcePartsFromTotalForce = 0.66f;
            if (_speedPartsFromTotalForce + _jumpForcePartsFromTotalForce != 1)
                _speedPartsFromTotalForce = 1 - _jumpForcePartsFromTotalForce;
        }

        private void ValidateHealth()
        {
            if (0.0f > _health && _health > 100.0f)
                _health = 100.0f;
        }

        private float GetDamageForFallingSpeed(float impactSpeed)
        {
            return impactSpeed <= _maxSafeFalingSpeed ? 0.0f : _fallDamages.First(fallDamage => fallDamage.SpeedInRange(impactSpeed)).Damage;
        }

        private bool ToDamageAndCheckDeath(float damageValue)
        {
            _health = damageValue < _health ? _health - damageValue : 0.0f;
            return _health == 0.0f;
        }

        private float GetTotalForceValueForPowerState(PowerState powerState)
        {
            return GetSpeedForPowerState(PowerState.Low) + GetJumpForceForPowerState(powerState);
        }
    }
}
