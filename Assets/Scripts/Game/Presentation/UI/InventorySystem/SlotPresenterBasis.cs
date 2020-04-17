using System;
using UnityEngine;

namespace Game.Presentation.UI.InventorySystem
{
    [CreateAssetMenu(fileName = "SlotPresenterBasis", menuName = "Game/Presentation/UI/InventorySystem/SlotPresenterBasis")]
    public class SlotPresenterBasis : ScriptableObject
    {
        [SerializeField] private SlotColors _colors;
        [SerializeField] private Sprite _emptySlotSprite;

        public SlotColors Colors => _colors;
        public Sprite EmptySlotSprite => _emptySlotSprite;

        [Serializable]
        public class SlotColors
        {
            [SerializeField] private Color _default = Color.white;
            [SerializeField] private Color _pointerEnter = new Color(0.45f, 1.0f, 0.65f);
            [SerializeField] private Color _dragging = new Color(1.0f, 0.70f, 0.40f);

            public Color Default => _default;
            public Color PointerEnter => _pointerEnter;
            public Color Dragging => _dragging;
        }
    }
}
