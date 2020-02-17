using Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.InventorySystem
{
    public class SlotPresenter : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _emptySlotSprite;
        [Header("Colors:")]
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _pointerEnterColor = new Color(0.45f, 1.0f, 0.63f);
        [SerializeField] private Color _draggingColor = new Color(1.0f, 0.67f, 0.39f);

        public void PresentItem(Item item)
        {
            if (item != null)
                Present(item.Base.InventorySprite);
            else
                Present(_emptySlotSprite);
        }

        public void ResetBacklight()
        {
            Backlight(_defaultColor);
        }

        public void PointerEnterBacklight()
        {
            Backlight(_pointerEnterColor);
        }

        public void DraggingBacklight()
        {
            Backlight(_draggingColor);
        }

        private void Present(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        private void Backlight(Color color)
        {
            _image.color = color;
        }
    }
}
