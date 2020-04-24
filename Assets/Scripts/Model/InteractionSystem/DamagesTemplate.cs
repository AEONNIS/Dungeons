using Dungeons.Model.Items;
using Dungeons.Model.Tiles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dungeons.Model.InteractionSystem
{
    [CreateAssetMenu(fileName = "DamagesTemplate", menuName = "Dungeons/Model/InteractionSystem/DamagesTemplate")]
    public class DamagesTemplate : ScriptableObject
    {
        [SerializeField] List<InteractionDamage> _damages;

        public Damage GetDamage(ItemType itemType, TileType environmentType)
        {
            return _damages.First(interaction => interaction.InteractionCouple.ItemType == itemType && interaction.InteractionCouple.TileType == environmentType).Damage;
        }
    }
}
