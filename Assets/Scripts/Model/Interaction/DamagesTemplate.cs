using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "DamagesTemplate", menuName = "Model/DamagesTemplate")]
    public class DamagesTemplate : ScriptableObject
    {
        [SerializeField] List<InteractionDamage> _damages;


        public Damage GetDamage(ItemBase item, EnvironmentBase environment)
        {
            return _damages.First(interaction => interaction.InteractionCouple.Item == item && interaction.InteractionCouple.Environment == environment).Damage;
        }
    }
}
