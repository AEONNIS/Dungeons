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
        private int _groundedHash = Animator.StringToHash("Grounded");
        private int _jumpingHash = Animator.StringToHash("Jumping");
        private int _horizontalInputHash = Animator.StringToHash("HorizontalInput");
        private int _horizontalSpeedHash = Animator.StringToHash("HorizontalSpeed");
        private int _funHash = Animator.StringToHash("Fun");
        private int _diedHash = Animator.StringToHash("Died");
        private int _moveSpeedMultiplierHash = Animator.StringToHash("MoveSpeedMultiplier");

        public void SetAnimatorStates(PlayerStates playerStates)
        {
            _animator.SetBool(_rightDirectionHash, playerStates.RightDirection);
            _animator.SetBool(_groundedHash, playerStates.Grounded);
            _animator.SetBool(_jumpingHash, playerStates.Jumping);
            _animator.SetBool(_horizontalInputHash, playerStates.HorizontalInput);
            _animator.SetBool(_horizontalSpeedHash, playerStates.HorizontalSpeed);
            (playerStates.Fun ? (Action<int>)_animator.SetTrigger : _animator.ResetTrigger)(_funHash);
            (playerStates.Died ? (Action<int>)_animator.SetTrigger : _animator.ResetTrigger)(_diedHash);
            _animator.SetFloat(_moveSpeedMultiplierHash, _moveSpeedMultipliers[(int)playerStates.PowerState]);
        }
    }
}
