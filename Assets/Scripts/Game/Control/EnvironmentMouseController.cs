using Game.Model;
using Game.Model.InteractionSystem;
using Game.Model.Tiles;
using UnityEngine;

namespace Game.Control
{
    public class EnvironmentMouseController : ElementMouseController
    {
        [SerializeField] private Environment _environment;
        [SerializeField] private Interaction _interaction;

        private protected override IInfoElement InfoElement => (IInfoElement)_environment;

        #region Unity
        private void OnValidate()
        {
            if ((_environment is IInfoElement) == false)
            {
                _environment = null;
                Debug.LogError($"{_environment.name} needs to implement {nameof(IInfoElement)}");
            }
        }

        private void OnMouseEnter()
        {
            MouseEntered();
        }

        private void OnMouseExit()
        {
            MouseExited();
        }

        private void OnMouseDown()
        {
            if (_environment.Type == EnvironmentType.BlackStone)
                return;

            if (PlayerIsClose())
            {
                if (_player.Inventory.InHandsItem != null)
                {
                    _interaction.ApplyDamageToEnvironmentAndItemInHands(_environment);
                    _backlighter.ToFlash();
                }
                else
                {
                    _notifier.ShowMessage(_notItemForInteractionMessage, InfoElement.Sprite);
                }
            }
            else
            {
                _notifier.ShowMessage(_playerFarMessage, InfoElement.Sprite);
            }

            _infoPanel.Present(InfoElement);
        }
        #endregion
    }
}
