using Game.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<float> _moveSpeedMultipliers;

        private int _rightDirectionHash = Animator.StringToHash("RightDirection");
        private int _isGroundedHash = Animator.StringToHash("IsGrounded");
        private int _isJumpingHash = Animator.StringToHash("IsJumping");
        private int _isHorizontalInputHash = Animator.StringToHash("IsHorizontalInput");
        private int _isHorizontalSpeedHash = Animator.StringToHash("IsHorizontalSpeed");
        private int _isFunHash = Animator.StringToHash("IsFun");
        private int _diedHash = Animator.StringToHash("Died");
        private int _moveSpeedMultiplierHash = Animator.StringToHash("MoveSpeedMultiplier");

        public void SetAnimatorStates(PlayerStates playerStates)
        {
            _animator.SetBool(_rightDirectionHash, playerStates.RightDirection);
            _animator.SetBool(_isGroundedHash, playerStates.IsGrounded);
            _animator.SetBool(_isJumpingHash, playerStates.IsJumping);
            _animator.SetBool(_isHorizontalInputHash, playerStates.IsHorizontalInput);
            _animator.SetBool(_isHorizontalSpeedHash, playerStates.IsHorizontalSpeed);
            (playerStates.IsFun ? (Action<int>)_animator.SetTrigger : _animator.ResetTrigger)(_isFunHash);
            (playerStates.Died ? (Action<int>)_animator.SetTrigger : _animator.ResetTrigger)(_diedHash);
            _animator.SetFloat(_moveSpeedMultiplierHash, _moveSpeedMultipliers[(int)playerStates.PowerState]);
        }
    }
}
