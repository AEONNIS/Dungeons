// !! Когда ломается предмет в руках и в инвентаре есть такой же предмет, имеет смысл помещать его в руки.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  public Transform inventoryPanelT;
  public Image imgHands;
  public Sprite sprEmptyHands;
  public Sprite sprEmptySlot;
  public ItemDisplay itemDEmpty;
  public Collider2D itemsCheck;
  public LayerMask layerItems;

  private int _numEmptySlots;

  private GameObject _inventoryPanelGO;
  private Transform _playerTransform;

  private ItemDisplay _itemDTemp;
  private ItemDisplay _itemDHands;
  private List<ItemDisplay> _itemsDSlots;
  private Image[] _imgSlots;

  private ContactFilter2D _fltrItems = new ContactFilter2D();
  private Collider2D[] _collidersItems;

  private void OnLoad()
  {
    ResetInventory();
  }

  private void Awake()
  {
    _numEmptySlots = inventoryPanelT.childCount;

    _inventoryPanelGO = inventoryPanelT.gameObject;
    _playerTransform = transform;

    _itemDHands = itemDEmpty;
    _itemsDSlots = new List<ItemDisplay>(_numEmptySlots);
    for (int i = 0; i < _numEmptySlots; i++)
    {
      _itemsDSlots.Add(itemDEmpty);
    }

    _imgSlots = new Image[_numEmptySlots];
    for (int i = 0; i < _numEmptySlots; i++)
    {
      _imgSlots[i] = inventoryPanelT.GetChild(i).GetComponent<Image>();
    }

    _fltrItems.SetLayerMask(layerItems);
    _collidersItems = new Collider2D[_numEmptySlots];

    OpenCloseInventory();
  }

  private void Start()
  {
    GManager.Instance.resetInventory += OnLoad;
  }

  public void OpenCloseInventory()
  {
    _inventoryPanelGO.SetActive(!_inventoryPanelGO.activeSelf);
  }

  public void TakeInHands(ItemDisplay itemD) // Взять в руки выбранный мышью предмет.
  {
    if (_itemDHands.item.id != 0) // Если руки заняты...
    {
      if (_numEmptySlots != 0) // ...а в инвентаре есть место...
      {
        for (int slot = 0; slot < _itemsDSlots.Count; slot++)
        {
          if (_itemsDSlots[slot].item.id == 0) // ...находим пустой слот...
          {
            MessageManager.ShowMessageWithImage("Предмет в руках помещен в инвентарь", imgHands.sprite);
            SwapItemsInSlots(slot, -1); // Перекладываем предмет в руках в инвентарь
            break;
          }
        }
      }
      else // ...и в инвентаре нет места
      {
        MessageManager.ShowMessageOnlyText("Руки и инвентарь уже заняты");
        MessageManager.ShowMessageOnlyText("Если хотите подобрать этот предмет, освободите руки или место в инвентаре");
        return;
      }
    }

    _itemDHands = itemD;
    imgHands.sprite = _itemDHands.item.spriteInInventory; // VISUAL
    _itemDHands._NumSlotInInventory = -1;
    _itemDHands._WarningShown = false;
    _itemDHands._thisGamObj.SetActive(false);

    MessageManager.ShowMessageWithImage("Взяли в руки " + _itemDHands.item.nameItem, _itemDHands.item.spriteOnScene);
  }

  public void TakeInInventory() // Подбор в инвентарь всех предметов поблизости игрока.
  {
    if (_numEmptySlots == 0)
    {
      MessageManager.ShowMessageOnlyText("В инвентаре нет свободного места");
      MessageManager.ShowMessageOnlyText("0 предметов помещено в инвентарь");
      return;
    }

    int numItemsScene = ItemsCheck();
    if (numItemsScene == 0)
    {
      MessageManager.ShowMessageOnlyText("Возле игрока нет предметов для подбора");
      return;
    }

    int numItemsTaken = 0;

    for (int slot = 0; slot < _itemsDSlots.Count; slot++)
    {
      if (_itemsDSlots[slot].item.id == 0) // Если слот пустой...
      {
        if (numItemsTaken < numItemsScene) // Если взяли еще не все предметы...
        {
          _itemsDSlots[slot] = _collidersItems[numItemsTaken].GetComponent<ItemDisplay>();
          _imgSlots[slot].sprite = _itemsDSlots[slot].item.spriteInInventory; // VISUAL
          _itemsDSlots[slot]._NumSlotInInventory = slot;
          numItemsTaken++;
          _numEmptySlots--;
          _itemsDSlots[slot]._thisGamObj.SetActive(false);
          MessageManager.ShowMessageWithImage("Подняли " + _itemsDSlots[slot].item.nameItem, _itemsDSlots[slot].item.spriteOnScene);
        }
      }
    }

    MessageManager.ShowMessageOnlyText("Всего поднято " + numItemsTaken + " предметов");
    if (numItemsScene > numItemsTaken)
    {
      MessageManager.ShowMessageOnlyText("Предметов не влезло в инвентарь: " + (numItemsScene - numItemsTaken));
    }
  }

  public void SwapItemsInSlots(int idSlot1, int idSlot2) // Меняем местами предметы в двух ячейках.
  {
    _itemDTemp = _itemsDSlots[idSlot1];

    if (idSlot2 == -1) // -1 означает слот рук
    {
      if (_itemsDSlots[idSlot1].item.id == 0 && _itemDHands.item.id == 0)
        return;

      _itemsDSlots[idSlot1] = _itemDHands;
      _itemsDSlots[idSlot1]._NumSlotInInventory = idSlot1;
      _itemDHands = _itemDTemp;
      _itemDHands._NumSlotInInventory = -1;
      _itemDHands._WarningShown = false;

      if (_itemsDSlots[idSlot1].item.id == 0 && _itemDHands.item.id != 0)
        _numEmptySlots++;
      if (_itemsDSlots[idSlot1].item.id != 0 && _itemDHands.item.id == 0)
        _numEmptySlots--;

      if (_itemDHands.item.id == 0) // VISUAL
        imgHands.sprite = sprEmptyHands;
      else
        imgHands.sprite = _itemDHands.item.spriteInInventory;
    }
    else
    {
      if (_itemsDSlots[idSlot1].item.id == 0 && _itemsDSlots[idSlot2].item.id == 0)
        return;

      _itemsDSlots[idSlot1] = _itemsDSlots[idSlot2];
      _itemsDSlots[idSlot1]._NumSlotInInventory = idSlot1;
      _itemsDSlots[idSlot2] = _itemDTemp;
      _itemsDSlots[idSlot2]._NumSlotInInventory = idSlot2;

      if (_itemsDSlots[idSlot2].item.id == 0) // VISUAL=
        _imgSlots[idSlot2].sprite = sprEmptySlot;
      else
        _imgSlots[idSlot2].sprite = _itemsDSlots[idSlot2].item.spriteInInventory;
    }

    if (_itemsDSlots[idSlot1].item.id == 0) // VISUAL=
      _imgSlots[idSlot1].sprite = sprEmptySlot;
    else
      _imgSlots[idSlot1].sprite = _itemsDSlots[idSlot1].item.spriteInInventory;
  }

  public void DiscardItem(int idSlot) // Выбросить предмет из инвентаря.
  {
    if (idSlot == -1) // -1 означает слот рук
    {
      _itemDHands._thisGamObj.transform.position = _playerTransform.position;
      _itemDHands._NumSlotInInventory = -2;
      _itemDHands._thisGamObj.SetActive(true);

      MessageManager.ShowMessageWithImage("Выбросили из рук " + _itemDHands.item.nameItem, _itemDHands.item.spriteInInventory);

      _itemDHands = itemDEmpty;
      imgHands.sprite = sprEmptyHands;
    }
    else
    {
      _itemsDSlots[idSlot]._thisGamObj.transform.position = _playerTransform.position;
      _itemsDSlots[idSlot]._NumSlotInInventory = -2;
      _itemsDSlots[idSlot]._thisGamObj.SetActive(true);

      MessageManager.ShowMessageWithImage("Выбросили из инвентаря " + _itemsDSlots[idSlot].item.nameItem, _itemsDSlots[idSlot].item.spriteInInventory);
      _numEmptySlots++;

      _itemsDSlots[idSlot] = itemDEmpty;
      _imgSlots[idSlot].sprite = sprEmptySlot;
    }
  }

  private void ResetInventory()
  {
    for (int i = -1; i < _itemsDSlots.Count; i++)
      DeleteItemD(i);
  }

  public void DeleteItemD(int idSlot)
  {
    if (idSlot == -1) // -1 означает слот рук
    {
      if (_itemDHands.item.id != 0)
      {

        _itemDHands._NumSlotInInventory = -2;
        _itemDHands = itemDEmpty;
        imgHands.sprite = sprEmptyHands;
      }
    }
    else
    {
      if (_itemsDSlots[idSlot].item.id != 0)
      {

        _itemsDSlots[idSlot]._NumSlotInInventory = -2;
        _itemsDSlots[idSlot] = itemDEmpty;
        _imgSlots[idSlot].sprite = sprEmptySlot;
      }
    }
  }

  private int ItemsCheck() // Ищем все предметы для подбора, попадающие в коллайдер.
  // ! Если предметов в коллайдере больше ячеек инвентаря (размер массива для результатов), то об оставшихся не будет известно.
  {
    Array.Clear(_collidersItems, 0, _collidersItems.Length);

    itemsCheck.enabled = true;
    int num = Physics2D.OverlapCollider(itemsCheck, _fltrItems, _collidersItems);
    itemsCheck.enabled = false;

    return num;
  }

  public ItemDisplay GetItemDInSlot(int idSlot)
  {
    if (idSlot == -1) // -1 означает слот рук
    {
      return _itemDHands;
    }
    else
    {
      return _itemsDSlots[idSlot];
    }
  }

  public void SetItemDInSlot(ItemDisplay itemD, int idSlot) // Используем в OnLoad() для ItemDisplay.
  {
    if (idSlot == -1) // -1 означает слот рук
    {
      _itemDHands = itemD;
      imgHands.sprite = _itemDHands.item.spriteInInventory; // VISUAL
      _itemDHands._NumSlotInInventory = -1;
      _itemDHands._WarningShown = false;
      _itemDHands._thisGamObj.SetActive(false);
    }
    else
    {
      _itemsDSlots[idSlot] = itemD;
      _imgSlots[idSlot].sprite = itemD.item.spriteInInventory; // VISUAL
      _itemsDSlots[idSlot]._NumSlotInInventory = idSlot;
      _itemsDSlots[idSlot]._thisGamObj.SetActive(false);
      _numEmptySlots--;
    }
  }
}
