using Game.Model;
using Game.PlayerCharacter;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class ElementMouseController : MonoBehaviour
    {
        [SerializeField] private protected ColorBacklight _backlight;
        [SerializeField] private protected Player _player;
        [SerializeField] private protected float _playerCloseDistance;
        [SerializeField] [TextArea(2, 3)] private protected string _playerFarMessage;
        [Header("UI:")]
        [SerializeField] protected private InfoPanel _infoPanel;
        [SerializeField] protected private Notifier _notifier;

        private protected virtual IInfoElement InfoElement { get; }

        private protected void WhenMouseEnter()
        {
            _backlight?.WhenPointerHover(PlayerIsClose());
            _infoPanel.Present(InfoElement);
        }

        private protected void WhenMouseExit()
        {
            _backlight?.ResetBacklight();
            _infoPanel.FadeOut();
        }

        private protected bool PlayerIsClose()
        {
            return Vector2.Distance(transform.position, _player.Position) <= _playerCloseDistance;
        }
    }
}
