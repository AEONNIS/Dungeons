using Game.Model.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.UI.InventorySystem
{
    public class SlotPresenter : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private SlotPresenterBasis _basis;

        public void Present(Item item)
        {
            if (item != null)
                Present(item.Sprite);
            else
                Present(_basis.EmptySlotSprite);
        }

        public void ResetBacklight()
        {
            Backlight(_basis.Colors.Default);
        }

        public void PointerEnterBacklight()
        {
            Backlight(_basis.Colors.PointerEnter);
        }

        public void DraggingBacklight()
        {
            Backlight(_basis.Colors.Dragging);
        }

        private void Present(Sprite item)
        {
            _image.sprite = item;
        }

        private void Backlight(Color color)
        {
            _image.color = color;
        }
    }
}
