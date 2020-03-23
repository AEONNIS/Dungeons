using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ColorValueBar : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _gradient;

        public void Present(float value)
        {
            _text.text = $"{value:F2} %";
            _gradient.fillAmount = value * 0.01f;
        }
    }
}
