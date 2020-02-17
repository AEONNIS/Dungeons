using UnityEngine;

public class ColorHighlightingItem : MonoBehaviour
{
  [Header("Расстояние до игрока")]
  [Tooltip("Y коррекция расположения центра у игрока")]
  public float correctionPlayerY = 0.38f;
  [Tooltip("Расстояние до игрока, меньше которого будет считаться, что игрок подошел вплотную к объекту.")]
  public float playerDistance = 1.3f;

  [Header("Цвета")]
  public Color defaultColor = Color.white;
  [Tooltip("Цвет подсветки при наведении, когда игрок стоит вплотную")]
  public Color selectedColorPl = new Color(115, 255, 160, 255);
  [Tooltip("Цвет подсветки при наведении, когда игрок находится на расстоянии")]
  public Color selectedColor = new Color(255, 245, 90, 255);

  private SpriteRenderer _renderer;
  private Transform _selfTransform;
  private Transform _playerTransform;
  private UI_Update _UI_Panel; // Панель в интерфейсе для вывода инфориации об объекте.
  private ItemDisplay _itemDisplay;
  private Inventory _inventory;

  private selectionState _selectionState = selectionState.normal;

  private void Awake()
  {
    _renderer = GetComponent<SpriteRenderer>();
    _selfTransform = GetComponent<Transform>();
    _playerTransform = GameObject.Find("PlayerOLD").transform;
    _UI_Panel = GameObject.Find("TooltipPanelOLD").GetComponent<UI_Update>();
    _itemDisplay = GetComponent<ItemDisplay>();
    _inventory = _playerTransform.GetComponent<Inventory>();
  }

  private void OnMouseEnter()
  {
    if (PlayerCheck() < playerDistance)
    {
      _selectionState = selectionState.selectedPlayer;
      _renderer.color = selectedColorPl;
    }
    else
    {
      _selectionState = selectionState.selected;
      _renderer.color = selectedColor;
    }

    _UI_Panel.DataReplacement(_renderer.sprite, _itemDisplay.item.nameItem, _itemDisplay.item.description, _itemDisplay.strength);
    _UI_Panel.Fade(1);
  }

  private void OnMouseDown()
  {
    if (PlayerCheck() < playerDistance)
    {
      _inventory.TakeInHands(_itemDisplay);
    }
    else
    {
      MessageManager.ShowMessageOnlyText("Слишком далеко, чтобы взять предмет в руки");
    }
  }

  private void OnMouseExit()
  {
    _selectionState = selectionState.normal;
    _renderer.color = defaultColor;

    _UI_Panel.Fade(0);
  }

  private void OnDisable()
  {
    OnMouseExit();
  }

  private float PlayerCheck()
  {
    return Vector2.Distance(_selfTransform.position, new Vector2(_playerTransform.position.x, _playerTransform.position.y + correctionPlayerY));
  }

  private enum selectionState { normal, selectedPlayer, selected };
}
