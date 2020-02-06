using Game.Model;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerBase _base;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerStates _states = new PlayerStates();

        #region Unity
        private void Awake()
        {
            _states.SetDefault();
        }

        private void OnEnable()
        {
            _mover.PlayerFalls += OnPlayerFalls;
            _mover.PlayerHasLanded += OnPlayerHasLanded;
        }

        private void OnDisable()
        {
            _mover.PlayerFalls -= OnPlayerFalls;
            _mover.PlayerHasLanded -= OnPlayerHasLanded;
        }
        #endregion

        public void SwitchPowerState()
        {
            _states.SwitchPowerState();
            _animator.SetAnimatorStates(_states);
        }

        public void TryMove(bool rightDirection)
        {
            if (_states.IsGrounded)
            {
                _states.SetMoveState(rightDirection);
                _mover.Move(_base, rightDirection, _states.PowerState);
                _animator.SetAnimatorStates(_states);
            }
        }

        public void TrySlowdown()
        {
            if (_states.IsGrounded && _states.IsHorizontalSpeed)
            {
                _states.SetStateToSlowdownOrStop(false, _mover.SlowdownAndCheckHorizontalSpeed());
                _animator.SetAnimatorStates(_states);
            }
        }

        public void TryJump()
        {
            if (_states.IsGrounded)
            {
                _states.SetJumpState();
                _mover.Jump(_base, _states.PowerState);
                _animator.SetAnimatorStates(_states);
            }
        }

        public void TryJumpMove(bool rightDirection)
        {
            if (_states.IsGrounded)
            {
                _states.SetJumpMoveState(rightDirection);
                _mover.JumpMove(_base, rightDirection, _states.PowerState);
                _animator.SetAnimatorStates(_states);
            }
        }

        private void OnPlayerFalls()
        {
            _states.SetFallState();
            _animator.SetAnimatorStates(_states);
        }

        private void OnPlayerHasLanded()
        {
            _states.SetLandedState();
            _animator.SetAnimatorStates(_states);
        }
    }
}
