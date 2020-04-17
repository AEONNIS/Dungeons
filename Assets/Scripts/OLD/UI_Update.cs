using UnityEngine;
using UnityEngine.UI;

public class UI_Update : MonoBehaviour
{
  public float _fadeDuration = 0.3f;
  public Image _image;
  public Image _strengthBar;
  public Text _strength;
  public Text _name;
  public Text _description;

  private Graphic[] _graphics;

  private void Awake()
  {
    _graphics = GetComponentsInChildren<Graphic>();
    Fade(0);
  }

  public void Fade(float alpha)
  {
    for (int i = 0; i < _graphics.Length; i++)
    {
      if (_graphics[i] != null)
      {
        _graphics[i].CrossFadeAlpha(alpha, _fadeDuration, false);
      }
    }
  }

  public void DataReplacement(Sprite sprite, string name, string description, float strength)
  {
    _image.sprite = sprite;
    _strength.text = strength.ToString() + " %";
    _strengthBar.fillAmount = strength * 0.01f;
    _name.text = name;
    _description.text = description;
  }
}
