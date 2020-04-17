using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour //OLD
{
  public float displayTime = 4.0f;

  private void OnEnable()
  {
    CancelInvoke();
    Invoke("RemoveMessage", displayTime);
  }

  private void RemoveMessage()
  {
    CancelInvoke();
    gameObject.SetActive(false);
  }
}
