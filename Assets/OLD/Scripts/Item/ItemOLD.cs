using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemOLD", menuName = "ItemOLD", order = 2)]
public class ItemOLD : ScriptableObject
{
  [Tooltip("Идентификатор предмета. Смотри перечисление ItemId.")]
  public int id = 0;
  [Tooltip("Название предмета")]
  public string nameItem;
  [TextArea(3, 7), Tooltip("Описание предмета. Используется во всплывающей подсказке.")]
  public string description;
  [Tooltip("Максимум предметов этого типа в одном слоте инвентаря. Пока не реализовано объединение предметов в стеке, потому не используется")]
  public int maxCountInStack = 1;
  [Tooltip("Количество единиц повреждающих запас прочности данного Item от взаимодействия со всеми Environment.")]
  public List<DamageFromEnvironment> damagesFromEnvironments;
  [Tooltip("Спрайт предмета на сцене")]
  public Sprite spriteOnScene;
  [Tooltip("Спрайт предмета в инвентаре")]
  public Sprite spriteInInventory;

  public float DamageSize(int EnvironmentId)
  {
    for (int i = 0; i < damagesFromEnvironments.Count; i++)
    {
      if (damagesFromEnvironments[i].idEnvironment == EnvironmentId)
      {
        return damagesFromEnvironments[i].damage;
      }
    }
    return 0;
  }
}

[Serializable]
public struct DamageFromEnvironment
{
  [Tooltip("id соответсвующего Environment")]
  public int idEnvironment;
  [Tooltip("Количество единиц повреждения от Environment")]
  public float damage;
}

public enum ItemId { EmptyItem, Pick1, Axe1, Shovel1 };