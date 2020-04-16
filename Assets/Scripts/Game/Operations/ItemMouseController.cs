using Game.Model;
using Game.Model.Items;
using UnityEngine;

namespace Game.Control
{
    public class ItemMouseController : ElementMouseController
    {
        [SerializeField] private Item _item;

        private protected override IInfoElement InfoElement => (IInfoElement)_item;

        #region Unity
        private void OnValidate()
        {
            if ((_item is IInfoElement) == false)
            {
                _item = null;
                Debug.LogError($"{_item.name} needs to implement {nameof(IInfoElement)}");
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
            if (PlayerIsClose())
            {
                if (_player.Inventory.TryTakeItemInHands(_item))
                    _backlighter.ResetImmediately();
            }
            else
            {
                _notifier?.ShowMessage(_playerFarMessage, InfoElement.Sprite);
            }

            _infoPanel.Present(InfoElement);
        }
        #endregion
    }
}
