using Game.Model.PlayerCharacter;
using Game.UI;
using UnityEngine;

namespace Game.PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _positionYCorrection = 0.38f;
        [SerializeField] private float _health = 100.0f;
        [SerializeField] private PlayerBase _base;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private KeyboardInput _keyboardinput;
        [SerializeField] private Timer _timer;
        [Range(0.0f, 0.2f)] [SerializeField] private float _jumpStartTime;
        [SerializeField] private PlayerStates _states = new PlayerStates();
        [Header("UI:")]
        [SerializeField] private PlayerPanel _playerPanel;

        public Vector2 Position => new Vector2(transform.position.x, transform.position.y + _positionYCorrection);
        public PlayerBase Base => _base;
        public PlayerStates States => _states;

        #region Unity
        private void Awake()
        {
            _states.SetDefault();
            _animator.SetAnimatorStates(_states);
            _playerPanel.Present(_health, _states.PowerState);
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
            _playerPanel.PresentPowerState(_states.PowerState);
        }

        public void TryMove(bool rightDirection)
        {
            if (_states.Grounded && _states.Jumping == false)
            {
                _states.SetMoveState(rightDirection);
                _mover.Move();
                _animator.SetAnimatorStates(_states);
            }
        }

        public void TrySlowdown()
        {
            if (_states.Grounded && _states.Jumping == false && _states.HorizontalSpeed)
            {
                _states.SetStateToSlowdownOrIdle(_mover.SlowdownAndCheckHorizontalSpeed());
                _animator.SetAnimatorStates(_states);
            }
        }

        public void TryJump()
        {
            if (_states.Grounded && _states.Jumping == false)
            {
                _states.SetJumpStartState();
                _mover.Jump();
                _animator.SetAnimatorStates(_states);

                _timer.StartTimer(_jumpStartTime, () =>
                {
                    _states.SetJumpState();
                    _animator.SetAnimatorStates(_states);
                });
            }
        }

        public void TryJumpMove(bool rightDirection)
        {
            if (_states.Grounded && _states.Jumping == false)
            {
                _states.SetJumpMoveStartState(rightDirection);
                _mover.JumpMove();
                _animator.SetAnimatorStates(_states);

                _timer.StartTimer(_jumpStartTime, () =>
                {
                    _states.SetJumpMoveState(rightDirection);
                    _animator.SetAnimatorStates(_states);
                });
            }
        }

        private void OnPlayerFalls()
        {
            _states.SetFallState();
            _animator.SetAnimatorStates(_states);
        }

        private void OnPlayerHasLanded(Vector2 landingVelocity)
        {
            if (FallingDamageAndCheckDeath(Mathf.Abs(landingVelocity.y)))
                Death();

            _states.SetLandedState();
            _animator.SetAnimatorStates(_states);
            _playerPanel.PresentHealth(_health);
        }

        private bool FallingDamageAndCheckDeath(float impactSpeed)
        {
            return ToDamageAndCheckDeath(_base.GetDamageForFallingSpeed(impactSpeed));
        }

        private bool ToDamageAndCheckDeath(float damageValue)
        {
            _health = damageValue < _health ? _health - damageValue : 0.0f;
            return _health == 0.0f;
        }

        private void Death()
        {
            _states.SetDeathState();
            _keyboardinput.enabled = false;
        }
    }
}
