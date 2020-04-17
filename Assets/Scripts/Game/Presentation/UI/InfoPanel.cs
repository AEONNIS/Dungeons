using Game.Infrastructure.Presentation.UI;
using Game.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.UI
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _name;
        [SerializeField] private ImageValueBar _strengthBar;
        [SerializeField] private Text _description;
        [SerializeField] private float _fadeDuration = 0.35f;

        private List<Graphic> _graphicElements = new List<Graphic>();

        #region Unity
        private void Awake()
        {
            _graphicElements.AddRange(GetComponentsInChildren<Graphic>());
            FadeOut();
        }
        #endregion

        public void Present(IInfoElement infoElement)
        {
            Present(infoElement.Sprite, infoElement.Name, infoElement.Description, infoElement.Strength);
            Fade(true, _fadeDuration);
        }

        public void FadeOut()
        {
            Fade(false, _fadeDuration);
        }

        private void Present(Sprite icon, string name, string description, float strength)
        {
            _icon.sprite = icon;
            _name.text = name;
            _description.text = description;
            _strengthBar.Present(strength);
        }

        private void Fade(bool visible, float duration)
        {
            float alpha = visible ? 1.0f : 0.0f;
            _graphicElements.ForEach(graphic => graphic.CrossFadeAlpha(alpha, duration, false));
        }
    }
}
