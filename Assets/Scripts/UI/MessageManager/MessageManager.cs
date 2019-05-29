using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
  public RectTransform content;
  public RectTransform UIPlayerPanel;
  public RectTransform inventoryPanel;
  public int countMessages = 15;
  public GameObject messageWithImgPrefab;
  public GameObject messageOnlyTxtPrefab;

  private RectTransform _selfRect;
  private int _heightScreen;
  private int _widthScreen;
  private float _widthReferenceResolution;
  private static Queue<GameObject> _messagesWithImage;
  private static Queue<GameObject> _messagesOnlyText;


  private void Awake()
  {
    _selfRect = GetComponent<RectTransform>();
    _widthReferenceResolution = _selfRect.parent.GetComponent<CanvasScaler>().referenceResolution.x;
    PositionUpdate();

    _messagesWithImage = new Queue<GameObject>();
    _messagesOnlyText = new Queue<GameObject>();

    for (int i = 0; i < countMessages; i++)
    {
      GameObject message = Instantiate(messageWithImgPrefab, content);
      message.SetActive(false);
      _messagesWithImage.Enqueue(message);
    }

    for (int i = 0; i < countMessages; i++)
    {
      GameObject message = Instantiate(messageOnlyTxtPrefab, content);
      message.SetActive(false);
      _messagesOnlyText.Enqueue(message);
    }
  }

  private void Update()
  {
    if (_heightScreen != Screen.height)
    {
      Debug.Log("Изменение высоты экрана");
      PositionUpdate();
    }

    if (_widthScreen != Screen.width)
    {
      Debug.Log("Изменение широты экрана");
      PositionUpdate();
    }
  }

  private void PositionUpdate()
  {
    _heightScreen = Screen.height;
    _widthScreen = Screen.width;

    _selfRect.offsetMax = new Vector2(_selfRect.rect.width, UIPlayerPanel.offsetMin.y);
    _selfRect.offsetMin = new Vector2(0.0f, -_widthReferenceResolution * _heightScreen / _widthScreen + inventoryPanel.offsetMax.y);
  }

  public static void ShowMessageWithImage(string message, Sprite spriteMsg)
  {
    GameObject msg = _messagesWithImage.Dequeue();
    msg.transform.SetAsFirstSibling();
    msg.transform.GetChild(0).GetComponent<Text>().text = message;
    msg.transform.GetChild(1).GetComponent<Image>().sprite = spriteMsg;
    _messagesWithImage.Enqueue(msg);
    msg.SetActive(true);
  }

  public static void ShowMessageOnlyText(string message)
  {
    GameObject msg = _messagesOnlyText.Dequeue();
    msg.transform.SetAsFirstSibling();
    msg.transform.GetChild(0).GetComponent<Text>().text = message;
    _messagesOnlyText.Enqueue(msg);
    msg.SetActive(true);
  }
}
