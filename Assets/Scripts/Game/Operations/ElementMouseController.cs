using Game.Model;
using Game.Model.PlayerCharacter;
using Game.Presentation;
using Game.Presentation.UI.NotificationSystem;
using UnityEngine;

namespace Game.Control
{
    public class ElementMouseController : MonoBehaviour
    {
        [SerializeField] private protected ColorBacklighter _backlighter;
        [SerializeField] private protected Player _player;
        [SerializeField] private protected float _playerCloseDistance;
        [TextArea(2, 3)]
        [SerializeField] private protected string _playerFarMessage;
        [TextArea(2, 3)]
        [SerializeField] private protected string _notItemForInteractionMessage;
        [Header("UI:")]
        [SerializeField] private protected InfoPanel _infoPanel;
        [SerializeField] private protected Notifier _notifier;

        private protected virtual IInfoElement InfoElement { get; }

        private protected void MouseEntered()
        {
            _backlighter?.Backlight(PlayerIsClose());
            _infoPanel.Present(InfoElement);
        }

        private protected void MouseExited()
        {
            _backlighter?.ResetSmoothly();
            _infoPanel.FadeOut();
        }

        private protected bool PlayerIsClose()
        {
            return Vector2.Distance(transform.position, _player.Position) <= _playerCloseDistance;
        }
    }
}
