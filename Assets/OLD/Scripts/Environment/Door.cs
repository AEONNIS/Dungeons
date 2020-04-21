using UnityEngine;
using System;

public class Door : MonoBehaviour
{
  public string nameDoor;
  [TextArea(3, 7), Tooltip("Описание объекта. Отображается во всплывающей подсказке.")]
  public string description;

  [Header("Расстояние до игрока")]
  [Tooltip("Y коррекция расположения центра у игрока")]
  public float correctionPlayerY = 0.38f;
  [Tooltip("Расстояние до игрока, меньше которого будет считаться, что игрок подошел вплотную к объекту.")]
  public float playerDistance = 1.3f;

  [Header("Цвета")]
  public Color defaultColor = Color.white;
  [Tooltip("Цвет подсветки при наведении, когда игрок стоит вплотную")]
  public Color selectedPlColor = new Color(115, 255, 160, 255);
  [Tooltip("Цвет подсветки при наведении, когда игрок находится на расстоянии")]
  public Color selectedColor = new Color(255, 245, 90, 255);

  private SpriteRenderer _thisRenderer;
  private Animator _thisAnimator;
  private Transform _thisTransform;
  private Transform _playerTransform;
  private PlayerOLD _player;
  private UI_Update _UI_Panel; // Панель в интерфейсе для вывода информации об объекте.

  private int _ahClose, _ahOpen;

  private doorState _doorState = doorState.close;

  private void OnSave()
  {
    string doorToSave = string.Format("{{\n_thisTransform.name = \"{0}\"\n", _thisTransform.name);
    //doorToSave += string.Format("_thisTransform.position = {0}\n", _thisTransform.position.ToString("0.00"));
    doorToSave += string.Format("Door._doorState = {0}\n}}\n", _doorState);

    GManager.Instance.AddObjectToSave((DoorId)0, doorToSave);
  }

  private void OnLoad()
  {
    string doorToLoad = GManager.Instance.ReadNextObjectToLoad((DoorId)0);
    _doorState = (doorState)Enum.Parse(typeof(doorState), GManager.FindStringParameterInObject(doorToLoad, "Door._doorState", "", ""));

    PlayAnimation();
  }

  private void Awake()
  {
    _thisRenderer = GetComponent<SpriteRenderer>();
    _thisAnimator = GetComponent<Animator>();
    _thisTransform = GetComponent<Transform>();
    _playerTransform = GameObject.Find("PlayerOLD").transform;
    _player = _playerTransform.GetComponent<PlayerOLD>();
    _UI_Panel = GameObject.Find("TooltipPanelOLD").GetComponent<UI_Update>();

    _ahClose = Animator.StringToHash("Close");
    _ahOpen = Animator.StringToHash("Open");
  }

  private void Start()
  {
    GManager.Instance.save += OnSave;
    GManager.Instance.load += OnLoad;
  }

  private void OnMouseEnter()
  {
    if (_doorState == doorState.close)
    {
      if (PlayerCheck() < playerDistance)
        _thisRenderer.color = selectedPlColor;
      else
        _thisRenderer.color = selectedColor;
    }

    _UI_Panel.DataReplacement(_thisRenderer.sprite, nameDoor, description, 100);
    _UI_Panel.Fade(1);
  }

  private void OnMouseDown()
  {
    if (_doorState == doorState.close)
    {
      if (PlayerCheck() < playerDistance)
      {
        _doorState = doorState.open;
        _thisRenderer.color = defaultColor;
        PlayAnimation();

        _player.PassedLevel();
      }
      else
      {
        MessageManager.ShowMessageOnlyText("Подойдите ближе, если хотите открыть эту дверь");
      }
    }
  }

  private void OnMouseExit()
  {
    if (_doorState == doorState.close)
    {
      _thisRenderer.color = defaultColor;
      _UI_Panel.Fade(0);
    }
  }

  private void PlayAnimation()
  {
    switch (_doorState)
    {
      case doorState.close:
        _thisAnimator.Play(_ahClose);
        break;
      case doorState.open:
        _thisAnimator.Play(_ahOpen);
        break;
    }
  }

  private float PlayerCheck()
  {
    return Vector2.Distance(_thisTransform.position, new Vector2(_playerTransform.position.x, _playerTransform.position.y + correctionPlayerY));
  }

  private enum doorState { close, open };
}

public enum DoorId { Door0 };
