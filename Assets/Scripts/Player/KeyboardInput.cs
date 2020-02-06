using UnityEngine;

namespace Game.Player
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [Header("Клавиши управленя:")]
        [SerializeField] private KeyCode _up = KeyCode.UpArrow;
        [SerializeField] private KeyCode _down = KeyCode.DownArrow;
        [SerializeField] private KeyCode _left = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _right = KeyCode.RightArrow;
        [SerializeField] private KeyCode _jump = KeyCode.Space;
        [SerializeField] private KeyCode _powerSwitch = KeyCode.LeftControl;
        [SerializeField] private KeyCode _inventory = KeyCode.I;
        [SerializeField] private KeyCode _pickUpItems = KeyCode.P;
        [SerializeField] private KeyCode _gameMenu = KeyCode.Escape;

        private HorizontalInput _horizontalInput;

        #region Unity
        private void Update()
        {
            if (Input.GetKeyDown(_powerSwitch))
                _player.SwitchPowerState();

            _horizontalInput = CheckHorizontalInput();

            if (Input.GetKey(_jump))
                PlayerTriesToJump(_horizontalInput);
            else
                PlayerTriesToMove(_horizontalInput);
        }
        #endregion

        private HorizontalInput CheckHorizontalInput()
        {
            if (Input.GetKeyDown(_left) && Input.GetKeyDown(_right) == false)
                return HorizontalInput.Left;
            else if (Input.GetKeyDown(_right) && Input.GetKeyDown(_left) == false)
                return HorizontalInput.Right;
            else
                return HorizontalInput.None;
        }

        private void PlayerTriesToJump(HorizontalInput horizontalInput)
        {
            if (horizontalInput == HorizontalInput.None)
                _player.TryJump();
            else if (horizontalInput == HorizontalInput.Left)
                _player.TryJumpMove(false);
            else
                _player.TryJumpMove(true);
        }

        private void PlayerTriesToMove(HorizontalInput horizontalInput)
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
