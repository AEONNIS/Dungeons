using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentOLD", menuName = "OLD/EnvironmentOLD", order = 1)]
public class EnvironmentOLD : ScriptableObject
{
    [Tooltip("Идентификатор тайла. Смотри перечисление EnvironmentId.")]
    public int id = 0;
    [Tooltip("Название материала")]
    public string nameMaterial;
    [TextArea(3, 7), Tooltip("Описание материала. Используется во всплывающей подсказке.")]
    public string description;
    [Tooltip("Проходимость тайла для персонажа")]
    public bool passability = false;
    [Tooltip("Количество единиц повреждающих запас прочности данного Environment от взаимодействия со всеми Item.")]
    public List<DamageFromItem> damagesFromItems;
    [Tooltip("Спрайт тайла")]
    public Sprite spriteDefault;
    [Tooltip("Спрайт тайла, когда он поврежден, но не разрушен")]
    public Sprite spriteDamaged;
    [Tooltip("Спрайт тайла, когда он разрушен")]
    public Sprite spriteDestroyed;

    public float DamageSize(int itemId)
    {
        for (int i = 0; i < damagesFromItems.Count; i++)
        {
            if (damagesFromItems[i].idItem == itemId)
            {
                return damagesFromItems[i].damage;
            }
        }
        return 0;
    }
}

[Serializable]
public struct DamageFromItem
{
    [Tooltip("id соответствующего Item")]
    public int idItem;
    [Tooltip("Количество единиц повреждения от Item")]
    public float damage;
}

public enum EnvironmentId { Stone0, Stone1, Stone2, Ground1, Ice1, Tree1 }