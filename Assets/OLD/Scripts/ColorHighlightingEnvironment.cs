using UnityEngine;

public class ColorHighlightingEnvironment : MonoBehaviour
{
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
  [Tooltip("Цвет подсветки при нанесении урона")]
  public Color onDamageColor = new Color(255, 40, 40, 255);
  [Tooltip("Скорость изменения цвета при нанесении урона"), Range(0, 60)]
  public float colorChangeSpeed = 30;

  private SpriteRenderer _renderer;
  private Color _tempColor;
  private Transform _selfTransform;
  private Transform _playerTransform;
  private UI_Update _UI_Panel; // Панель в интерфейсе для вывода инфориации об объекте.
  private EnvironmentDisplay _environmentDisplay;

  private damageState _damageState = damageState.noDamage;
  private selectionState _selectionState = selectionState.normal; // Никак не использую эти состояния. Возможно они и не нужны.

  private void Awake()
  {
    _renderer = GetComponent<SpriteRenderer>();
    _selfTransform = GetComponent<Transform>();
    _playerTransform = GameObject.Find("PlayerOLD").transform;
    _UI_Panel = GameObject.Find("TooltipPanelOLD").GetComponent<UI_Update>();
    _environmentDisplay = GetComponent<EnvironmentDisplay>();
  }

  private void Update()
  {
    if (_damageState == damageState.increase)
    {
      ColorChange(onDamageColor, damageState.decrease);
    }

    if (_damageState == damageState.decrease)
    {
      ColorChange(_tempColor, damageState.noDamage);
    }
  }

  private void OnMouseEnter()
  {
    if (PlayerCheck() < playerDistance)
    {
      _selectionState = selectionState.selectedPlayer;
      _renderer.color = selectedPlColor;
    }
    else
    {
      _selectionState = selectionState.selected;
      _renderer.color = selectedColor;
    }

    _UI_Panel.DataReplacement(_renderer.sprite, _environmentDisplay.tileEnvironment.nameMaterial, _environmentDisplay.tileEnvironment.description, _environmentDisplay.strength);
    _UI_Panel.Fade(1);
  }

  private void OnMouseDown()
  {
    if (PlayerCheck() < playerDistance)
    {
      _environmentDisplay.EnvironmentDamage();
    }
    else
    {
      MessageManager.ShowMessageOnlyText("Подойдите ближе, если хотите взаимодействовать с этим тайлом");
    }
  }

  private void OnMouseExit()
  {
    _selectionState = selectionState.normal;
    if (_damageState != damageState.noDamage)
    {
      _tempColor = defaultColor;
      _damageState = damageState.decrease;
    }
    else
    {
      _renderer.color = defaultColor;
    }

    _UI_Panel.Fade(0);
  }

  private float PlayerCheck()
  {
    return Vector2.Distance(_selfTransform.position, new Vector2(_playerTransform.position.x, _playerTransform.position.y + correctionPlayerY));
  }

  private void ColorChange(Color targetColor, damageState targetState)
  {
    _renderer.color = Color.Lerp(_renderer.color, targetColor, colorChangeSpeed * Time.deltaTime);
    _damageState = (_renderer.color == targetColor) ? targetState : _damageState;
  }

  public void HighlightingDamage()
  {
    if (_damageState == damageState.noDamage)
    {
      _tempColor = _renderer.color;
      _damageState = damageState.increase;
    }
  }

  private enum damageState { noDamage, increase, decrease };
  private enum selectionState { normal, selectedPlayer, selected };
}
