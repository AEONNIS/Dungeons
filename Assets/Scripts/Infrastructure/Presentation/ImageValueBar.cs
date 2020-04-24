using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Infrastructure.Presentation
{
    public class ImageValueBar : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private string _textEnd = " %";
        [SerializeField] private Image _image;

        public void Present(float value)
        {
            _text.text = $"{value:0.00}{_textEnd}";
            _image.fillAmount = value * 0.01f;
        }
    }
}
