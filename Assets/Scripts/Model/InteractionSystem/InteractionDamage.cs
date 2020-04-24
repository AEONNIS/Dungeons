using UnityEngine;
using System;

namespace Dungeons.Model.InteractionSystem
{
    [Serializable]
    public class InteractionDamage
    {
        [SerializeField] private InteractionCouple _interactionCouple;
        [SerializeField] private Damage _damage;

        public InteractionCouple InteractionCouple => _interactionCouple;
        public Damage Damage => _damage;
    }
}
