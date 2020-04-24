using Dungeons.Model.PlayerCharacter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Presentation.PlayerCharacter
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<float> _moveSpeedMultipliers;

        private readonly int _rightDirectionHash = Animator.StringToHash("RightDirection");
        private readonly int _groundedHash = Animator.StringToHash("Grounded");
        private readonly int _jumpingHash = Animator.StringToHash("Jumping");
        private readonly int _horizontalInputHash = Animator.StringToHash("HorizontalInput");
        private readonly int _horizontalSpeedHash = Animator.StringToHash("HorizontalSpeed");
        private readonly int _funHash = Animator.StringToHash("Fun");
        private readonly int _diedHash = Animator.StringToHash("Died");
        private readonly int _moveSpeedMultiplierHash = Animator.StringToHash("MoveSpeedMultiplier");

        public void SetStates(PlayerStates playerStates)
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
