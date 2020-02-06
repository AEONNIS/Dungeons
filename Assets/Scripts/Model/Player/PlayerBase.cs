using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "PlayerBase", menuName = "Model/PlayerBase")]
    public class PlayerBase : ScriptableObject
    {
        [SerializeField] private List<PlayerCharacteristics> _characteristicsSet;
        [SerializeField] private float _partForSpeedFromTotalForce = 0.34f;
        [SerializeField] private float _partForJumpForceFromTotalForce = 0.66f;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private float _maxSafeFalingSpeed;
        [SerializeField] private List<FallDamage> _fallDamages;

        public float Health => _playerHealth.Health;

        #region Unity
        private void OnValidate()
        {
            if (_partForSpeedFromTotalForce <= 0 || _partForSpeedFromTotalForce >= 1)
                _partForSpeedFromTotalForce = 0.34f;
            if (_partForJumpForceFromTotalForce <= 0 || _partForJumpForceFromTotalForce >= 1)
                _partForJumpForceFromTotalForce = 0.66f;
            if (_partForSpeedFromTotalForce + _partForJumpForceFromTotalForce != 1)
                _partForSpeedFromTotalForce = 1 - _partForJumpForceFromTotalForce;
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
            return new Vector2(_partForSpeedFromTotalForce * totalForce, _partForJumpForceFromTotalForce * totalForce);
        }

        public bool FallingDamageAndCheckDeath(float impactSpeed)
        {
            return _playerHealth.ToDamageAndCheckDeath(GetDamageForFallingSpeed(impactSpeed));
        }

        private float GetDamageForFallingSpeed(float impactSpeed)
        {
            return impactSpeed <= _maxSafeFalingSpeed ? 0.0f : _fallDamages.First(fallDamage => fallDamage.SpeedInRange(impactSpeed)).Damage;
        }

        private float GetTotalForceValueForPowerState(PowerState powerState)
        {
            return GetSpeedForPowerState(PowerState.Low) + GetJumpForceForPowerState(powerState);
        }
    }
}
