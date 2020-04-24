using Dungeons.Model;
using Dungeons.Model.InteractionSystem;
using Dungeons.Model.Tiles;
using UnityEngine;

namespace Dungeons.Presentation
{
    public class TileMouseController : ElementMouseController
    {
        [SerializeField] private Tile _tile;
        [SerializeField] private Interaction _interaction;

        private protected override IInfoElement InfoElement => (IInfoElement)_tile;

        #region Unity
        private void OnValidate()
        {
            if ((_tile is IInfoElement) == false)
            {
                _tile = null;
                Debug.LogError($"{_tile.name} needs to implement {nameof(IInfoElement)}");
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
            if (_tile.Type == TileType.BlackStone)
                return;

            if (PlayerIsClose())
            {
                if (_player.Inventory.InHandsItem != null)
                {
                    _interaction.ApplyDamageToTileAndItemInHands(_tile);
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
