using System;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public struct PlayerStates
    {
        private static int _powerStatesLength = Enum.GetNames(typeof(PowerState)).Length;

        [SerializeField] private bool _rightDirection;
        [SerializeField] private bool _grounded;
        [SerializeField] private bool _jumping;
        [SerializeField] private bool _horizontalInput;
        [SerializeField] private bool _horizontalSpeed;
        [SerializeField] private bool _fun;
        [SerializeField] private bool _died;
        [SerializeField] private PowerState _powerState;

        public bool RightDirection => _rightDirection;
        public bool Grounded => _grounded;
        public bool Jumping => _jumping;
        public bool HorizontalInput => _horizontalInput;
        public bool HorizontalSpeed => _horizontalSpeed;
        public bool Fun => _fun;
        public bool Died => _died;
        public PowerState PowerState => _powerState;

        public void SetDefault()
        {
            SetStates(true, true, false, false, false, false, false, PowerState.Normal);
        }

        public void SwitchPowerState()
        {
            _powerState = (int)_powerState == _powerStatesLength - 1 ? 0 : ++_powerState;
        }

        public void SetMoveState(bool rightDirection)
        {
            SetStates(rightDirection, true, false, true, true, _fun, _died, _powerState);
        }

        public void SetStateToSlowdownOrIdle(bool horizontalSpeed)
        {
            SetStates(_rightDirection, true, false, false, horizontalSpeed, _fun, _died, _powerState);
        }

        public void SetJumpStartState()
        {
            SetStates(_rightDirection, true, true, false, _horizontalSpeed, _fun, _died, _powerState);
        }

        public void SetJumpState()
        {
            SetStates(_rightDirection, false, true, false, _horizontalSpeed, _fun, _died, _powerState);
        }

        public void SetJumpMoveStartState(bool rightDirection)
        {
            SetStates(rightDirection, true, true, true, true, _fun, _died, _powerState);
        }

        public void SetJumpMoveState(bool rightDirection)
        {
            SetStates(rightDirection, false, true, true, true, _fun, _died, _powerState);
        }

        public void SetFallState()
        {
            SetStates(_rightDirection, false, false, _horizontalInput, _horizontalSpeed, _fun, _died, _powerState);
        }

        public void SetLandedState()
        {
            SetStates(_rightDirection, true, false, _horizontalInput, _horizontalSpeed, _fun, _died, _powerState);
        }

        public void SetDeathState()
        {
            _died = true;
        }

        private void SetStates(bool rightDirection, bool grounded, bool jumping, bool horizontalInput, bool horizontalSpeed,
                               bool fun, bool died, PowerState powerState)
        {
            _rightDirection = rightDirection;
            _grounded = grounded;
            _jumping = jumping;
            _horizontalInput = horizontalInput;
            _horizontalSpeed = horizontalSpeed;
            _fun = fun;
            _died = died;
            _powerState = powerState;
        }
    }

    public enum PowerState { Low, Normal, High }
}
