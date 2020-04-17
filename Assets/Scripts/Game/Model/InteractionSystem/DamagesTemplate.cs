using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Game.Model.Tiles;
using Game.Model.Items;

namespace Game.Model.InteractionSystem
{
    [CreateAssetMenu(fileName = "DamagesTemplate", menuName = "Model/DamagesTemplate")]
    public class DamagesTemplate : ScriptableObject
    {
        [SerializeField] List<InteractionDamage> _damages;

        public Damage GetDamage(ItemType itemType, EnvironmentType environmentType)
        {
            return _damages.First(interaction => interaction.InteractionCouple.ItemType == itemType && interaction.InteractionCouple.EnvironmentType == environmentType).Damage;
        }
    }
}
