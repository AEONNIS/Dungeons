using UnityEngine;
using System;

namespace Game.Model
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
