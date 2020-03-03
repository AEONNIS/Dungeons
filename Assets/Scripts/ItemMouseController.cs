using Game.Model;
using UnityEngine;

namespace Game
{
    public class ItemMouseController : ElementMouseController
    {
        [SerializeField] private Item _item;
        [SerializeField] private Model.InventorySystem.Inventory _inventory;

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
            WhenMouseEnter();
        }

        private void OnMouseExit()
        {
            WhenMouseExit();
        }

        private void OnMouseDown()
        {
            if (PlayerIsClose())
            {
                _inventory.TakeItemInHands(_item);
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
