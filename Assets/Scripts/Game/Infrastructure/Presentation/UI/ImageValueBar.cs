using UnityEngine;
using UnityEngine.UI;

namespace Game.Infrastructure.Presentation.UI
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
