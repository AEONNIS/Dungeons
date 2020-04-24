using Dungeons.Model.PlayerCharacter;
using System;
using UnityEngine;

namespace Dungeons.Operations
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [Header("Клавиши управления:")]
        [SerializeField] private KeyCode[] _upKeys = { KeyCode.UpArrow, KeyCode.W };
        [SerializeField] private KeyCode[] _downKeys = { KeyCode.DownArrow, KeyCode.S };
        [SerializeField] private KeyCode[] _leftKeys = { KeyCode.LeftArrow, KeyCode.A };
        [SerializeField] private KeyCode[] _rightKeys = { KeyCode.RightArrow, KeyCode.D };
        [SerializeField] private KeyCode[] _jumpKeys = { KeyCode.Space };
        [SerializeField] private KeyCode[] _powerSwitchKeys = { KeyCode.LeftControl, KeyCode.RightControl };
        [SerializeField] private KeyCode[] _inventoryKeys = { KeyCode.I };
        [SerializeField] private KeyCode[] _pickUpItemsKeys = { KeyCode.P };
        [SerializeField] private KeyCode[] _gameMenuKeys = { KeyCode.Escape };

        private HorizontalInput _horizontalInput;

        private enum HorizontalInput { Left = -1, None = 0, Right = 1 }

        #region Unity
        private void FixedUpdate()
        {
            _horizontalInput = GetHorizontalInput();

            if (AnyKeyFrom(_jumpKeys, Input.GetKey))
                PlayerTryJump(_horizontalInput);
            else
                PlayerTryMove(_horizontalInput);
        }

        private void Update()
        {
            if (AnyKeyFrom(_powerSwitchKeys, Input.GetKeyDown))
                _player.SwitchPowerState();

            if (AnyKeyFrom(_inventoryKeys, Input.GetKeyDown))
                _player.Inventory.SwitchState();

            if (AnyKeyFrom(_pickUpItemsKeys, Input.GetKeyDown))
                _player.Inventory.PickUpAllItemsNearPlayer();

        }
        #endregion

        private bool AnyKeyFrom(KeyCode[] keyCodes, Func<KeyCode, bool> keyOperation)
        {
            foreach (var keyCode in keyCodes)
            {
                if (keyOperation(keyCode))
                    return true;
            }
            return false;
        }

        private HorizontalInput GetHorizontalInput()
        {
            if (AnyKeyFrom(_leftKeys, Input.GetKey) && AnyKeyFrom(_rightKeys, Input.GetKey) == false)
                return HorizontalInput.Left;
            else if (AnyKeyFrom(_rightKeys, Input.GetKey) && AnyKeyFrom(_leftKeys, Input.GetKey) == false)
                return HorizontalInput.Right;
            else
                return HorizontalInput.None;
        }

        private void PlayerTryJump(HorizontalInput horizontalInput)
        {
            if (horizontalInput == HorizontalInput.None)
                _player.TryJumpUp();
            else if (horizontalInput == HorizontalInput.Left)
                _player.TryJumpForward(false);
            else
                _player.TryJumpForward(true);
        }

        private void PlayerTryMove(HorizontalInput horizontalInput)
        {
            if (horizontalInput == HorizontalInput.None)
                _player.TrySlowdown();
            else if (horizontalInput == HorizontalInput.Left)
                _player.TryMoveForward(false);
            else
                _player.TryMoveForward(true);
        }
    }
}
