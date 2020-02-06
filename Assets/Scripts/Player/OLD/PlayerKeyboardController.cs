using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour //OLD
{
  // Используемые клавиши.
  [Header("Клавиши управления")]
  // public KeyCode rightKey = KeyCode.RightArrow;
  // public KeyCode leftKey = KeyCode.LeftArrow;
  public KeyCode upKey = KeyCode.UpArrow;
  public KeyCode downKey = KeyCode.DownArrow;
  public KeyCode jumpKey = KeyCode.Space;
  public KeyCode forceSwitchKey = KeyCode.LeftControl;
  public KeyCode inventoryKey = KeyCode.I;
  public KeyCode takeItemsKey = KeyCode.T;
  public KeyCode pauseMenuKey = KeyCode.Escape;

  private PlayerOLD _player;
  private Inventory _inventory;

  private float horizontal;

  private void Awake()
  {
    _player = _player == null ? GetComponent<PlayerOLD>() : _player;
    if (_player == null)
      Debug.LogError("Скрипт 'Player' не установлен на контроллер");

    _inventory = _inventory == null ? GetComponent<Inventory>() : _inventory;
    if (_inventory == null)
      Debug.LogError("Скрипт 'Inventory' не установлен на контроллер");
  }

  private void Update()
  {
    if (Input.GetKeyDown(pauseMenuKey))
    {
      if (GManager.Instance.GamePaused)
        GManager.Instance.ResumeGame();
      else
        GManager.Instance.PauseGame();
    }

    if (_player != null && !GManager.Instance.GamePaused)
    {
      if (Input.GetKeyDown(forceSwitchKey))
        _player.ForceSwitch();
    }

    if (_inventory != null && !GManager.Instance.GamePaused)
    {
      if (Input.GetKeyDown(inventoryKey))
        _inventory.OpenCloseInventory();

      if (Input.GetKeyDown(takeItemsKey))
        _inventory.TakeInInventory();
    }
  }

  private void FixedUpdate()
  {
    if (_player != null && !GManager.Instance.GamePaused)
    {
      horizontal = Input.GetAxisRaw("Horizontal");

      if (Input.GetKey(jumpKey))
      {
        if (horizontal == 0)
          _player.Jump();
        else
          _player.JumpMove(horizontal);
      }
      else
      {
        if (horizontal != 0)
          _player.Move(horizontal);
        else
          _player.MoveToIdle();
      }
    }
  }
}
