using Game.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Model.PlayerCharacter
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Rigidbody2D _rigidbody2d;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private float _deltaTimeFactorForVelocityLerp = 5.0f;
        [SerializeField] private UnityEvent _playerFalls;
        [SerializeField] private UnityEventVector2 _playerHasLanded;

        public event UnityAction PlayerFalls { add => _playerFalls.AddListener(value); remove => _playerFalls.RemoveListener(value); }
        public event UnityAction<Vector2> PlayerHasLanded { add => _playerHasLanded.AddListener(value); remove => _playerHasLanded.RemoveListener(value); }

        #region Unity
        private void FixedUpdate()
        {
            GroundCheck();
        }
        #endregion

        public void MoveForward()
        {
            _rigidbody2d.velocity = Vector2.Lerp(_rigidbody2d.velocity, GetDirectionVector(_player.States.RightDirection) * GetCurrentDeltaSpeed(_player.Base, _player.States.PowerState),
                                                 _deltaTimeFactorForVelocityLerp * Time.deltaTime);
        }

        public bool SlowdownAndCheckHorizontalSpeed()
        {
            if (_rigidbody2d.velocity.x != 0)
            {
                _rigidbody2d.velocity = Vector2.Lerp(_rigidbody2d.velocity, Vector2.zero, Time.deltaTime);
                return true;
            }

            return false;
        }

        public void JumpUp()
        {
            _rigidbody2d.AddForce(Vector2.up * GetCurrentDeltaJumpForce(_player.Base, _player.States.PowerState), ForceMode2D.Impulse);
        }

        public void JumpForward()
        {
            _rigidbody2d.velocity = Vector2.zero;
            _rigidbody2d.AddForce(GetCurrentDeltaTotalForce(_player.Base, _player.States.RightDirection, _player.States.PowerState), ForceMode2D.Impulse);
        }

        private void GroundCheck()
        {
            bool isGrounded = _groundChecker.IsGrounded();

            if (isGrounded == false && _player.States.Jumping == false)
                _playerFalls?.Invoke();
            else if (isGrounded == true && _player.States.Grounded == false)
                _playerHasLanded?.Invoke(_rigidbody2d.velocity);
        }

        private Vector2 GetDirectionVector(bool rightDirection)
        {
            return rightDirection ? Vector2.right : Vector2.left;
        }

        private float GetCurrentDeltaSpeed(PlayerBase playerBase, PowerState powerState)
        {
            return playerBase.GetSpeedForPowerState(powerState) * Time.deltaTime;
        }

        private float GetCurrentDeltaJumpForce(PlayerBase playerBase, PowerState powerState)
        {
            return playerBase.GetJumpForceForPowerState(powerState) * Time.deltaTime;
        }

        private Vector2 GetCurrentDeltaTotalForce(PlayerBase playerBase, bool rightDirection, PowerState powerState)
        {
            Vector2 totalForce = playerBase.GetTotalForceVectorForPowerState(powerState);
            Vector2 direction = GetDirectionVector(rightDirection);
            return new Vector2(totalForce.x * direction.x, totalForce.y) * Time.deltaTime;
        }
    }

    [Serializable]
    public class UnityEventVector2 : UnityEvent<Vector2> { }
}
