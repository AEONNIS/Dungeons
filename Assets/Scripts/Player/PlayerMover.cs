using Game.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2d;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private float _deltaTimeFactorForVelocityLerp = 5.0f;
        [SerializeField] private UnityEvent _playerFalls;
        [SerializeField] private UnityEvent _playerHasLanded;

        private bool _isGrounded = true;
        private Vector2 _lastVelocity;

        public event UnityAction PlayerFalls { add => _playerFalls.AddListener(value); remove => _playerFalls.RemoveListener(value); }
        public event UnityAction PlayerHasLanded { add => _playerHasLanded.AddListener(value); remove => _playerHasLanded.RemoveListener(value); }
        public Vector2 LastVelocity => _lastVelocity;

        #region Unity
        private void FixedUpdate()
        {
            GroundCheck();
            _lastVelocity = _rigidbody2d.velocity;
        }
        #endregion

        public void Move(PlayerBase playerBase, bool rightDirection, PowerState powerState)
        {
            _rigidbody2d.velocity = Vector2.Lerp(_rigidbody2d.velocity, GetDirectionVector(rightDirection) * GetCurrentDeltaSpeed(playerBase, powerState),
                                                 _deltaTimeFactorForVelocityLerp * Time.deltaTime);
        }

        public bool SlowdownAndCheckHorizontalSpeed()
        {
            if (_rigidbody2d.velocity.x != 0)
            {
                _rigidbody2d.velocity = Vector2.Lerp(_rigidbody2d.velocity, Vector2.zero, Time.deltaTime);
                return false;
            }

            return true;
        }

        public void Jump(PlayerBase playerBase, PowerState powerState)
        {
            _rigidbody2d.AddForce(Vector2.up * GetCurrentDeltaJumpForce(playerBase, powerState), ForceMode2D.Impulse);
        }

        public void JumpMove(PlayerBase playerBase, bool rightDirection, PowerState powerState)
        {
            _rigidbody2d.velocity = Vector2.zero;
            _rigidbody2d.AddForce(GetCurrentDeltaTotalForce(playerBase, rightDirection, powerState), ForceMode2D.Impulse);
        }

        private void GroundCheck()
        {
            if (_groundChecker.IsGrounded() != _isGrounded)
            {
                if (_isGrounded)
                    _playerFalls?.Invoke();
                else
                    _playerHasLanded?.Invoke();
            }
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
}
