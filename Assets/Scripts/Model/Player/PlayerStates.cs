using System;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public struct PlayerStates
    {
        private static int _powerStatesLength = Enum.GetNames(typeof(PowerState)).Length;

        [SerializeField] private bool _rightDirection;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isJumping;
        [SerializeField] private bool _isHorizontalInput;
        [SerializeField] private bool _isHorizontalSpeed;
        [SerializeField] private bool _isFun;
        [SerializeField] private bool _died;
        [SerializeField] private PowerState _powerState;

        public bool RightDirection => _rightDirection;
        public bool IsGrounded => _isGrounded;
        public bool IsJumping => _isJumping;
        public bool IsHorizontalInput => _isHorizontalInput;
        public bool IsHorizontalSpeed => _isHorizontalSpeed;
        public bool IsFun => _isFun;
        public bool Died => _died;
        public PowerState PowerState => _powerState;

        public void SetDefault()
        {
            _rightDirection = true;
            _isGrounded = true;
            _isJumping = false;
            _isHorizontalInput = false;
            _isHorizontalSpeed = false;
            _isFun = false;
            _died = false;
            _powerState = PowerState.Normal;
        }

        public void SwitchPowerState()
        {
            _powerState = (int)_powerState == _powerStatesLength - 1 ? 0 : ++_powerState;
        }

        public void SetMoveState(bool rightDirection)
        {
            _rightDirection = rightDirection;
            _isGrounded = true;
            _isJumping = false;
            _isHorizontalInput = true;
            _isHorizontalSpeed = true;
        }

        public void SetStateToSlowdownOrIdle(bool isHorizontalSpeed)
        {
            _isGrounded = true;
            _isJumping = false;
            _isHorizontalInput = false;
            _isHorizontalSpeed = isHorizontalSpeed;
        }

        public void SetJumpState()
        {
            _isGrounded = false;
            _isJumping = true;
            _isHorizontalInput = false;
        }

        public void SetJumpMoveState(bool rightDirection)
        {
            _rightDirection = rightDirection;
            _isGrounded = false;
            _isJumping = true;
            _isHorizontalInput = true;
        }

        public void SetFallState()
        {
            _isGrounded = false;
            _isJumping = false;
        }

        public void SetLandedState()
        {
            _isGrounded = true;
            _isJumping = false;
        }
    }

    public enum PowerState { Low, Normal, High }
}
