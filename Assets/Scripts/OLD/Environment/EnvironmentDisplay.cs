using UnityEngine;

public class EnvironmentDisplay : MonoBehaviour
{
  [Tooltip("Ссылка на соответствующий Environment (Scriptable Object)")]
  public EnvironmentOLD tileEnvironment;
  [Tooltip("Текущий запас прочности тайла. 100(max) - без повреждений, 0(min) - тайл полностью разрушен")]
  public float strength = 100;
  [Tooltip("Время через которое разрушенный тайл скрывается со сцены")]
  public float shutdownTime = 5;

  private GameObject _thisGamObj;
  private Transform _thisTransform;
  private SpriteRenderer _spriteRend;
  private Rigidbody2D _rigidbody;
  private ColorHighlightingEnvironment _selfHighlighting;
  private ItemDisplay _inHandsItemDisplay;
  private Inventory _inventory;

  private float _damageSize = 0;

  private void OnSave()
  {
    string envToSave = string.Format("{{\n_thisGamObj.name = \"{0}\"\n", _thisGamObj.name);
    envToSave += string.Format("_thisTransform.position = {0}\n", _thisTransform.position.ToString("0.00"));
    envToSave += string.Format("_thisTransform.rotation = {0}\n", _thisTransform.rotation.ToString("0.00"));
    envToSave += string.Format("_rigidbody.velocity = {0}\n", _rigidbody.velocity.ToString("0.00"));
    envToSave += string.Format("EnvironmentDisplay.strength = {0}\n", strength.ToString("0.0"));
    envToSave += string.Format("EnvironmentDisplay._damageSize = {0}\n}}\n", _damageSize.ToString("0.0"));

    GManager.Instance.AddObjectToSave((EnvironmentId)tileEnvironment.id, envToSave);
  }

  private void OnLoad()
  {
    string envToLoad = GManager.Instance.ReadNextObjectToLoad((EnvironmentId)tileEnvironment.id);
    _thisGamObj.name = GManager.FindStringParameterInObject(envToLoad, "_thisGamObj.name");
    _thisTransform.position = GManager.FindVector3InObject(envToLoad, "_thisTransform.position");
    _thisTransform.rotation = GManager.FindQuaternionInSObject(envToLoad, "_thisTransform.rotation");
    strength = GManager.FindFloatParamInSObject(envToLoad, "EnvironmentDisplay.strength");
    _damageSize = GManager.FindFloatParamInSObject(envToLoad, "EnvironmentDisplay._damageSize");

    CancelInvoke();
    SetStateEnvironment();

    if (_rigidbody.bodyType == RigidbodyType2D.Dynamic)
      _rigidbody.velocity = GManager.FindVector2InObject(envToLoad, "_rigidbody.velocity");
  }

  private void Awake()
  {
    _thisGamObj = gameObject;
    _thisTransform = transform;
    _spriteRend = GetComponent<SpriteRenderer>();
    if (tileEnvironment.id != 0)
      _rigidbody = GetComponent<Rigidbody2D>();

    _selfHighlighting = GetComponent<ColorHighlightingEnvironment>();
    _inventory = GameObject.Find("PlayerOLD").GetComponent<Inventory>();
  }

  private void Start()
  {
    if (tileEnvironment.id == 0)
      return;
    GManager.Instance.save += OnSave;
    GManager.Instance.load += OnLoad;
  }

  public void EnvironmentDamage()
  {
    if (strength <= 0)
    {
      MessageManager.ShowMessageWithImage(tileEnvironment.nameMaterial + " уже разрушен", tileEnvironment.spriteDestroyed);
      return;
    }

    if (tileEnvironment.id == 0) // Воздействие на неразрушаемый тайл "Черный камень"
    {
      MessageManager.ShowMessageWithImage(tileEnvironment.nameMaterial + " не возможно разрушить", tileEnvironment.spriteDefault);
      return;
    }
    else
    {
      _inHandsItemDisplay = _inventory.GetItemDInSlot(-1);

      if (_inHandsItemDisplay.item.id == 0) // Если в руках игрока ничего нет...
      {
        MessageManager.ShowMessageOnlyText("Для разрушения тайла необходим соответствующий предмет в руке"); // !! Добавить подсказку о том, какой Item эффективнее всего разрушает именно этот тайл.
        return;
      }

      _damageSize = tileEnvironment.DamageSize(_inHandsItemDisplay.item.id);
      _inHandsItemDisplay.ItemDamage(this);
      if (_damageSize > 0) // Если тайл получил реальный урон...
      {
        strength -= _damageSize;
        _selfHighlighting.HighlightingDamage();

        SetStateEnvironment();
      }
    }
  }

  private void SetStateEnvironment()
  {
    if (100 <= strength)
    {
      _thisGamObj.SetActive(true);
      _thisGamObj.layer = 8; // Ground
      _spriteRend.sprite = tileEnvironment.spriteDefault;
      _rigidbody.bodyType = RigidbodyType2D.Static;
    }
    else if (30 < strength && strength < 100)
    {
      _thisGamObj.SetActive(true);
      _thisGamObj.layer = 8; // Ground
      _spriteRend.sprite = tileEnvironment.spriteDamaged;
      _rigidbody.bodyType = RigidbodyType2D.Static;
    }
    else if (0 < strength && strength <= 30)
    {
      _thisGamObj.SetActive(true);
      _thisGamObj.layer = 8; // Ground
      _spriteRend.sprite = tileEnvironment.spriteDamaged;
      _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    else if (strength <= 0)
    {
      if (_damageSize > 0)
      {
        if (tileEnvironment.id == 5) // Если дерево, то поварачиваем, когда разрушено !! КОСТЫЛЬ!
          _rigidbody.AddTorque(-2, ForceMode2D.Impulse); // Крутящий момент сделать в зависимости от стороны, с которой стоит игрок (отлетает в сторону от игрока).

        _thisGamObj.SetActive(true);
        Invoke("Shutdown", shutdownTime);
      }
      else
        _thisGamObj.SetActive(false);

      _thisGamObj.layer = 11; // Ground destroyed
      _spriteRend.sprite = tileEnvironment.spriteDestroyed;
      _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
  }

  private void Shutdown()
  {
    _damageSize = 0;
    _thisGamObj.SetActive(false);
  }
}