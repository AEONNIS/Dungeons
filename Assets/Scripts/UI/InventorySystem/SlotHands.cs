using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotHands : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
  public RectTransform RT_slotForDrag;
  [Header("Цвета")]
  public Color clrDefault = Color.white;
  [Tooltip("Цвет подсветки при наведении")]
  public Color clrPointerEnter = new Color(115, 255, 160, 255);
  [Tooltip("Цвет в процессе перетаскивания")]
  public Color clrDragging = new Color(255, 170, 100, 255);

  private int _idSlot = -1;
  private bool _dragging = false;

  private Camera _camera;
  private Vector2 _mousePosition;
  private float _xRefResolution;
  private RectTransform _RTSelf;
  private RectTransform _RTInventoryPanel;
  private Image _imgSelf;
  private Image _imgSlotForDrag;
  private ItemDisplay _itemDSelf;
  public ItemDisplay _ItemDSelf { get { return _itemDSelf; } }
  private UI_Update _UI_Panel;
  private Inventory _inventory;

  private void Awake()
  {
    _camera = Camera.main;
    _xRefResolution = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution.x;
    _RTSelf = GetComponent<RectTransform>();
    _RTInventoryPanel = GameObject.Find("Inventory").GetComponent<RectTransform>();
    _imgSelf = GetComponent<Image>();
    _imgSlotForDrag = RT_slotForDrag.GetComponent<Image>();
    _UI_Panel = GameObject.Find("UI Tooltip Panel").GetComponent<UI_Update>();
    _inventory = GameObject.Find("PlayerOLD").GetComponent<Inventory>();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (!_dragging)
    {
      _imgSelf.color = clrPointerEnter;
    }

    _itemDSelf = _inventory.GetItemDInSlot(_idSlot);
    if (_itemDSelf.item.id != 0)
    {
      _UI_Panel.DataReplacement(_itemDSelf.item.spriteInInventory, _itemDSelf.item.nameItem, _itemDSelf.item.description, _itemDSelf.strength);
      _UI_Panel.Fade(1);
    }
    else
    {
      _UI_Panel.Fade(0);
    }
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (!_dragging)
    {
      _imgSelf.color = clrDefault;
      _UI_Panel.Fade(0);
    }
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
    {
      _inventory.SwapItemsInSlots(0, _idSlot); // Меняем с первым предметом в инвентаре.
      OnPointerEnter(eventData);
    }
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    _itemDSelf = _inventory.GetItemDInSlot(_idSlot);
    if (_itemDSelf.item.id != 0)
    {
      _dragging = true;
      _imgSelf.color = clrDragging;

      _imgSlotForDrag.sprite = _imgSelf.sprite;
      RT_slotForDrag.offsetMax = _RTSelf.offsetMax;
      RT_slotForDrag.offsetMin = _RTSelf.offsetMin;
      RT_slotForDrag.position = _RTSelf.position;

      RT_slotForDrag.gameObject.SetActive(true);
    }
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (_dragging)
    {
      _mousePosition = _camera.ScreenToWorldPoint(new Vector2(
                       Input.mousePosition.x + RT_slotForDrag.rect.width * 0.6f * Screen.width / _xRefResolution,
                       Input.mousePosition.y - RT_slotForDrag.rect.height * 0.6f * Screen.width / _xRefResolution));
      RT_slotForDrag.position = _mousePosition;
    }
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (_dragging)
    {
      _dragging = false;
      RT_slotForDrag.gameObject.SetActive(false);

      bool outOfInventory = !RectTransformUtility.RectangleContainsScreenPoint(_RTInventoryPanel, Input.mousePosition, _camera);
      bool outOfSelf = !RectTransformUtility.RectangleContainsScreenPoint(_RTSelf, Input.mousePosition, _camera);
      if (outOfInventory && outOfSelf)
      {
        _inventory.DiscardItem(_idSlot);
      }
      else if (!outOfSelf)
      {
        OnPointerEnter(eventData);
        return;
      }
      _imgSelf.color = clrDefault;
    }
  }

  public void OnDrop(PointerEventData eventData)
  {
    SlotInInventory draggingSlot = eventData.pointerDrag.GetComponent<SlotInInventory>();
    if (draggingSlot._ItemDSelf.item.id != 0)
    {
      _inventory.SwapItemsInSlots(draggingSlot._IdSlot, _idSlot);
    }
    OnPointerEnter(eventData);
  }
}
