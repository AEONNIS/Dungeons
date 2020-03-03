using Game.Model.InventorySystem;
using UnityEngine;

namespace Game.PlayerCharacter
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Inventory _inventory;
        [Header("Клавиши управления:")]
        [SerializeField] private KeyCode _upKey = KeyCode.UpArrow;
        [SerializeField] private KeyCode _downKey = KeyCode.DownArrow;
        [SerializeField] private KeyCode _leftKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _rightKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode _powerSwitchKey = KeyCode.LeftControl;
        [SerializeField] private KeyCode _inventoryKey = KeyCode.I;
        [SerializeField] private KeyCode _pickUpItemsKey = KeyCode.P;
        [SerializeField] private KeyCode _gameMenuKey = KeyCode.Escape;

        private HorizontalInput _horizontalInput;

        #region Unity
        private void FixedUpdate()
        {
            _horizontalInput = GetHorizontalInput();

            if (Input.GetKey(_jumpKey))
                PlayerTryJump(_horizontalInput);
            else
                PlayerTryMove(_horizontalInput);
        }

        private void Update()
        {
            if (Input.GetKeyDown(_powerSwitchKey))
                _player.SwitchPowerState();

            if (Input.GetKeyDown(_inventoryKey))
                _inventory.SwitchOpeningState();

            if (Input.GetKeyDown(_pickUpItemsKey))
                _inventory.PickUpAllItemsNearPlayer();

        }
        #endregion

        private HorizontalInput GetHorizontalInput()
        {
            if (Input.GetKey(_leftKey) && Input.GetKey(_rightKey) == false)
                return HorizontalInput.Left;
            else if (Input.GetKey(_rightKey) && Input.GetKey(_leftKey) == false)
                return HorizontalInput.Right;
            else
                return HorizontalInput.None;
        }

        private void PlayerTryJump(HorizontalInput horizontalInput)
        {
            if (horizontalInput == HorizontalInput.None)
                _player.TryJump();
            else if (horizontalInput == HorizontalInput.Left)
                _player.TryJumpMove(false);
            else
                _player.TryJumpMove(true);
        }

        private void PlayerTryMove(HorizontalInput horizontalInput)
        {
            if (horizontalInput == HorizontalInput.None)
                _player.TrySlowdown();
            else if (horizontalInput == HorizontalInput.Left)
                _player.TryMove(false);
            else
                _player.TryMove(true);
        }
    }

    public enum HorizontalInput { Left = -1, None = 0, Right = 1 }
}
