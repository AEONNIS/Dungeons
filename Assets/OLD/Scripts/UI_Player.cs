using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
  [SerializeField]
  private Image healthBar;
  [SerializeField]
  private Text health;
  [SerializeField]
  private Text forceState;

  public void SetHealth(float _health)
  {
    health.text = _health.ToString() + " %";
    healthBar.fillAmount = _health * 0.01f;
  }

  public void SetForceState(int varForceState)
  {
    switch (varForceState)
    {
      case 0:
        forceState.text = "Двиг. медл. / Прыг. слабо";
        break;
      case 1:
        forceState.text = "Двиг. норм. / Прыг. норм.";
        break;
      case 2:
        forceState.text = "Двиг. быстро / Прыг. сильно";
        break;
    }
  }
  public void SetForceState(string _forceState)
  {
    forceState.text = _forceState;
  }
}
