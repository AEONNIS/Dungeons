using Game.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<float> _moveSpeedMultipliers;

        private int _rightDirectionHash;
        private int _isGroundedHash;
        private int _isJumpingHash;
        private int _isHorizontalInputHash;
        private int _isHorizontalSpeedHash;
        private int _triumphingHash;
        private int _diedHash;
        private int _moveSpeedMultiplierHash;

        #region Unity
        private void Awake()
        {
            _rightDirectionHash = Animator.StringToHash("RightDirection");
            _isGroundedHash = Animator.StringToHash("IsGrounded");
            _isJumpingHash = Animator.StringToHash("IsJumping");
            _isHorizontalInputHash = Animator.StringToHash("IsHorizontalInput");
            _isHorizontalSpeedHash = Animator.StringToHash("IsHorizontalSpeed");
            _triumphingHash = Animator.StringToHash("Triumphing");
            _diedHash = Animator.StringToHash("Died");
            _moveSpeedMultiplierHash = Animator.StringToHash("MoveSpeedMultiplier");
        }
        #endregion

        public void SetAnimatorStates(PlayerStates playerStates)
        {
            _animator.SetBool(_rightDirectionHash, playerStates.RightDirection);
            _animator.SetBool(_isGroundedHash, playerStates.IsGrounded);
            _animator.SetBool(_isJumpingHash, playerStates.IsJumping);
            _animator.SetBool(_isHorizontalInputHash, playerStates.IsHorizontalInput);
            _animator.SetBool(_isHorizontalSpeedHash, playerStates.IsHorizontalSpeed);
            _animator.SetBool(_triumphingHash, playerStates.Triumphing);
            _animator.SetBool(_diedHash, playerStates.Died);
            _animator.SetFloat(_moveSpeedMultiplierHash, _moveSpeedMultipliers[(int)playerStates.PowerState]);
        }
    }
}
