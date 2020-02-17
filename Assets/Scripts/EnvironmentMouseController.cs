using Game.Model;
using Game.PlayerCharacter;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class EnvironmentMouseController : MonoBehaviour
    {
        [SerializeField] private Environment _environment;
        [SerializeField] private ColorBacklight _backlight;
        [SerializeField] private InfoPanel _infoPanel;
        [SerializeField] private Notifier _notifier;
        [TextArea(2, 3)] [SerializeField] private string _playerIsFarMessage;
        [SerializeField] private Player _player;
        [SerializeField] private float _playerCloseDistance;

        #region Unity
        private void OnMouseEnter()
        {
            _backlight?.WhenPointerHover(PlayerIsClose());
            _infoPanel.Present(_environment);
        }

        private void OnMouseExit()
        {
            _backlight?.ResetBacklight();
            _infoPanel.FadeOut();
        }

        private void OnMouseDown()
        {
            if (PlayerIsClose())
            {

            }
            else
            {
                _notifier?.ShowMessage(_playerIsFarMessage, _environment.Sprite);
            }
        }

        private void OnMouseOver()
        {
            _infoPanel.Present(_environment);
        }
        #endregion

        private bool PlayerIsClose()
        {
            return Vector2.Distance(transform.position, _player.Position) <= _playerCloseDistance;
        }
    }
}
