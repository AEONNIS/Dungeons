using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.PlayerCharacter
{
    [CreateAssetMenu(fileName = "PlayerBase", menuName = "Model/PlayerBase")]
    public partial class PlayerBase : ScriptableObject
    {
        [SerializeField] private List<PlayerCharacteristics> _characteristicsSet;
        [Header("Parts from TotalForce:")]
        [SerializeField] private float _speedPartsFromTotalForce = 0.34f;
        [SerializeField] private float _jumpForcePartsFromTotalForce = 0.66f;
        [Header("Fall characteristics:")]
        [SerializeField] private float _maxSafeFalingSpeed;
        [SerializeField] private List<FallDamage> _fallDamages;

        #region Unity
        private void OnValidate()
        {
            ValidateTotalForceParts();
        }
        #endregion

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

        public float GetDamageForFallingSpeed(float impactSpeed)
        {
            return impactSpeed <= _maxSafeFalingSpeed ? 0.0f : _fallDamages.First(fallDamage => fallDamage.SpeedIsInRangeMinExclusive(impactSpeed)).Damage;
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

        private float GetTotalForceValueForPowerState(PowerState powerState)
        {
            return GetSpeedForPowerState(PowerState.Low) + GetJumpForceForPowerState(powerState);
        }
    }
}
