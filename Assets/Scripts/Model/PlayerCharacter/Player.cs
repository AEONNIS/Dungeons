using Dungeons.Infrastructure;
using Dungeons.Model.InventorySystem;
using Dungeons.Model.Items;
using Dungeons.Operations;
using Dungeons.Presentation;
using Dungeons.Presentation.PlayerCharacter;
using UnityEngine;

namespace Dungeons.Model.PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _positionYCorrection = 0.38f;
        [SerializeField] private float _health = 100.0f;
        [SerializeField] private PlayerData _data;
        [SerializeField] private Mover _mover;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private KeyboardInput _keyboardinput;
        [SerializeField] private Timer _timer;
        [Range(0.0f, 0.2f)]
        [SerializeField] private float _jumpStartTime;
        [SerializeField] private PlayerStates _states = new PlayerStates();
        [Header("UI:")]
        [SerializeField] private PlayerPanel _playerPanel;

        public Vector2 Position => new Vector2(transform.position.x, transform.position.y + _positionYCorrection);
        public PlayerData Data => _data;
        public Inventory Inventory => _inventory;
        public PlayerStates States => _states;

        #region Unity
        private void Awake()
        {
            _states.SetDefault();
            _animator.SetStates(_states);
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

        public void Init()
        {
            _playerPanel.Present(_health, _states.PowerState);
        }

        public void SwitchPowerState()
        {
            _states.SwitchPowerState();
            _animator.SetStates(_states);
            _playerPanel.PresentPowerState(_states.PowerState);
        }

        public void TryMoveForward(bool rightDirection)
        {
            if (_states.Grounded && _states.Jumping == false)
            {
                _states.SetMoveState(rightDirection);
                _mover.MoveForward();
                _animator.SetStates(_states);
            }
        }

        public void TrySlowdown()
        {
            if (_states.Grounded && _states.Jumping == false && _states.HorizontalSpeed)
            {
                _states.SetStateToSlowdownOrIdle(_mover.SlowdownAndCheckHorizontalSpeed());
                _animator.SetStates(_states);
            }
        }

        public void TryJumpUp()
        {
            if (_states.Grounded && _states.Jumping == false)
            {
                _states.SetJumpStartState();
                _mover.JumpUp();
                _animator.SetStates(_states);

                _timer.StartOff(_jumpStartTime, () =>
                {
                    _states.SetJumpState();
                    _animator.SetStates(_states);
                });
            }
        }

        public void TryJumpForward(bool rightDirection)
        {
            if (_states.Grounded && _states.Jumping == false)
            {
                _states.SetJumpMoveStartState(rightDirection);
                _mover.JumpForward();
                _animator.SetStates(_states);

                _timer.StartOff(_jumpStartTime, () =>
                {
                    _states.SetJumpMoveState(rightDirection);
                    _animator.SetStates(_states);
                });
            }
        }

        public void PutNear(Item item)
        {
            item.transform.position = transform.position;
            item.gameObject.SetActive(true);
        }

        private void OnPlayerFalls()
        {
            _states.SetFallState();
            _animator.SetStates(_states);
        }

        private void OnPlayerHasLanded(Vector2 landingVelocity)
        {
            if (FallingDamageAndCheckDeath(Mathf.Abs(landingVelocity.y)))
                Death();

            _states.SetLandedState();
            _animator.SetStates(_states);
            _playerPanel.PresentHealth(_health);
        }

        private bool FallingDamageAndCheckDeath(float impactSpeed)
        {
            return ToDamageAndCheckDeath(_data.GetDamageForFallingSpeed(impactSpeed));
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
