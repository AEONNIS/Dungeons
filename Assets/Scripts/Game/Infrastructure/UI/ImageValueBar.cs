using UnityEngine;
using UnityEngine.UI;

namespace Game.Infrastructure.UI
{
    public class ImageValueBar : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        public void Present(float value)
        {
            _text.text = $"{value:0.00} %";
            _image.fillAmount = value * 0.01f;
        }
    }
}
