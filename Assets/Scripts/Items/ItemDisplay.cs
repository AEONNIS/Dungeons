using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
  [Tooltip("Ссылка на соответствующий Item (Scriptable Object)")]
  public Item item;
  [Tooltip("Текущий запас прочности предмета. 100(max) - новый предмет, 0(min) - предмет полностью разрушен")]
  public float strength = 100;
  [HideInInspector]
  public GameObject _thisGamObj;

  private Transform _thisTransform;
  private Rigidbody2D _rigidbody;
  private Inventory _inventory;

  /// <summary>
  /// В каком слоте инвентаря находится предмет. -2 - предмет не помещен в инвентарь.
  /// -1 - предмет в слоте рук, 0-15 - предмет в соответствующем слоте инвентаря.
  /// </summary>
  private int _numSlotInInventory = -2;
  public int _NumSlotInInventory
  {
    get { return _numSlotInInventory; }
    set { _numSlotInInventory = value; }
  }
  private float _damageSize;
  private bool _warningShown;

  public bool _WarningShown
  {
    get { return _warningShown; }
    set { _warningShown = value; }
  }

  private void OnSave()
  {
    string itemToSave = string.Format("{{\n_thisGamObj.name = \"{0}\"\n", _thisGamObj.name);
    itemToSave += string.Format("_thisTransform.position = {0}\n", _thisTransform.position.ToString("0.00"));
    itemToSave += string.Format("_thisTransform.rotation = {0}\n", _thisTransform.rotation.ToString("0.00"));
    itemToSave += string.Format("_rigidbody.velocity = {0}\n", _rigidbody.velocity.ToString("0.00"));
    itemToSave += string.Format("ItemDisplay.strength = {0}\n", strength.ToString("0.0"));
    itemToSave += string.Format("ItemDisplay._numSlotInInventory = {0}\n}}\n", _numSlotInInventory);

    GManager.Instance.AddObjectToSave((ItemId)item.id, itemToSave);
  }

  private void OnLoad()
  {
    string itemToLoad = GManager.Instance.ReadNextObjectToLoad((ItemId)item.id);
    _thisGamObj.name = GManager.FindStringParameterInObject(itemToLoad, "_thisGamObj.name");
    _thisTransform.position = GManager.FindVector3InObject(itemToLoad, "_thisTransform.position");
    _thisTransform.rotation = GManager.FindQuaternionInSObject(itemToLoad, "_thisTransform.rotation");
    _rigidbody.velocity = GManager.FindVector2InObject(itemToLoad, "_rigidbody.velocity");
    strength = GManager.FindFloatParamInSObject(itemToLoad, "ItemDisplay.strength");
    _numSlotInInventory = (int)GManager.FindFloatParamInSObject(itemToLoad, "ItemDisplay._numSlotInInventory");

    if (_numSlotInInventory != -2)
      _inventory.SetItemDInSlot(this, _numSlotInInventory);

    SetStateItem();
  }

  private void Awake()
  {
    _thisGamObj = gameObject;
    _thisTransform = transform;
    _rigidbody = GetComponent<Rigidbody2D>();
    _inventory = GameObject.Find("Player").GetComponent<Inventory>();
  }

  private void Start()
  {
    GManager.Instance.save += OnSave;
    GManager.Instance.load += OnLoad;
  }

  public void ItemDamage(EnvironmentDisplay envDisp)
  {
    _damageSize = item.DamageSize(envDisp.tileEnvironment.id);
    if (_damageSize > 0)
    {
      strength -= _damageSize;

      if (0 < strength && strength <= 10 && !_warningShown)
      {
        MessageManager.ShowMessageWithImage(item.nameItem + " в руках игрока скоро сломается", item.spriteInInventory);
        _warningShown = true;
      }
      if (strength <= 0)
      {
        MessageManager.ShowMessageWithImage(item.nameItem + " в руках игрока сломан(а)", item.spriteInInventory);
        _inventory.DeleteItemD(-1);
        _thisGamObj.SetActive(false);
        _thisGamObj.layer = 12;
      }
    }
  }

  private void SetStateItem()
  {
    if (_numSlotInInventory == -2)
    {
      if (strength <= 0)
      {
        _thisGamObj.SetActive(false);
        _thisGamObj.layer = 12;
        _warningShown = true;
      }
      else
      {
        _thisGamObj.SetActive(true);
        _thisGamObj.layer = 9;
        _warningShown = false;
      }
    }
    else
    {
      _thisGamObj.SetActive(false);
      _thisGamObj.layer = 9;
      _warningShown = false;
    }
  }
}
