using Dungeons.Model.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Presentation.InventorySystem
{
    public class SlotPresenter : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private SlotPresenterData _data;

        public void Present(Item item)
        {
            if (item != null)
                Present(item.Sprite);
            else
                Present(_data.EmptySlotSprite);
        }

        public void ResetBacklight()
        {
            Backlight(_data.Colors.Default);
        }

        public void PointerEnterBacklight()
        {
            Backlight(_data.Colors.PointerEnter);
        }

        public void DraggingBacklight()
        {
            Backlight(_data.Colors.Dragging);
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
