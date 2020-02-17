using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Model
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
