using UnityEngine;

namespace Dungeons.Infrastructure.Presentation
{
    public class RectTransformStretcher : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _basis;
        [SerializeField] private Paddings _paddings;

        private Rect _initialRect;

        public RectTransform Basis => _basis;
        public Paddings Paddings => _paddings;

        public void Init()
        {
            _initialRect = _rectTransform.rect;
        }

        public void StretchTo(Rect basis, Paddings paddings)
        {
            _rectTransform.offsetMax = basis.max - paddings.LeftTop;
            _rectTransform.offsetMin = basis.min + paddings.RightBottom;
        }

        public void StretchTo(Rect basis)
        {
            StretchTo(basis, new Paddings());
        }

        public void StretchToBasis(bool usePaddings = true)
        {
            StretchTo(_basis.rect, usePaddings ? _paddings : new Paddings());
        }

        public void StretchToInitialRect()
        {
            StretchTo(_initialRect);
        }
    }
}
